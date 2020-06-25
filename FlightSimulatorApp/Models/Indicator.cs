using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FlightSimulatorApp
{
    public class IndicatorCollection : ObservableCollection<Indicator>
    { 
    }

    public class Indicator
    {
        private string _name;
        private double _value;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public Indicator()
        {
        }

        public Indicator(string name, double value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}