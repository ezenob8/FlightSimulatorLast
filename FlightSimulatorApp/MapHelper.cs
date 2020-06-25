using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Helpers
{
    public class MapHelper:ContentControl
    {
        public static readonly DependencyProperty CenterProperty = DependencyProperty.RegisterAttached(
            "Center",
            typeof(Location),
            typeof(MapHelper),
            new PropertyMetadata(0.0, new PropertyChangedCallback(CenterChanged))
        );

        public static void SetCenter(DependencyObject obj, Location value)
        {
            obj.SetValue(CenterProperty, value);
        }

        public static Location GetCenter(DependencyObject obj)
        {
            return (Location)obj.GetValue(CenterProperty);
        }

        private static void CenterChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            Map map = (Map)obj;
            if (map != null)
                map.Center = (Location)args.NewValue;
        }
    }
}
