using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stride.Core.Mathematics
{
    public struct Int2 : IEquatable<Int2>, IFormattable
    {
        //public static readonly int SizeInBytes = Utilities.SizeOf<Int2>();

        public static readonly Int2 Zero = new Int2();

        public static readonly Int2 UnitX = new Int2(1, 0);

        public static readonly Int2 UnitY = new Int2(0, 1);

        public static readonly Int2 One = new Int2(1, 1);

        public int X;

        public int Y;

        public Int2(int value)
        {
            X = value;
            Y = value;
        }

        public Int2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return X;
                    case 1: return Y;
                }
                throw new ArgumentOutOfRangeException("index", "Indices for Int2 run from 0 to 1, inclusive.");
            }

            set
            {
                switch(index)
                {
                    case 0: X = value; break;
                    case 1: Y = value; break;
                    default: throw new ArgumentOutOfRangeException("index", "Indices for Int2 run from 0 to 1, inclusive.");
                }
            }
        }

        public int Length()
        {
            return (int)Math.Sqrt((X * X) + (Y * Y));
        }

        public int LengthSquared()
        {
            return (X * X) + (Y * Y);
        }

        public int[] ToArray()
        {
            return new int[] { X, Y };
        }

        public static void Add(ref Int2 left, ref Int2 right, out Int2 result)
        {
            result = new Int2(left.X + right.X, left.Y + right.Y);
        }

        public static Int2 Add(Int2 left, Int2 right)
        {
            return new Int2(left.X + right.X, left.Y + right.Y);
        }

        public static void Subtract(ref Int2 left, ref Int2 right, out Int2 result)
        {
            result = new Int2(left.X - right.X, left.Y - right.Y);
        }

        public static Int2 Subtract(Int2 left, Int2 right)
        {
            return new Int2(left.X - right.X, left.Y - right.Y);
        }

        public static void Multiply(ref Int2 value, int scale, out Int2 result)
        {
            result = new Int2(value.X * scale, value.Y * scale);
        }

        public static Int2 Multiply(Int2 value, int scale)
        {
            return new Int2(value.X * scale, value.Y * scale);
        }

        public static void Divide(ref Int2 value, int scale, out Int2 result)
        {
            result = new Int2(value.X / scale, value.Y / scale);
        }

        public static Int2 Divide(Int2 value, int scale)
        {
            return new Int2(value.X / scale, value.Y / scale);
        }

        public bool Equals(Int2 other)
        {
            return ((float)Math.Abs(other.X - X) < MathUtil.ZeroTolerance &&
                (float)Math.Abs(other.Y - Y) < MathUtil.ZeroTolerance); 
        }

        public override bool Equals(object value)
        {
            if (value == null)
                return false;

            if (value.GetType() != GetType())
                return false;

            return Equals((Int2)value);
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "X:{0} Y:{1}", X, Y);
        }

        public string ToString(string format)
        {
            if (format == null)
                return ToString();

            return string.Format(CultureInfo.CurrentCulture, "X:{0} Y:{1}", X.ToString(format, CultureInfo.CurrentCulture), Y.ToString(format, CultureInfo.CurrentCulture));
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, "X:{0} Y:{1}", X, Y);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
                return ToString(formatProvider);

            return string.Format(formatProvider, "X:{0} Y:{1}", X.ToString(format, formatProvider), Y.ToString(format, formatProvider));
        }
    }
}
