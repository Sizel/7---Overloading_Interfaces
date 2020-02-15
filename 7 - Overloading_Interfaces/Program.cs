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
            Angle angle1 = new Angle(30, 45, 61);
            Angle angle2 = new Angle(45, 30, 30);
            Console.WriteLine($"Angle1: { angle1 }");
            Console.WriteLine($"Angle2: { angle2 }");
            Angle sum = angle1 + angle2;
            Console.WriteLine($"A1 + A2 = { sum }");
            Angle diff = angle1 - angle2;
            Console.WriteLine($"A1 - A2 = { diff }");
            int scaleFactor = 2;
            Angle scaledAngle1 = angle1 * scaleFactor;
            Console.WriteLine($"angle1 * { scaleFactor } = { scaledAngle1 }");
            Angle angle4 = scaledAngle1 / scaleFactor;
            Console.WriteLine($"angle1 / { scaleFactor } = { angle4 }");
            Console.WriteLine();

            Angle angle3 = new Angle(45, 30, 30);
            Console.WriteLine($"Angle1 == angle2 ? : { angle1 == angle2 }");
            Console.WriteLine($"Angle2 == angle3 ? : { angle2 == angle3 }");
            Console.WriteLine($"Angle2 equals angle3 ? : {angle2.Equals(angle3) }");
            Console.WriteLine();
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
            Console.WriteLine("Angle2 consists of: ");
            foreach (var angleComponent in angle2)
            {
                Console.WriteLine(angleComponent);
            }
            #endregion
        }
    }

    class Angle : IComparable<Angle>
    {
        private int degrees;
        public int Degrees
        {
            get
            {
                return degrees;
            }
            set
            {
                degrees = value % 360;
            }
        }
        private int minutes;
        public int Minutes
        {
            get
            {
                return minutes;
            }
            set
            {
                minutes = value % 60;
                if (value > 60)
                {
                    degrees++;
                }

            }
        }
        private int seconds;
        public int Seconds
        {
            get
            {
                return seconds;
            }
            set
            {
                seconds = value % 60;
                if (value > 60)
                {
                    minutes++;
                }
            }
        }
        public Angle(int degrees, int minutes, int seconds)
        {
            Degrees = degrees;
            Minutes = minutes;
            Seconds = seconds;
        }
        public static Angle operator +(Angle lhs, Angle rhs)
        {
            int sumSeconds = lhs.Seconds + rhs.Seconds;
            int sumMinutes = lhs.Minutes + rhs.Minutes;
            int sumDegrees = lhs.Degrees + rhs.Degrees;

            Angle angle = new Angle(sumDegrees, sumMinutes, sumSeconds);

            return angle;
        }
        public static Angle operator -(Angle lhs, Angle rhs)
        {
            int deltaSeconds = lhs.Seconds - rhs.Seconds;
            int deltaMinutes = lhs.Minutes - rhs.Minutes;
            int deltaDegrees = lhs.Degrees - rhs.Degrees;

            if (lhs.Seconds < rhs.Seconds)
            {
                deltaSeconds = 60 + deltaSeconds;
                deltaMinutes--;
            }
            if (lhs.Minutes < rhs.Minutes)
            {
                deltaMinutes = 60 + deltaMinutes;
                deltaDegrees--;
            }
            if (lhs.Degrees < rhs.Degrees)
            {
                deltaDegrees = 360 + deltaDegrees;
            }

            return new Angle(deltaDegrees, deltaMinutes, deltaSeconds);
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
        public static Angle operator *(Angle angle, int scaleFactor)
        {
            int scaledSeconds = angle.Seconds * scaleFactor;
            int scaledMinutes = angle.Minutes * scaleFactor;
            int scaledDegrees = angle.Degrees * scaleFactor;

            return new Angle(scaledDegrees, scaledMinutes, scaledSeconds);
        }
        public static Angle operator /(Angle angle, int scaleFactor)
        {
            int scaledSeconds;
            int scaledMinutes;
            int scaledDegrees = angle.Degrees / scaleFactor;

            if (angle.Degrees % scaleFactor != 0)
            {
                scaledMinutes = (angle.Minutes + 60) / scaleFactor;
            }
            else
            {
                scaledMinutes = angle.Minutes / scaleFactor;
            }

            if (angle.Minutes % scaleFactor != 0)
            {
                scaledSeconds = (angle.Seconds + 60) / scaleFactor;
            }
            else
            {
                scaledSeconds = angle.Seconds / scaleFactor;
            }

            return new Angle(scaledDegrees, scaledMinutes, scaledSeconds);
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
            return $"Degrees: { Degrees }, Minutes: { Minutes }, Seconds: { Seconds }";
        }
        public int CompareTo(Angle angle)
        {
            return Degrees.CompareTo(angle.Degrees);
        }
        // Yield implimentation
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < 3; i++)
            {
                yield return this[i];
            }
        }
    }

    class AngleEnumerator : IEnumerator<int>
    {
        int position = -1;
        Angle angle;
        public AngleEnumerator(Angle angle)
        {
            this.angle = angle;
        }

        int IEnumerator<int>.Current
        {
            get
            {
                if (position < 0 || position >= 3)
                {
                    throw new InvalidOperationException();
                }
                return angle[position];
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();

        void IDisposable.Dispose()
        {

        }

        bool IEnumerator.MoveNext()
        {
            if (position < 2)
            {
                position++;
                return true;
            }
            else
            {
                return false;
            }
        }

        void IEnumerator.Reset()
        {
            position = -1;
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
