using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using SimpleTCP;

namespace TCP_CommTest_Server.ViewModels
{
    internal class MessageClass
    {
        public string Command { get; set; }
        public string Response { get; set; }
    }
    internal class ShellViewModel : Screen
    {
		private string message1;
		public string Message1
		{
			get { return message1; }
			set { message1 = value;  NotifyOfPropertyChange(() => Message1); }
		}

        private string message2;
        public string Message2
        {
            get { return message2; }
            set { message2 = value; NotifyOfPropertyChange(() => Message2); }
        }

        private string message3;
        public string Message3
        {
            get { return message3; }
            set { message3 = value; NotifyOfPropertyChange(() => Message3); }
        }

        private string message4;
        public string Message4
        {
            get { return message4; }
            set { message4 = value; NotifyOfPropertyChange(() => Message4); }
        }

        private string message5;
        public string Message5
        {
            get { return message5; }
            set { message5 = value; NotifyOfPropertyChange(() => Message5); }
        }

        private string message6;
        public string Message6
        {
            get { return message6; }
            set { message6 = value; NotifyOfPropertyChange(() => Message6); }
        }

        private string message7;
        public string Message7
        {
            get { return message7; }
            set { message7 = value; NotifyOfPropertyChange(() => Message7); }
        }

        private bool canStartListen;
		public bool CanStartListen	
		{
			get { return canStartListen; }
			set
			{
				canStartListen = value;
				NotifyOfPropertyChange(() => CanStartListen);
			}
		}
        /****************************************************/

        private string autoResp1;
        public string AutoResp1
        {
            get { return autoResp1; }
            set { autoResp1 = value; NotifyOfPropertyChange(() => AutoResp1); }
        }

        private string autoResp2;
        public string AutoResp2
        {
            get { return autoResp2; }
            set { autoResp2 = value; NotifyOfPropertyChange(() => AutoResp2); }
        }

        private string autoResp3;
        public string AutoResp3
        {
            get { return autoResp3; }
            set { autoResp3 = value; NotifyOfPropertyChange(() => AutoResp3); }
        }

        private string autoResp4;
        public string AutoResp4
        {
            get { return autoResp4; }
            set { autoResp4 = value; NotifyOfPropertyChange(() => AutoResp4); }
        }

        private string autoResp5;
        public string AutoResp5
        {
            get { return autoResp5; }
            set { autoResp5 = value; NotifyOfPropertyChange(() => AutoResp5); }
        }

        private string autoResp6;
        public string AutoResp6
        {
            get { return autoResp6; }
            set { autoResp6 = value; NotifyOfPropertyChange(() => AutoResp6); }
        }

        private string autoResp7;
        public string AutoResp7
        {
            get { return autoResp7; }
            set
            {
                autoResp7 = value;
                NotifyOfPropertyChange(() => AutoResp7);
            }
        }
        /****************************************************/

        SimpleTcpServer server = new SimpleTcpServer();

        public void StartListen()
		{
            CanStartListen = false;

            server.Start(8003);
		}

        private ObservableCollection<MessageClass> Commands = new ObservableCollection<MessageClass>();

        private string replyMessage = "****";
        public ShellViewModel()
		{		
			CanStartListen = true;

            Message1 = "IN01"; AutoResp1 = "INOK";           				
            Message2 = "SRDY"; AutoResp2 = "SRDY";
            Message3 = "OP01"; AutoResp3 = "OP01";
            Message4 = "RESP"; AutoResp4 = "RESP";
            Message5 = "RESF"; AutoResp5 = "RESF";
            Message6 = "RESERVED1";  AutoResp6 = "***";
            Message7 = "RESERVED2";  AutoResp7 = "***";
                        
            //Commands.Add(Message1, AutoResp1);
            //Commands.Add(Message2, AutoResp2);
            //Commands.Add(Message3, AutoResp3);
            //Commands.Add(Message4, AutoResp4);
            //Commands.Add(Message5, AutoResp5);
            //Commands.Add(Message6, AutoResp6);
            //Commands.Add(Message7, AutoResp7);
            
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += Server_DataReceived;
            server.ClientConnected += Server_ClientConnected;
            server.ClientDisconnected += Server_ClientDisconnected;
        }

        private void Server_DataReceived(object sender, SimpleTCP.Message e)
        {
            //MessageBox.Show(e.MessageString);

            if (e.MessageString == Message1) 
            {
                e.Reply(autoResp1);
            }
            else if(e.MessageString == Message2)
            {
                e.Reply(autoResp2);
            }
            else if (e.MessageString == Message3)
            {
                e.Reply(autoResp3);
            }
            else if (e.MessageString == Message4)
            {
                e.Reply(autoResp4);
            }
            else if (e.MessageString == Message5)
            {
                e.Reply(autoResp5);
            }
            else if (e.MessageString == Message6)
            {
                e.Reply(autoResp6);
            }
            else if (e.MessageString == Message7)
            {
                e.Reply(autoResp7);
            }
            else
            {
                e.Reply("****");
            }
            //foreach(string command in Commands.Keys)
            //{
            //    if (command == e.MessageString)
            //    {
            //        e.Reply(Commands[command]);
            //        break;
            //    }
            //    else
            //    {
            //        e.Reply("****");
            //    }
            //}
        }

        private void Server_ClientConnected(object sender, TcpClient e)
        {
            
        }
        private void Server_ClientDisconnected(object sender, System.Net.Sockets.TcpClient e)
        {
            if (server != null)
                CanStartListen = true;
        }
    }
}
