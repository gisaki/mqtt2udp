﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;

namespace Mqtt2Udp
{
    public partial class Form1 : Form
    {
        // アプリ
        private IMqtt2UdpUserApp user_app_ = new UserApp();
        private System.Timers.Timer user_app_timer_ = null;

        public Form1()
        {
            InitializeComponent();

            textBoxHost.Text = Properties.Settings.Default.host;
            textBoxUsername.Text = Properties.Settings.Default.username;
            textBoxPassword.Text = Properties.Settings.Default.password;
            textBoxTopicSub.Text= Properties.Settings.Default.topic_sub;
            textBoxTopicPub.Text = Properties.Settings.Default.topic_pub;
            textBox_UDPIP.Text = Properties.Settings.Default.udp_ip;
            textBox_UDPPort.Text = Properties.Settings.Default.udp_port;

            update_gui();

            // アプリ
            user_app_.FormInitialized();
            // タイマーの生成
            this.user_app_timer_ = new System.Timers.Timer();
            this.user_app_timer_.Elapsed += new System.Timers.ElapsedEventHandler(OnElapsed_TimersTimer);
            this.user_app_timer_.Interval = 1000;
            // タイマーを開始
            this.user_app_timer_.Start();
        }

        private void OnElapsed_TimersTimer(object sender, System.Timers.ElapsedEventArgs e)
        {
            // アプリ
            user_app_.OnElapsed_TimersTimer(sender, e);
        }

        // サーバ証明書の検証
        private static Boolean RemoteCertificateValidationCallback(Object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            //　サーバ証明書を検証せず、常に許容
            return true;
        }
        
        private MqttClient client_ = null;
        private bool client_ongoing_ = false;
        private void button_MQTT_Con_Click(object sender, EventArgs e)
        {
            // 連続操作抑止
            if (this.client_ongoing_)
            {
                return;
            }
            this.client_ongoing_ = true;
            update_gui(); // GUI更新

            // すでに接続していれば切断
            mqtt_close();

            string host = textBoxHost.Text;
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;
            string topicsub = textBoxTopicSub.Text;

            try
            {
                // create client instance
                this.client_ = new MqttClient(host, 8883, true, MqttSslProtocols.TLSv1_2, RemoteCertificateValidationCallback, null);

                // register to message received
                this.client_.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

                string clientId = Guid.NewGuid().ToString();
                this.client_.Connect(clientId, username, password);

                // subscribe to the topic "/home/temperature" with QoS 0
                this.client_.Subscribe(
                    new string[] { topicsub },
                    new byte[] { uPLibrary.Networking.M2Mqtt.Messages.MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE }
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine("MQTT Connect error({0})", ex.Message);
                mqtt_close();
            }

            this.client_ongoing_ = false;
            update_gui(); // GUI更新

            textBox1.Text = string.Empty;
            string msg = ((this.client_ != null) && (this.client_.IsConnected)) ? "MQTT Connected" : "MQTT Connect failed";
            textBox1.Text += string.Format("[{0}]{1}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff"), msg);

            // アプリ
            if ((this.client_ != null) && (this.client_.IsConnected))
            {
                user_app_.Connected();
            }
        }

        private void button_MQTT_Dis_Click(object sender, EventArgs e)
        {
            // 連続操作抑止
            if (this.client_ongoing_)
            {
                return;
            }
            this.client_ongoing_ = true;
            update_gui(); // GUI更新

            // （接続していれば）切断
            mqtt_close();

            this.client_ongoing_ = false;
            update_gui(); // GUI更新

            // アプリ
            user_app_.Disconnected();
        }

        private void mqtt_close()
        {
            if (this.client_ != null)
            {
                this.client_.Disconnect();
            }
            this.client_ = null;
            textBox1.Text = string.Empty;
            string msg = "MQTT Closed";
            textBox1.Text += string.Format("[{0}]{1}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff"), msg);
        }

