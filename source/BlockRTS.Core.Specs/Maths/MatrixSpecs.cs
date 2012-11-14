using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Maths;
using Machine.Specifications;

namespace BlockRTS.Core.Specs.Maths
{

    public abstract class MatrixSpecs
    {
        protected static Mat4 m;
        Establish c = () =>
        {
            m = new Mat4(new[,]{
            {4.0,3.0,-2.0,2.0},
            {3.0,-1.0,0.0,4.0},
            {6.0,5.0,-3.0,0.0},
            {1.0,1.0,2.0,1.0}
            });
        };
    }

    public class when_testing_mat4_equality:MatrixSpecs
    {
        private It should_equal = () =>
            {
                var other = new Mat4(new[,]{{4.0,3.0,-2.0,2.0},{3.0,-1.0,0.0,4.0},{6.0,5.0,-3.0,0.0},{1.0,1.0,2.0,1.0}});
                m.ShouldEqual(other);
            };
    }

    public class when_inverting : MatrixSpecs
    {
        private static Mat4 inv;
        private Because of = () => inv = m.Inverse();
        private It should_return_the_expected_result = () => (new Mat4(new[,]{{-55.0, 27.0, 38.0, 2.0}, {51.0, -30.0, -22.0, 18.0}, {-25.0, 4.0, 9.0, 34.0},{54.0, -5.0, -34.0, 3.0}})/91.0).ShouldEqual(inv);
    }
}
