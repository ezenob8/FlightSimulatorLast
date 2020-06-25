using FlightSimulatorApp.Core.Managers;
using FlightSimulatorApp.Core.Utils;
using FlightSimulatorApp.ViewModels;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Configuration;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfCustomControls;

namespace FlightSimulatorApp
{
    public partial class Interface : Window
    {
        GeneralViewModel GeneralVM;
        public Interface()
        {
            string cred_str;
            InitializeMap(out cred_str);
            InitializeComponent();
            AddPropertiesToMap(cred_str);
            InitializeViewModelContext();
            InitializeTimer();
            SuscribeEvents();
        }

        private void InitializeTimer()
        {
            TimerManager.Start(new EventHandler((Object myObject, EventArgs myEventArgs) => { TimerEventProcessor(GeneralVM, EventArgs.Empty); Refreshmap(); }));
        }

        private void Refreshmap()
        {
            myMap.Center = GeneralVM.MapLocation;
        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            try
            {
                TimerManager.Timer.Stop();
                //sending gets
                ((GeneralViewModel)myObject).GetIndicators();
                //waiting for answer
                TimerManager.Timer.Enabled = true;
            }
            catch (ServerConnectionManager.FlightSimulatorConnectionException)
            {
                ((GeneralViewModel)myObject).Disconnected = true;
            }
        }

        private void SuscribeEvents()
        {
            OnScreenJoystick.Moved += VirtualJoystick_Moved;
        }

        private void InitializeViewModelContext()
        {
            GeneralVM = new GeneralViewModel();
            DataContext = GeneralVM;
        }

        private void AddPropertiesToMap(string cred_str)
        {
            myMap.CredentialsProvider = new ApplicationIdCredentialsProvider(cred_str);
        }

        private static void InitializeMap(out string cred_str)
        {
            cred_str = ConfigurationManager.AppSettings.Get("MAP_cred");
        }

        private void EXIT_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void VirtualJoystick_Moved(object sender, WpfCustomControls.VirtualJoystickEventArgs args)
        {
            double PosX, PosY, ang;
            MathGeometry.CalculateAngleAndPositions(args, out PosX, out PosY, out ang);
            label_rudder.Content = string.Format(ConfigurationManager.AppSettings["labelRudderFormat"], PosX, ang);
            label_elevator.Content = string.Format(ConfigurationManager.AppSettings["labelElevatorFormat"], PosY, ang);
            try
            {
                GeneralVM.SetRudder(PosX);
                GeneralVM.SetElevator(PosY);
            }
            catch(ServerConnectionManager.FlightSimulatorConnectionException)
            {
                GeneralVM.Disconnected = true;
            }
        }

        private void ShowMessageReconnect()
        {
            DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Reconnect?", "Disconnection detected. Reopen Server.", MessageBoxButtons.YesNo);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                ServerConnectionManager.ConnectionToServer.closeSocketWithServer(ServerConnectionManager.Sender);
                ServerConnectionManager.cancelConnection = false;
                GeneralVM.Disconnected = false;
            }
            else if (dialogResult == System.Windows.Forms.DialogResult.No)
            {
                ServerConnectionManager.cancelConnection = false;
                EXIT_Click(null, null);
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var myForm = new NetSettings();
            myForm.Text = "Network Settings";
            myForm.StartPosition = FormStartPosition.CenterScreen;
            myForm.Show();
        }

        private void throttle_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            label_slider_throttle.Content = string.Format(ConfigurationManager.AppSettings["labelThrottleFormat"], e.NewValue);

            try
            {
                GeneralVM.SetAileron(e.NewValue);
            }
            catch (ServerConnectionManager.FlightSimulatorConnectionException)
            {
                GeneralVM.Disconnected = true;
            }
        }
        private void aileron_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            label_slider_aileron.Content = string.Format(ConfigurationManager.AppSettings["labelAileronFormat"], e.NewValue);
            try
            {
                GeneralVM.SetAileron(e.NewValue);
            }
            catch (ServerConnectionManager.FlightSimulatorConnectionException)
            {
                GeneralVM.Disconnected = true;
            }
        }

        private void chkDisconnected_Checked(object sender, RoutedEventArgs e)
        {
            ServerConnectionManager.cancelConnection = true;
            //Message BOX Yes or No
            if (ServerConnectionManager.cancelConnection == true)
            {
                ShowMessageReconnect();
            }
        }
    }
}
