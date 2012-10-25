using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Maths;


namespace BlockRTS.Core
{
    public static class Extensions
    {
        public static IObservable<T> SubSample<T>(this IObservable<T> source, int interval)
        {
            var i = -1;
            return source.Where(o =>
            {
                i = (i + 1) % interval;
                return i == 0;
            });
        }

        public static string Fmt(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
        public static double NextDouble(this Random rnd, double min, double max)
        {
            return (max - min) * rnd.NextDouble() + min;
        }

        public static Quat ToQuat(this Euler e)
        {
            var sinp = Math.Sin(e.Pitch.Radians / 2.0);
            var siny = Math.Sin(e.Yaw.Radians / 2.0);
            var sinr = Math.Sin(e.Roll.Radians / 2.0);
            var cosp = Math.Cos(e.Pitch.Radians / 2.0);
            var cosy = Math.Cos(e.Yaw.Radians / 2.0);
            var cosr = Math.Cos(e.Roll.Radians / 2.0);
            return new Quat(sinr * cosp * cosy - cosr * sinp * siny, cosr * sinp * cosy + sinr * cosp * siny, cosr * cosp * siny - sinr * sinp * cosy, cosr * cosp * cosy + sinr * sinp * siny);
        }
    }
}
