using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Common.FlightSimulator.Constants;
using FlightSimulatorApp.Core.Managers;

namespace FlightSimulatorApp.Core.Managers
{
    public class ServerConnectionManager
    {
        public static Connection ConnectionToServer { get; set; } = new Connection();
        public static Socket Sender { get; set; }
        public static string SocketAnswer { get; set; }

        public static bool cancelConnection = false;

        public static Queue<KeyValuePair<int,double>> queue = new Queue<KeyValuePair<int, double>>();
        public static bool stopSets =  false;
        public static void SetCommandInServer(string command, double value)
        {
            bool erroConnnection = false;
            Task serverTask = Task.Run(() =>
            {

                try
                {
                    StablishedConnnectionToServer();
                    double d = Math.Truncate(value * 1000) / 1000;
                    string strToSend = d.ToString().Replace(',', '.');
                    string set_msg = $"set {command} {strToSend}\n";
                    string answer = $" {ConnectionToServer.sendMessage(Sender, set_msg)}";
                }
                catch(ServerConnectionManager.FlightSimulatorConnectionException)
                {
                    erroConnnection = true;
                }
               
            });
            serverTask.Wait();
            if (erroConnnection)
                throw new FlightSimulatorConnectionException("Error connection with Server\n");
        }

        public static void StablishedConnnectionToServer() {
            
            try
            { 
                if ((Sender == null || (Sender != null && Sender.Connected == false)) && !cancelConnection)
                    Sender = ConnectionToServer.openSocketToServer();
            }
            catch (SocketException)
            {
                throw new FlightSimulatorConnectionException("It wasn't able to connect to the Server\n");
            }
        }

       

        public static void GetIndicatorsFromServer(IndicatorCollection IndicatorList)
        {
            try
            {
                StablishedConnnectionToServer();
                NumberFormatInfo nfi = new NumberFormatInfo();
                nfi.NumberDecimalSeparator = ".";
                string answer;
                for (int i = 0; i < ViewConstants.get_indicators.Length; i++)
                {
                    answer = ConnectionToServer.sendEspecificMessageToGets(Sender, ViewConstants.get_indicators[i]);
                    try
                    {
                        double d = Convert.ToDouble(answer, nfi);
                        IndicatorList[i] = new Indicator() { Name = ViewConstants.indicators_name[i], Value = d };
                    }
                    catch(FormatException)
                    {
                        //In Case of ERR, do nothing
                    }
                }
            }
            catch (SocketException)
            {
                throw new FlightSimulatorConnectionException("It wasn't able to connect to the Server\n");
            }
        }

        [Serializable]
        public class FlightSimulatorConnectionException : Exception
        {
            public FlightSimulatorConnectionException()
            {
            }

            public FlightSimulatorConnectionException(string message) : base(message)
            {
            }

            public FlightSimulatorConnectionException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected FlightSimulatorConnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
}
