using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Maths;
using Machine.Specifications;
using Machine.Specifications.Model;

namespace BlockRTS.Core.Specs.Maths
{
    public class AngleSpecs
    {
        public abstract class AngleBase
        {
            protected static Angle angle;
            Establish c = () =>
            {
                angle = Angle.FromRadians(0);
            };
        }

        public class when_creating_angles
        {
            It can_be_created_from_zero_radians = () => Angle.FromRadians(0).ShouldEqual(new Angle());
            It can_be_created_from_zero_degrees = () => Angle.FromDegrees(0).ShouldEqual(new Angle());

            It can_be_created_from_180_degrees = () => Angle.FromDegrees(180).ShouldEqual(Angle.FromRadians(Math.PI));
        }

        public class when_testing_equality
        {
            It should_match_same_value = () => Angle.FromRadians(0).ShouldEqual(Angle.FromRadians(0));

            It should_match_same_value_when_using_static_methods = () => Angle.FromRadians(2 * Math.PI).ShouldEqual(Angle.FromRadians(2 * Math.PI));

            It should_match_same_value_when_using_different_static_methods = () => Angle.FromDegrees(0).Radians.ShouldEqual(Angle.FromRadians(0).Radians);
        }
        public class when_adding_two_angles
        {
            It should_return_the_expected_result = () => ((Angle.FromRadians(Math.PI) + Angle.FromRadians(Math.PI))).ShouldEqual(Angle.TwoPI);
        }

        public class when_adding_angles : AngleBase
        {
            private Because of = () => angle += Angle.FromDegrees(90);

            private It should_add_the_correct_value = () => angle.ShouldEqual(Angle.FromRadians(Math.PI/2.0));
        }

        public class when_subtrating_angles : AngleBase
        {
            private Because of = () => angle -= Angle.FromDegrees(90);

            private It should_subtrat_the_correct_value = () => angle.Radians.ShouldEqual(Angle.FromRadians(-Math.PI / 2.0));
        }

    }
}
