using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows;
using System.Threading;
using System.Globalization;
using System.Net.Sockets;
using FlightSimulatorApp.Core.Managers;
using Common.FlightSimulator.Constants;
using Microsoft.Maps.MapControl.WPF;
using System.Configuration;

namespace FlightSimulatorApp.ViewModels
{
    public class GeneralViewModel : IGeneric
    {
        #region variables definition
        //CONTROLS =================================================================
        private ControlCollection _controllist = new ControlCollection();
        //==========================================================================
        private double _rudderValue;
        private double _elevatorValue;
        private double _throttleValue;
        private double _aileronValue;
        private string _indicatorsText;
        private string _socketAnswer;
        //INDICATORS  =================================================================
        public IndicatorCollection _indicatorlist = new IndicatorCollection();
        //=============================================================================
        private LocationCollection polygonLocations;
        private Location mapLocation;
        private bool _disconnected;
        #endregion

        #region properties definition
        public ControlCollection ControlList
        {
            get { return _controllist; }
            set
            {
                _controllist = value;
                RaisePropertyChanged("ControlList");
            }
        }
        public double RudderValue
        {
            get { return _rudderValue; }
            set
            {
                _rudderValue = value;
                RaisePropertyChanged("RudderValue");
                SetRudder(value);
            }
        }
        public double ElevatorValue
        {
            get { return _elevatorValue; }
            set
            {
                _elevatorValue = value;
                RaisePropertyChanged("ElevatorValue");
                SetElevator(value);
            }
        }
        public double ThrottleValue
        {
            get { return _throttleValue; }
            set
            {
                _throttleValue = value;
                RaisePropertyChanged("ThrottleValue");
                SetThrottle(value);
            }
        }
        public double AileronValue
        {
            get { return _aileronValue; }
            set
            {
                _aileronValue = value;
                RaisePropertyChanged("AileronValue");
                SetAileron(value);
            }
        }
        public IndicatorCollection IndicatorList
        {
            get { return _indicatorlist; }
            set
            {
                _indicatorlist = value;
                RaisePropertyChanged("IndicatorList");
            }
        }
        public string IndicatorsText
        {
            get { return _indicatorsText; }
            set
            {
                _indicatorsText = value;
                RaisePropertyChanged("IndicatorsText");
            }
        }
        public string SocketAnswer
        {
            get { return _socketAnswer; }
            set
            {
                _socketAnswer = value;
                RaisePropertyChanged("SocketAnswer");
            }
        }
        public LocationCollection PolygonLocations
        {
            get { return polygonLocations; }
            set
            {
                polygonLocations = value;
                RaisePropertyChanged("PolygonLocations");
            }
        }
        public Location MapLocation
        {
            get { return mapLocation; }
            set
            {
                mapLocation = value;
                RaisePropertyChanged("MapLocation");
            }
        }
        public bool Disconnected
        {
            get { return _disconnected; }
            set
            {
                _disconnected = value;
                RaisePropertyChanged("Disconnected");
            }
        }
        #endregion

        #region properties_methods definition
        void ControlCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("ControlList");
        }
        public void SetRudder(object value)
        {
            FlightManager.SetCommand((double)value, FlightManager.FlightCommand.Rudder);
        }

        /// <summary>
        /// This method sets the elevator value in the server
        /// </summary>
        /// <param name="value"> value is the elevator value </param>
        public void SetElevator(object value)
        {
            FlightManager.SetCommand((double)value, FlightManager.FlightCommand.Elevator);
        }
        public void SetThrottle(object value)
        {
           FlightManager.SetCommand((double)value, FlightManager.FlightCommand.Throttle);
        }
        public void SetAileron(object value)
        {
           FlightManager.SetCommand((double)value, FlightManager.FlightCommand.Aileron);
        }

        /*Function to create a string from IndicatorList (ObservableCollection)*/
        private void CreateIndicatorsText()
        {
            IndicatorsText = "";
            foreach (var pair in IndicatorList)
            {
                IndicatorsText += pair.Name + ": " + pair.Value + "\n";
            }
            IndicatorsText += "\n";
        }

        void IndicatorCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("IndicatorList");
        }
        public void GetIndicators()
        {
            try
            {
                ServerConnectionManager.GetIndicatorsFromServer(IndicatorList);
                CreateIndicatorsText();
                UpdateMap();
            }
            catch(ServerConnectionManager.FlightSimulatorConnectionException)
            {
                Disconnected = true;
            }
        }
        public static double init = 34.872770;
        private void UpdateMap()
        {
            double indicator_long_val = 0.0, indicator_lat_val = 0.0;

            for (int i = 0; i < ViewConstants._indicators.Length; i++)
            {
                if (ViewConstants.longitude_indicator == ViewConstants._indicators[i])
                {
                    indicator_long_val = IndicatorList[i].Value;
                }
                if (ViewConstants.latitude_indicators == ViewConstants._indicators[i])
                {
                   indicator_lat_val = IndicatorList[i].Value;
                }
            }
            PolygonLocations = new LocationCollection() {
                                   new Location(indicator_lat_val - 0.25, indicator_long_val),
                                   new Location(indicator_lat_val + 0.25, indicator_long_val),
                                   new Location(indicator_lat_val + 0.25, indicator_long_val + 0.5),
                                   new Location(indicator_lat_val - 0.25, indicator_long_val + 0.5)};
            Location newLocation = new Location(indicator_lat_val, indicator_long_val);
            MapLocation = newLocation;
        }
        #endregion

        public GeneralViewModel()
        {
            InitializeControlList();
            InitializeIndicatorList();
            InitializeLocation();
            CreateIndicatorsText();
        }

        private void InitializeLocation()
        {
            double BenGuiron_lat = Convert.ToDouble(ConfigurationManager.AppSettings.Get("BENGUIRON_latitude"));
            double BenGuiron_long = Convert.ToDouble(ConfigurationManager.AppSettings.Get("BENGUIRON_longitude"));
            Location BenGuiron_location = new Location(BenGuiron_lat, BenGuiron_long);
            PolygonLocations = new LocationCollection() {
                                   new Location(BenGuiron_lat - 0.25, BenGuiron_long),
                                   new Location(BenGuiron_lat + 0.25, BenGuiron_long),
                                   new Location(BenGuiron_lat + 0.25, BenGuiron_long + 0.5),
                                   new Location(BenGuiron_lat - 0.25, BenGuiron_long + 0.5)};
            Location newLocation = new Location(BenGuiron_lat, BenGuiron_long);
            MapLocation = BenGuiron_location;
        }
        private void InitializeIndicatorList()
        {
            int howmanyIndicators = ViewConstants.indicators_name.Count();
            IndicatorList = new IndicatorCollection();
            for (int i = 0; i < howmanyIndicators; i++)
            {
                IndicatorList.Add(new Indicator() { Name = ViewConstants.indicators_name[i], Value = 0 });
            }
            IndicatorList.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(IndicatorCollectionChanged);
        }

        private void InitializeControlList()
        {
            
            int howmanyControls = ViewConstants.controls_names.Count();
            ControlList = new ControlCollection();
            for (int i = 0; i < howmanyControls; i++)
            {
                ControlList.Add(new Control() { Name = ViewConstants.controls_names[i], Value = 0 });
            }
            ControlList.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(ControlCollectionChanged);
        }
    }
}