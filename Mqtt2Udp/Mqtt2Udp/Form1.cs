using System;
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
        private void button_MQTT_Con_Click(object sender, EventArgs e)
        {
            string host = textBoxHost.Text;
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;
            string topicsub = textBoxTopicSub.Text;

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

        private void client_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            // handle message received
            this.textBox1.Invoke((Action)(() =>
            {
                textBox1.Text += string.Format("[{0}][{1}]", DateTime.Now, e.Topic);
                string msg = Encoding.UTF8.GetString(e.Message);
                textBox1.Text += msg;
                textBox1.Text += "\r\n";

                // raw
                textBox1.Text += string.Format("[{0}]\r\n", BitConverter.ToString(e.rcvbuf));

                {
                    // 送受信に利用するIPアドレス、ポート番号
                    int port = Int32.Parse(textBox_UDPPort.Text);
                    System.Net.IPAddress ipaddress = System.Net.IPAddress.Parse(textBox_UDPIP.Text);
                    //System.Net.IPAddress ipaddress = System.Net.IPAddress.Broadcast;

                    // 送信データ
                    byte[] buffer = e.rcvbuf;

                    // UDP送信
                    var client = new System.Net.Sockets.UdpClient(port);
                    client.EnableBroadcast = true;
                    client.Connect(new System.Net.IPEndPoint(ipaddress, port));
                    client.Send(buffer, buffer.Length);
                    client.Close();
                }

            }));
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
            Properties.Settings.Default.host = textBoxHost.Text;
            Properties.Settings.Default.username = textBoxUsername.Text;
            Properties.Settings.Default.password = textBoxPassword.Text;
            Properties.Settings.Default.topic_sub = textBoxTopicSub.Text;
            Properties.Settings.Default.topic_pub = textBoxTopicPub.Text;
            Properties.Settings.Default.udp_ip = textBox_UDPIP.Text;
            Properties.Settings.Default.udp_port = textBox_UDPPort.Text;
            Properties.Settings.Default.Save();

            if (this.client_ == null)
            {
                return;
            }
            this.client_.Disconnect();
        }
    }
}
