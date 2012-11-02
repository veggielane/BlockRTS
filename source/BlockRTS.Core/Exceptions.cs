using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockRTS.Core
{
    public class test  : Exception
    {
        public test(string message)
            : base(message)
        {
        }
    }
}
