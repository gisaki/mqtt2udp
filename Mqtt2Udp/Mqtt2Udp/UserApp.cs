using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mqtt2Udp
{
    public class UserApp : IMqtt2UdpUserApp
    {
        // コンストラクタ
        public UserApp() { }

        public void FormInitialized()
        {
            // TODO
            Console.WriteLine("FormInitialized");
        }
        public void FormClosed()
        {
            // TODO
            Console.WriteLine("FormClosed");
        }
        public void Connected()
        {
            // TODO
            Console.WriteLine("Connected");
        }
        public void Disconnected()
        {
            // TODO
            Console.WriteLine("Disconnected");
        }
        public void MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            // TODO
            Console.WriteLine("MqttMsgPublishReceived");
        }
    }
}
