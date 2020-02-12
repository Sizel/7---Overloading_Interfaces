using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace _7___Overloading_Interfaces
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Operations with angles
            Angle angle1 = new Angle(90, 0, 0);
            Angle angle2 = new Angle(45, 30, 30);
            Angle resAngle = angle1 + angle2;
            Console.WriteLine(resAngle);
            Angle angle3 = new Angle(45, 30, 30);
            Console.WriteLine(angle1 == angle2);
            Console.WriteLine(angle2 == angle3);
            Console.WriteLine(angle2.Equals(angle3));
            #endregion
            #region Sorting 
            Angle[] angles = {
                              new Angle(40, 30, 40),
                              new Angle(20, 50, 30),
                              new Angle(15, 40, 90)
                             };
            Console.WriteLine("Before sorting");
            foreach (var angle in angles)
            {
                Console.WriteLine(angle);
            }
            //Array.Sort(angles, new CompareByDegrees());
            //Array.Sort(angles, new CompareByMinutes());
            Array.Sort(angles, new CompareBySeconds());
            Console.WriteLine("After sorting");
            foreach (var angle in angles)
            {
                Console.WriteLine(angle);
            }
            #endregion
            #region Ienumerable and Ienumerator

            #endregion
        }
    }

    class Angle : IComparable<Angle>, IEnumerable<Angle>
    {
        public int Degrees { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        public Angle(int degrees, int minutes, int seconds)
        {
            Degrees = degrees;
            Minutes = minutes;
            Seconds = seconds;
        }

        public static Angle operator +(Angle lhs, Angle rhs)
        {
            Angle angle = new Angle(lhs.Degrees + rhs.Degrees, lhs.Minutes + rhs.Minutes, lhs.Seconds + rhs.Seconds);

            return angle;
        }

        public static bool operator ==(Angle lhs, Angle rhs)
        {
            if (lhs.Minutes == rhs.Minutes && lhs.Degrees == rhs.Degrees && lhs.Seconds == rhs.Seconds)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool Equals(Object o)
        {
            return this == (Angle)o;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator !=(Angle lhs, Angle rhs)
        {
            return !(lhs == rhs);
        }
        public int this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return Degrees;
                    case 1:
                        return Minutes;
                    case 2:
                        return Seconds;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (i)
                {
                    case 0:
                        Degrees = value;
                        break;
                    case 1:
                        Minutes = value;
                        break;
                    case 2:
                        Seconds = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }
        public override string ToString()
        {
            return String.Format($"Degrees: { Degrees }, Minutes: { Minutes }, Seconds: { Seconds }");
        }
        public int CompareTo(Angle angle)
        {
            return Degrees.CompareTo(angle.Degrees);
        }

        public IEnumerator<Angle> GetEnumerator()
        {
            return new AngleEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    class AngleEnumerator : IEnumerator<Angle>
    {
        Angle IEnumerator<Angle>.Current => throw new NotImplementedException();

        object IEnumerator.Current => throw new NotImplementedException();

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        bool IEnumerator.MoveNext()
        {
            throw new NotImplementedException();
        }

        void IEnumerator.Reset()
        {
            throw new NotImplementedException();
        }
    }
    class CompareByDegrees : IComparer<Angle>
    {
        public int Compare(Angle x, Angle y)
        {
            if (x.Degrees > y.Degrees)
            {
                return 1;
            }
            else if (x.Degrees < y.Degrees)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }

    class CompareByMinutes : IComparer<Angle>
    {
        public int Compare(Angle x, Angle y)
        {
            if (x.Minutes > y.Minutes)
            {
                return 1;
            }
            else if (x.Minutes < y.Minutes)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }

    class CompareBySeconds : IComparer<Angle>
    {
        public int Compare(Angle x, Angle y)
        {
            if (x.Seconds > y.Seconds)
            {
                return 1;
            }
            else if (x.Seconds < y.Seconds)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
