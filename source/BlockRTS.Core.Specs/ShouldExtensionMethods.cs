using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Machine.Specifications;
using Machine.Specifications.Utility.Internal;

namespace BlockRTS.Core.Specs
{
    public static class ShouldExtensionMethods
    {

        public static double ShouldNearlyEqual(this double actual, double expected, double epsilon = 2E-24)
        {

            if (actual.NearlyEquals(expected,epsilon))
                throw new SpecificationException(PrettyPrintingExtensions.FormatErrorMessage(actual, expected));
            return actual;
        }
    }
}
