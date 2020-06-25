using System;

namespace Common.FlightSimulator.Constants
{
    public static class ViewConstants
    {
        public static string longitude_indicator = "/position/longitude-deg";

        public static string latitude_indicators = "/position/latitude-deg";

        public static string get_location_indicators = longitude_indicator + latitude_indicators;

        public static string[] get_indicators = new string[] {"get /instrumentation/heading-indicator/indicated-heading-deg\n",
                                                "get /instrumentation/gps/indicated-vertical-speed\n",
                                                "get /instrumentation/gps/indicated-ground-speed-kt\n",
                                                "get /instrumentation/airspeed-indicator/indicated-speed-kt\n",
                                                "get /instrumentation/gps/indicated-altitude-ft\n",
                                                "get /instrumentation/attitude-indicator/internal-roll-deg\n",
                                                "get /instrumentation/attitude-indicator/internal-pitch-deg\n",
                                                "get /instrumentation/altimeter/indicated-altitude-ft\n",
                                                "get " + longitude_indicator + "\n", "get " + latitude_indicators + "\n"};

        public static string control_command_rudder = "/controls/flight/rudder";

        public static string control_command_elevator = "/controls/flight/elevator";

        public static string control_command_throttle = "/controls/engines/current-engine/throttle";

        public static string control_command_aileron = "/controls/flight/aileron";

        public static string[] controls_names = new string[] { "/controls/engines/current-engine/throttle",
                                                              "/controls/flight/aileron",
                                                              "/controls/flight/elevator",
                                                              "/controls/flight/rudder"};

        public static string get_controls = $"get {control_command_rudder}\n" +
                                            $"get {control_command_elevator}\n" +
                                            $"get {control_command_throttle}\n" +
                                            $"get {control_command_aileron}\n";
        
        public static string gps_ground_speed = "gps_indicated-ground-speed-kt";

        public static string[] indicators_name = new string[] {"indicated-heading-deg", "gps_indicated-vertical-speed",
                                              "gps_indicated-ground-speed-kt", "airspeed-indicator_indicated-speed-kt",
                                              "gps_indicated-altitude-ft", "altitude-indicator_internal-roll-deg",
                                              "altitude-indicator_internal-pitch-deg", "altimeter_indicated-altitude-ft",
                                              "position-longitude-deg", "position_latitude-deg"};

        public static string[] _indicators = new string[] {"/instrumentation/heading-indicator/indicated-heading-deg",
                                                "/instrumentation/gps/indicated-vertical-speed",
                                                "/instrumentation/gps/indicated-ground-speed-kt",
                                                "/instrumentation/airspeed-indicator/indicated-speed-kt",
                                                "/instrumentation/gps/indicated-altitude-ft",
                                                "/instrumentation/attitude-indicator/internal-roll-deg",
                                                "/instrumentation/attitude-indicator/internal-pitch-deg",
                                                "/instrumentation/altimeter/indicated-altitude-ft",
                                                longitude_indicator, latitude_indicators };

    }
}
