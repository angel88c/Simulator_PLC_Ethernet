using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SimpleTCP;

namespace iBtest.TCPClient
{
    public class Control
    {
        private TimeSpan Timeout;
        SimpleTcpClient client = new SimpleTcpClient();

        public event EventHandler<string> OnReceivedEvent;

        public void OnReceived(string args)
        {
            var onReceivedEvent = OnReceivedEvent;
            if(onReceivedEvent != null)
            {
                System.IO.File.WriteAllText(@"c:\temp\event.txt", "Event fired " + args);
                onReceivedEvent(this, args);
            }
        }

        private void Client_DataReceived(object sender, Message e)
        {
            if (OnReceivedEvent != null)
                this.OnReceived(e.MessageString);
        }

        public bool Connect(string ip, int port, int timeout, out string error)
        {
            client = new SimpleTcpClient();
            client.DataReceived += Client_DataReceived;

            if (ip.ToLower() == "localhost") ip = "127.0.0.1";

            Regex regexIP = new Regex(@"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$");

            if (regexIP.Match(ip).Success)
            {
                client.Connect(ip, port);
                if (client.TcpClient != null)
                {
                    if (client.TcpClient.Connected)
                    {
                        error = "";
                        Timeout = TimeSpan.FromMilliseconds(timeout);
                        return true;
                    }
                }

                error = "TCP Client could not connect to " + ip; 
                return false;
            }

            error = "Invalid IP address: " + ip;            
            return false;
        }

        public bool WriteMessage(string message, out string retMessage, out string error)
        {
            error = "";
            var Message = client.WriteAndGetReply(message, Timeout);
            
            if (Message == null)
            {
                retMessage = "";
                error = $"Error, Timeout[{Timeout.TotalMilliseconds}ms]";
                return false;
            }
                        
            retMessage = Message.MessageString;
            return true;
        }

        public bool Disconnect()
        {
            if (client != null)
            {
                if (client.TcpClient != null)
                {
                    if (client.TcpClient.Connected)
                        client.Disconnect();
                }
            }
            return true;
        }
    }
}
