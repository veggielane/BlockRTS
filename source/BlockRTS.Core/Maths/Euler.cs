using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockRTS.Core.Maths
{
    public class Euler
    {
        public Angle Roll { get; private set; }
        public Angle Pitch { get; private set; }
        public Angle Yaw { get; private set; }

        public Euler(Angle roll, Angle pitch, Angle yaw)
        {
            Roll = roll;
            Pitch = pitch;
            Yaw = yaw;
        }
    }
}
