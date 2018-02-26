namespace Mqtt2Udp
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_MQTT_Con = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button_MQTT_Pub = new System.Windows.Forms.Button();
            this.textBoxHost = new System.Windows.Forms.TextBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxTopicSub = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxTopicPub = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_UDPIP = new System.Windows.Forms.TextBox();
            this.textBox_UDPPort = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_MQTT_Con
            // 
            this.button_MQTT_Con.Location = new System.Drawing.Point(12, 81);
            this.button_MQTT_Con.Name = "button_MQTT_Con";
            this.button_MQTT_Con.Size = new System.Drawing.Size(84, 23);
            this.button_MQTT_Con.TabIndex = 0;
            this.button_MQTT_Con.Text = "Connect";
            this.button_MQTT_Con.UseVisualStyleBackColor = true;
            this.button_MQTT_Con.Click += new System.EventHandler(this.button_MQTT_Con_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 203);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(435, 119);
            this.textBox1.TabIndex = 1;
            // 
            // button_MQTT_Pub
            // 
            this.button_MQTT_Pub.Location = new System.Drawing.Point(12, 110);
            this.button_MQTT_Pub.Name = "button_MQTT_Pub";
            this.button_MQTT_Pub.Size = new System.Drawing.Size(84, 23);
            this.button_MQTT_Pub.TabIndex = 2;
            this.button_MQTT_Pub.Text = "Publish";
            this.button_MQTT_Pub.UseVisualStyleBackColor = true;
            this.button_MQTT_Pub.Click += new System.EventHandler(this.button_MQTT_Pub_Click);
            // 
            // textBoxHost
            // 
            this.textBoxHost.Location = new System.Drawing.Point(179, 14);
            this.textBoxHost.Name = "textBoxHost";
            this.textBoxHost.Size = new System.Drawing.Size(268, 19);
            this.textBoxHost.TabIndex = 3;
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(179, 39);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(268, 19);
            this.textBoxUsername.TabIndex = 3;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(179, 64);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(268, 19);
            this.textBoxPassword.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(109, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Host";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(109, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Username";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(109, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Password";
            // 
            // textBoxTopicSub
            // 
            this.textBoxTopicSub.Location = new System.Drawing.Point(179, 89);
            this.textBoxTopicSub.Name = "textBoxTopicSub";
            this.textBoxTopicSub.Size = new System.Drawing.Size(268, 19);
            this.textBoxTopicSub.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(109, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "Topic(sub)";
            // 
            // textBoxTopicPub
            // 
            this.textBoxTopicPub.Location = new System.Drawing.Point(179, 114);
            this.textBoxTopicPub.Name = "textBoxTopicPub";
            this.textBoxTopicPub.Size = new System.Drawing.Size(268, 19);
            this.textBoxTopicPub.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(109, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "Topic(pub)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(109, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "UDP";
            // 
            // textBox_UDPIP
            // 
            this.textBox_UDPIP.Location = new System.Drawing.Point(179, 164);
            this.textBox_UDPIP.Name = "textBox_UDPIP";
            this.textBox_UDPIP.Size = new System.Drawing.Size(182, 19);
            this.textBox_UDPIP.TabIndex = 8;
            this.textBox_UDPIP.Text = "255.255.255.255";
            // 
            // textBox_UDPPort
            // 
            this.textBox_UDPPort.Location = new System.Drawing.Point(367, 164);
            this.textBox_UDPPort.Name = "textBox_UDPPort";
            this.textBox_UDPPort.Size = new System.Drawing.Size(80, 19);
            this.textBox_UDPPort.TabIndex = 9;
            this.textBox_UDPPort.Text = "1883";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 334);
            this.Controls.Add(this.textBox_UDPPort);
            this.Controls.Add(this.textBox_UDPIP);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxTopicPub);
            this.Controls.Add(this.textBoxTopicSub);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.textBoxHost);
            this.Controls.Add(this.button_MQTT_Pub);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button_MQTT_Con);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_MQTT_Con;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button_MQTT_Pub;
        private System.Windows.Forms.TextBox textBoxHost;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxTopicSub;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxTopicPub;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_UDPIP;
        private System.Windows.Forms.TextBox textBox_UDPPort;
    }
}

