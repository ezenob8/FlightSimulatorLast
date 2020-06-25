using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.FlightSimulator.Constants;
using FlightSimulatorApp.Core.Managers;

namespace FlightSimulatorApp.Core.Managers
{
    /// <summary>
    /// This class manages the flight logic.
    /// @author $user Ezequiel Glocer
    /// </summary>
    public class FlightManager
    {
        public enum FlightCommand  { 
            Rudder, Elevator, Throttle, Aileron
        }
        public static void SetCommand(double value, FlightCommand commandType)
        {
            Mutex mutex = new Mutex();
            mutex.WaitOne();
            switch(commandType)
            {
                case FlightCommand.Rudder:
                    ServerConnectionManager.SetCommandInServer(ViewConstants.control_command_rudder, value);
                    break;
                case FlightCommand.Elevator:
                    ServerConnectionManager.SetCommandInServer(ViewConstants.control_command_elevator, value);
                    break;
                case FlightCommand.Throttle:
                    ServerConnectionManager.SetCommandInServer(ViewConstants.control_command_throttle, value);
                    break;
                case FlightCommand.Aileron:
                    ServerConnectionManager.SetCommandInServer(ViewConstants.control_command_aileron, value);
                    break;
            }
            mutex.ReleaseMutex();
        }

    }
}
