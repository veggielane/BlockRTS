using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
    }
}
