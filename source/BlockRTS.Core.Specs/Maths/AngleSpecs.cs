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
    public abstract class AngleSpecs
    {
        protected static Angle angle;
        Establish c = () =>
        {
            angle = Angle.FromRadians(0);
        };
    }

    public class when_creating_an_angle
    {
        It can_be_created_from_radians = () => Angle.FromRadians(0).ShouldEqual(new Angle());
        It can_be_created_from_degrees = () => Angle.FromDegrees(0).ShouldEqual(new Angle());
    }

    public class when_testing_equality
    {
        It should_match_same_value = () => new Angle().ShouldEqual(new Angle());
        It should_match_same_value_when_using_static_methods = () => Angle.FromRadians(2 * Math.PI).ShouldEqual(Angle.FromRadians(2 * Math.PI));
        It should_match_same_value_when_using_different_static_methods = () => Angle.FromDegrees(0).ShouldEqual(Angle.FromRadians(0));

    }

    public class when_adding_two_angles
    {
        private It should_return_the_expected_result = () => ((double) (Angle.FromRadians(Math.PI) + Angle.FromRadians(Math.PI))).ShouldNearlyEqual(Angle.TwoPI, Double.Epsilon);
    }


    public class when_adding_angles:AngleSpecs
    {
        private Because of = () => angle += Angle.FromDegrees(0);

    }
}