        private void update_gui()
        {
            if (this.client_ongoing_)
            {
                button_MQTT_Con.Enabled = false;
                button_MQTT_Dis.Enabled = false;
                button_MQTT_Pub.Enabled = false;
            }
            else if (this.client_ == null)
            {
                button_MQTT_Con.Enabled = true;
                button_MQTT_Dis.Enabled = false;
                button_MQTT_Pub.Enabled = false;
            }
            else if (this.client_.IsConnected)
            {
                button_MQTT_Con.Enabled = false;
                button_MQTT_Dis.Enabled = true;
                button_MQTT_Pub.Enabled = true;
            }
            else
            {
                button_MQTT_Con.Enabled = true;
                button_MQTT_Dis.Enabled = false;
                button_MQTT_Pub.Enabled = false;
            }
        }

        private void client_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            // handle message received
            this.textBox1.Invoke((Action)(() =>
            {
                textBox1.Text = string.Empty;
                textBox1.Text += string.Format("[{0}][{1}]", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss fff"), e.Topic);
                string msg = Encoding.UTF8.GetString(e.Message);
                textBox1.Text += msg;
                textBox1.Text += "\r\n";

                // raw
                textBox1.Text += string.Format("[{0}]\r\n", BitConverter.ToString(e.rcvbuf));

                {
                    try
                    {
                        // 送受信に利用するIPアドレス、ポート番号
                        int port = Int32.Parse(textBox_UDPPort.Text);
                        System.Net.IPAddress ipaddress = System.Net.IPAddress.Parse(textBox_UDPIP.Text);
                        this.udpdst_ = new System.Net.IPEndPoint(ipaddress, port);

                        // 送信データ
                        byte[] buffer = e.rcvbuf;
                        // UDP送信
                        udp_send_aync(buffer);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("udp port/ipaddress error({0})", ex.Message);
                    }
                }

                // アプリ
                user_app_.MqttMsgPublishReceived(sender, e);
            }));
        }

        // UDP
        private System.Net.Sockets.UdpClient udpclient_ = null;
        System.Net.IPEndPoint udpdst_ = null;
        private void udp_send_aync(byte[] sndBuffer)
        {
            if (this.udpclient_  == null)
            {
                this.udpclient_ = new System.Net.Sockets.UdpClient();
            }

            this.udpclient_.BeginSend(
                sndBuffer, sndBuffer.Length,
                udpdst_,
                new AsyncCallback(udp_send_callback), this.udpclient_
            );
        }
        public static void udp_send_callback(IAsyncResult ar)
        {
            System.Net.Sockets.UdpClient udp = (System.Net.Sockets.UdpClient)ar.AsyncState;
            // 送信終了
            try
            {
                udp.EndSend(ar);
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                Console.WriteLine("udp send error({0}/{1})", ex.Message, ex.ErrorCode);
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine("udp has closed({0})", ex.Message);
            }
        }

        private void button_MQTT_Pub_Click(object sender, EventArgs e)
        {
            if (this.client_ == null) 
            {
                return;
            }
            string topicpub = textBoxTopicPub.Text;
            this.client_.Publish(
                topicpub, 
                Encoding.UTF8.GetBytes("hello vs!"), 
                uPLibrary.Networking.M2Mqtt.Messages.MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, 
                false
            );
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // アプリ
            // タイマーを停止
            this.user_app_timer_.Stop();

            Properties.Settings.Default.host = textBoxHost.Text;
            Properties.Settings.Default.username = textBoxUsername.Text;
            Properties.Settings.Default.password = textBoxPassword.Text;
            Properties.Settings.Default.topic_sub = textBoxTopicSub.Text;
            Properties.Settings.Default.topic_pub = textBoxTopicPub.Text;
            Properties.Settings.Default.udp_ip = textBox_UDPIP.Text;
            Properties.Settings.Default.udp_port = textBox_UDPPort.Text;
            Properties.Settings.Default.Save();

            mqtt_close();
            if (this.udpclient_ != null)
            {
                this.udpclient_.Close();
            }

            // アプリ
            user_app_.FormClosed();
        }
    }

    public interface IMqtt2UdpUserApp
    {
        void FormInitialized();
        void FormClosed();
        void Connected();
        void Disconnected();
        void MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e);
        void OnElapsed_TimersTimer(object sender, System.Timers.ElapsedEventArgs e);
    }
}
