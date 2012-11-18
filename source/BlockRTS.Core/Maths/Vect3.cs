using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockRTS.Core.Maths
{
    public class Vect2
    {
        public Vect2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; private set; }
        public double Y { get; private set; }

        public static Vect2 Zero
        {
            get { return new Vect2(0.0, 0.0); }
        }

    }

    public class Vect4
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }
        public double A { get; private set; }

        public Vect4(double x, double y, double z, double a)
        {
            X = x;
            Y = y;
            Z = z;
            A = a;
        }

        public static Vect4 Zero
        {
            get { return new Vect4(0, 0, 0, 0); }
        }

        public Vect3 ToVec3()
        {
            return new Vect3(X,Y,Z);
        }
    }

    public class Vect3
    {
        public Vect3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }

        public double Length
        {
            get { return Math.Sqrt(LengthSquared); }
        }

        public double LengthSquared
        {
            get { return X * X + Y * Y + Z * Z; }
        }


        public Vect3 Normalize()
        {
            var num = 1f / Length;
            return new Vect3(X * num, Y * num, Z * num);
        }


        public static Vect3 Zero
        {
            get { return new Vect3(0.0, 0.0, 0.0); }
        }

        public static Vect3 UnitX
        {
            get { return new Vect3(1.0, 0.0, 0.0); }
        }

        public static Vect3 UnitY
        {
            get { return new Vect3(0.0, 1.0, 0.0); }
        }

        public static Vect3 UnitZ
        {
            get { return new Vect3(0.0, 0.0, 1.0); }
        }

        public Vect3 CrossProduct(Vect3 v)
        {
            return new Vect3(Y * v.Z - Z * v.Y, Z * v.X - X * v.Z, X * v.Y - Y * v.X);
        }

        public double DotProduct(Vect3 v)
        {
            return X * v.X + Y * v.Y + Z * v.Z;
        }

        public static Vect3 operator +(Vect3 v1, Vect3 v2)
        {
            return new Vect3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vect3 operator -(Vect3 v1, Vect3 v2)
        {
            return new Vect3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
        public static Vect3 operator -(Vect3 v)
        {
            return new Vect3(-v.X, -v.Y, -v.Z);
        }

        public static Vect3 operator *(Vect3 v, double d)
        {
            return new Vect3(v.X * d, v.Y * d, v.Z * d);
        }

        public static Vect3 operator *(double d, Vect3 v)
        {
            return v * d;
        }

        public static Vect3 operator /(Vect3 v, double d)
        {
            return new Vect3(v.X / d, v.Y / d, v.Z / d);
        }

        public static Vect3 operator /(double d, Vect3 v)
        {
            return v / d;
        }

        public override string ToString()
        {
            return "Vector3({0},{1},{2})".Fmt(X, Y, Z);
        }

        public Mat4 ToMat4()
        {
            return Mat4.Translate(this);
        }
    }
}
