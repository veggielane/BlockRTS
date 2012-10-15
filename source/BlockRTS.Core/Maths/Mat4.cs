﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockRTS.Core.Maths
{
    public class Mat4
    {
        private readonly Double[,] _data;

        public Mat4(Double[,] data)
        {
            _data = data;
        }

        public Double this[int i, int j]
        {
            get
            {
                if (i < 1 || i > 4 || j < 1 || j > 4)
                    throw new ArgumentOutOfRangeException();
                return _data[i - 1, j - 1];
            }
            set
            {
                if (i < 1 || i > 4 || j < 1 || j > 4)
                    throw new ArgumentOutOfRangeException();
                _data[i - 1, j - 1] = value;
            }
        }

        public Double X
        {
            get { return this[1, 4]; }
            set { this[1, 4] = value; }
        }

        public Double Y
        {
            get { return this[2, 4]; }
            set { this[2, 4] = value; }
        }

        public Double Z
        {
            get { return this[3, 4]; }
            set { this[3, 4] = value; }
        }

        public Double LengthSquared
        {
            get { return Math.Pow(this[1, 4], 2.0) + Math.Pow(this[2, 4], 2.0) + Math.Pow(this[3, 4], 2.0); }
        }

        public Double Length
        {
            get { return Math.Sqrt(LengthSquared); }
        }

        public static Mat4 Identity
        {
            get
            {
                return new Mat4(new[,]
                    {
                        {1.0, 0.0, 0.0, 0.0},
                        {0.0, 1.0, 0.0, 0.0},
                        {0.0, 0.0, 1.0, 0.0},
                        {0.0, 0.0, 0.0, 1.0}
                    });
            }
        }

        public Mat4 Inverse()
        {
            throw new NotImplementedException();
        }

        public Mat4 Cofactor()
        {
            throw new NotImplementedException();
        }

        public Mat4 Adjoint()
        {
            throw new NotImplementedException();
        }

        public Double Determinant()
        {
            throw new NotImplementedException();
        }

        public Mat4 Add(Mat4 matrix)
        {
            return new Mat4(new[,]
                {
                    {
                        this[1, 1] + matrix[1, 1], this[1, 2] + matrix[1, 2], this[1, 3] + matrix[1, 3],
                        this[1, 4] + matrix[1, 4]
                    },
                    {
                        this[2, 1] + matrix[2, 1], this[2, 2] + matrix[2, 2], this[2, 3] + matrix[2, 3],
                        this[2, 4] + matrix[2, 4]
                    },
                    {
                        this[3, 1] + matrix[3, 1], this[3, 2] + matrix[3, 2], this[3, 3] + matrix[3, 3],
                        this[3, 4] + matrix[3, 4]
                    },
                    {
                        this[4, 1] + matrix[4, 1], this[4, 2] + matrix[4, 2], this[4, 3] + matrix[4, 3],
                        this[4, 4] + matrix[4, 4]
                    }
                });
        }

        public Mat4 Add(Double scalar)
        {
            return new Mat4(new[,]
                {
                    {this[1, 1] + scalar, this[1, 2] + scalar, this[1, 3] + scalar, this[1, 4] + scalar},
                    {this[2, 1] + scalar, this[2, 2] + scalar, this[2, 3] + scalar, this[2, 4] + scalar},
                    {this[3, 1] + scalar, this[3, 2] + scalar, this[3, 3] + scalar, this[3, 4] + scalar},
                    {this[4, 1] + scalar, this[4, 2] + scalar, this[4, 3] + scalar, this[4, 4] + scalar}
                });
        }

        public Mat4 Subtract(Mat4 matrix)
        {
            return new Mat4(new[,]
                {
                    {
                        this[1, 1] - matrix[1, 1], this[1, 2] - matrix[1, 2], this[1, 3] - matrix[1, 3],
                        this[1, 4] - matrix[1, 4]
                    },
                    {
                        this[2, 1] - matrix[2, 1], this[2, 2] - matrix[2, 2], this[2, 3] - matrix[2, 3],
                        this[2, 4] - matrix[2, 4]
                    },
                    {
                        this[3, 1] - matrix[3, 1], this[3, 2] - matrix[3, 2], this[3, 3] - matrix[3, 3],
                        this[3, 4] - matrix[3, 4]
                    },
                    {
                        this[4, 1] - matrix[4, 1], this[4, 2] - matrix[4, 2], this[4, 3] - matrix[4, 3],
                        this[4, 4] - matrix[4, 4]
                    }
                });
        }

        public Mat4 Subtract(Double scalar)
        {
            return new Mat4(new[,]
                {
                    {this[1, 1] - scalar, this[1, 2] - scalar, this[1, 3] - scalar, this[1, 4] - scalar},
                    {this[2, 1] - scalar, this[2, 2] - scalar, this[2, 3] - scalar, this[2, 4] - scalar},
                    {this[3, 1] - scalar, this[3, 2] - scalar, this[3, 3] - scalar, this[3, 4] - scalar},
                    {this[4, 1] - scalar, this[4, 2] - scalar, this[4, 3] - scalar, this[4, 4] - scalar}
                });
        }

        public Mat4 Multiply(Mat4 matrix)
        {
            return new Mat4(new[,]
                {
                    {
                        this[1, 1]*matrix[1, 1] + this[1, 2]*matrix[2, 1] + this[1, 3]*matrix[3, 1] +
                        this[1, 4]*matrix[4, 1],
                        this[1, 1]*matrix[1, 2] + this[1, 2]*matrix[2, 2] + this[1, 3]*matrix[3, 2] +
                        this[1, 4]*matrix[4, 2],
                        this[1, 1]*matrix[1, 3] + this[1, 2]*matrix[2, 3] + this[1, 3]*matrix[3, 3] +
                        this[1, 4]*matrix[4, 3],
                        this[1, 1]*matrix[1, 4] + this[1, 2]*matrix[2, 4] + this[1, 3]*matrix[3, 4] +
                        this[1, 4]*matrix[4, 4]
                    },
                    {
                        this[2, 1]*matrix[1, 1] + this[2, 2]*matrix[2, 1] + this[2, 3]*matrix[3, 1] +
                        this[2, 4]*matrix[4, 1],
                        this[2, 1]*matrix[1, 2] + this[2, 2]*matrix[2, 2] + this[2, 3]*matrix[3, 2] +
                        this[2, 4]*matrix[4, 2],
                        this[2, 1]*matrix[1, 3] + this[2, 2]*matrix[2, 3] + this[2, 3]*matrix[3, 3] +
                        this[2, 4]*matrix[4, 3],
                        this[2, 1]*matrix[1, 4] + this[2, 2]*matrix[2, 4] + this[2, 3]*matrix[3, 4] +
                        this[2, 4]*matrix[4, 4]
                    },
                    {
                        this[3, 1]*matrix[1, 1] + this[3, 2]*matrix[2, 1] + this[3, 3]*matrix[3, 1] +
                        this[3, 4]*matrix[4, 1],
                        this[3, 1]*matrix[1, 2] + this[3, 2]*matrix[2, 2] + this[3, 3]*matrix[3, 2] +
                        this[3, 4]*matrix[4, 2],
                        this[3, 1]*matrix[1, 3] + this[3, 2]*matrix[2, 3] + this[3, 3]*matrix[3, 3] +
                        this[3, 4]*matrix[4, 3],
                        this[3, 1]*matrix[1, 4] + this[3, 2]*matrix[2, 4] + this[3, 3]*matrix[3, 4] +
                        this[3, 4]*matrix[4, 4]
                    },
                    {
                        this[4, 1]*matrix[1, 1] + this[4, 2]*matrix[2, 1] + this[4, 3]*matrix[3, 1] +
                        this[4, 4]*matrix[4, 1],
                        this[4, 1]*matrix[1, 2] + this[4, 2]*matrix[2, 2] + this[4, 3]*matrix[3, 2] +
                        this[4, 4]*matrix[4, 2],
                        this[4, 1]*matrix[1, 3] + this[4, 2]*matrix[2, 3] + this[4, 3]*matrix[3, 3] +
                        this[4, 4]*matrix[4, 3],
                        this[4, 1]*matrix[1, 4] + this[4, 2]*matrix[2, 4] + this[4, 3]*matrix[3, 4] +
                        this[4, 4]*matrix[4, 4]
                    }
                });
        }

        public Mat4 Multiply(Double scalar)
        {
            return new Mat4(new[,]
                {
                    {this[1, 1]*scalar, this[1, 2]*scalar, this[1, 3]*scalar, this[1, 4]*scalar},
                    {this[2, 1]*scalar, this[2, 2]*scalar, this[2, 3]*scalar, this[2, 4]*scalar},
                    {this[3, 1]*scalar, this[3, 2]*scalar, this[3, 3]*scalar, this[3, 4]*scalar},
                    {this[4, 1]*scalar, this[4, 2]*scalar, this[4, 3]*scalar, this[4, 4]*scalar}
                });
        }


        public Mat4 Divide(Double scalar)
        {
            return Multiply(1.0/scalar);
        }

        public Mat4 Transpose()
        {
            return new Mat4(new[,]
                {
                    {this[1, 1], this[2, 1], this[3, 1], this[4, 1]},
                    {this[1, 2], this[2, 2], this[3, 2], this[4, 2]},
                    {this[1, 3], this[2, 3], this[3, 3], this[4, 3]},
                    {this[1, 4], this[2, 4], this[3, 4], this[4, 4]}
                });
        }

        public static Mat4 Translate(Double x, Double y, Double z)
        {
            return new Mat4(new[,]
                {
                    {1.0, 0.0, 0.0, x},
                    {0.0, 1.0, 0.0, y},
                    {0.0, 0.0, 1.0, z},
                    {0.0, 0.0, 0.0, 1.0}
                });
        }

        public static Mat4 Translate(Vect3 v)
        {
            return Translate(v.X, v.Y, v.Z);
        }

        public static Mat4 RotateX(Angle theta)
        {
            return new Mat4(new[,]
                {
                    {1.0, 0.0, 0.0, 0.0},
                    {0.0, Math.Cos(theta), -Math.Sin(theta), 0.0},
                    {0.0, Math.Sin(theta), Math.Cos(theta), 0.0},
                    {0.0, 0.0, 0.0, 1.0}
                });
        }

        public static Mat4 RotateY(Angle theta)
        {
            return new Mat4(new[,]
                {
                    {Math.Cos(theta), 0.0, Math.Sin(theta), 0.0},
                    {0.0, 1.0, 0.0, 0.0},
                    {-Math.Sin(theta), 0.0, Math.Cos(theta), 0.0},
                    {0.0, 0.0, 0.0, 1.0}
                });
        }

        public static Mat4 RotateZ(Angle theta)
        {
            return new Mat4(new[,]
                {
                    {Math.Cos(theta), -Math.Sin(theta), 0.0, 0.0},
                    {Math.Sin(theta), Math.Cos(theta), 0.0, 0.0},
                    {0.0, 0.0, 1.0, 0.0},
                    {0.0, 0.0, 0.0, 1.0}
                });
        }

        public override String ToString()
        {
            return "Matrix <4x4>:\n"
                   + "|" + this[1, 1] + "," + this[1, 2] + "," + this[1, 3] + "," + this[1, 4] + "|\n"
                   + "|" + this[2, 1] + "," + this[2, 2] + "," + this[2, 3] + "," + this[2, 4] + "|\n"
                   + "|" + this[3, 1] + "," + this[3, 2] + "," + this[3, 3] + "," + this[3, 4] + "|\n"
                   + "|" + this[4, 1] + "," + this[4, 2] + "," + this[4, 3] + "," + this[4, 4] + "|";
        }

        public Vect3 ToVect3()
        {
            return new Vect3(X,Y,Z);
        }

        public static Mat4 operator +(Mat4 m1, Mat4 m2)
        {
            return m1.Add(m2);
        }

        public static Mat4 operator +(Mat4 m, Double d)
        {
            return m.Add(d);
        }

        public static Mat4 operator -(Mat4 m1, Mat4 m2)
        {
            return m1.Subtract(m2);
        }

        public static Mat4 operator -(Mat4 m, Double d)
        {
            return m.Subtract(d);
        }

        public static Mat4 operator *(Mat4 m1, Mat4 m2)
        {
            return m1.Multiply(m2);
        }

        public static Mat4 operator *(Mat4 m, Double d)
        {
            return m.Multiply(d);
        }

        public static Mat4 operator *(Double d, Mat4 m)
        {
            return m.Multiply(d);
        }

        public static Mat4 operator /(Mat4 m, Double d)
        {
            return m.Divide(d);
        }

        public override int GetHashCode()
        {
            return (_data != null ? _data.GetHashCode() : 0);
        }


        public static Mat4 LookAt(Vect3 eye, Vect3 target, Vect3 up)
        {
            var vector31 = (eye - target).Normalize();
            var right = up.CrossProduct(vector31).Normalize();
            var vector32 = vector31.CrossProduct(right).Normalize();
            return new Mat4(new[,]
                {
                    {right.X, right.Y, right.Z, 0.0},
                    {vector32.X, vector32.Y, vector32.Z, 0.0},
                    {vector31.X, vector31.Y, vector31.Z, 0.0},
                    {0.0, 0.0, 0.0, 1.0}
                })*Translate(-eye);
        }

        public static Mat4 CreatePerspectiveFieldOfView(double fovy, double aspect, double zNear, double zFar)
        {
            if (fovy <= 0.0 || fovy > Math.PI)
                throw new ArgumentOutOfRangeException("fovy");
            if (aspect <= 0.0)
                throw new ArgumentOutOfRangeException("aspect");
            if (zNear <= 0.0)
                throw new ArgumentOutOfRangeException("zNear");
            if (zFar <= 0.0)
                throw new ArgumentOutOfRangeException("zFar");
            if (zNear >= zFar)
                throw new ArgumentOutOfRangeException("zNear");
            var top = zNear*Math.Tan(0.5*fovy);
            var bottom = -top;
            var left = bottom*aspect;
            var right = top*aspect;
            return new Mat4(new[,]
                {
                    {2.0*zNear/(right - left), 0.0, (right + left)/(right - left), 0.0},
                    {0.0, 2.0*zNear/(top - bottom), (top + bottom)/(top - bottom), 0.0},
                    {0.0, 0.0, -(zFar + zNear)/(zFar - zNear), -(2.0*zFar*zNear)/(zFar - zNear)},
                    {0.0, 0.0, -1, 0.0}
                });
        }
    }
}
