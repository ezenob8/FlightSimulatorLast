using FlightSimulatorApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlightSimulatorApp.Core.Managers
{
    public class TimerManager
    {
        public static Timer Timer = new Timer();
    
        // This is the method to run when the timer is raised.
        private static void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            Timer.Stop();

            try
            {
                //paras los sets
                ServerConnectionManager.stopSets = true;
                //envias los gets
                ((GeneralViewModel)myObject).GetIndicators();
                //esperas respuesta
                Timer.Enabled = true;
                ServerConnectionManager.stopSets = false;
            }
            catch(ServerConnectionManager.FlightSimulatorConnectionException)
            {
                ((GeneralViewModel)myObject).Disconnected = true;

            }


        }
        public static void Start(EventHandler eh, int interval = 250) //5000 antes
        {
            /* Adds the event and the event handler for the method that will 
          process the timer event to the timer. */
            Timer.Tick += eh;

            // Sets the timer interval to 5 seconds.
            Timer.Interval = interval;
            Timer.Start();
        }
    }
}
