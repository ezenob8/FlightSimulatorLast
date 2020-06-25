using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCustomControls;
using System.Windows.Controls;

namespace FlightSimulatorApp.Core.Utils
{
    public class MathGeometry
    {
        public static void CalculateAngleAndPositions(VirtualJoystickEventArgs args, out double PosX, out double PosY, out double ang)
        {
            double distance = Convert.ToDouble($"{args.Distance}");
            ang = Convert.ToDouble($"{args.Angle}");

            if (ang >= 0 && ang < 90)
            {
                ang = 270 + ang;
            }
            else if (ang >= 90 && ang <= 360)
            {
                ang -= 90;
            }

            double ang_rad = (ang * Math.PI) / 180.0;
            double cos_ang = Math.Cos(ang_rad);
            double sin_ang = Math.Sin(ang_rad);

            if (ang == 90)
            {
                cos_ang = 0;
                sin_ang = 1;
            }
            else if (ang == 180)
            {
                cos_ang = -1;
                sin_ang = 0;
            }
            else if (ang == 270)
            {
                cos_ang = 0;
                sin_ang = -1;
            }

            PosX = (cos_ang * distance) / 100;
            PosY = sin_ang;
        }

    }
}
