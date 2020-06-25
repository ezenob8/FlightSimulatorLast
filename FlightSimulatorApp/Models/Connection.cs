using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using static FlightSimulatorApp.Core.Managers.ServerConnectionManager;

namespace FlightSimulatorApp
{
    public class Connection
    {
        private int _port;
        private string _ip_addr;

        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        public string IP_addr
        {
            get { return _ip_addr; }
            set { _ip_addr = value; }
        }

        public Connection() {
            _port = Convert.ToInt32(ConfigurationManager.AppSettings.Get("PORT"));
            _ip_addr = ConfigurationManager.AppSettings.Get("IP_address");
        }

        public Socket openSocketToServer() {

            IPAddress ipAddress = IPAddress.Parse(_ip_addr);
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, _port);

             // Create a TCP/IP  socket
            Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            sender.Connect(remoteEP);
            return sender;
        }

        public string sendMessage(Socket sender, string message) {
            try
            {
                //Mutex mutex = new Mutex();
                //mutex.WaitOne();
                // Encode the data string "command" into a byte array "msg"
                byte[] msg = Encoding.ASCII.GetBytes(message);

                // Data buffer for incoming data
                byte[] answer = new byte[1024];

                // Send the data through the socket
                _ = sender.Send(msg);

                // Receive the response from the remote device 
                int bytesRec = sender.Receive(answer);
                string received = Encoding.ASCII.GetString(answer, 0, bytesRec);
                //mutex.ReleaseMutex();

                return received;
            }
             
            catch (SocketException)
            {
                throw new FlightSimulatorConnectionException("It was'nt able to connect to the Server\n");
            }
}


        public string sendEspecificMessageToGets(Socket sender, string message)
        {
            try
            {
                //Mutex mutex = new Mutex();
                //mutex.WaitOne();
                // Encode the data string "command" into a byte array "msg"
                byte[] msg = Encoding.ASCII.GetBytes(message);

                // Data buffer for incoming data
                byte[] answer = new byte[1024];

                // Send the data through the socket
                _ = sender.Send(msg);

                // Receive the response from the remote device 
                int bytesRec = sender.Receive(answer);
                string received = Encoding.ASCII.GetString(answer, 0, bytesRec);
                //mutex.ReleaseMutex();
                return received;
            }

            catch (SocketException)
            {
                throw new FlightSimulatorConnectionException("It wasn't able to connect to the Server\n");
            }
        }

        public void closeSocketWithServer(Socket sender)
        {
            // Release the socket
            if(sender != null)
            {
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
        }
        //TODO: Hacer el caso de cuando se desconecta o cierra el Server, preguntar si usar el mismo puerto e ip y si no lo desea pedirle nuevos
    }
}