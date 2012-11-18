using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockRTS.Core.Maths
{
    public class Ray
    {
        public Vect3 Start { get; private set; }
        public Vect3 End { get; private set; }
        public Ray(Vect3 start, Vect3 end)
        {
            Start = start;
            End = end;
        }
    }
}
