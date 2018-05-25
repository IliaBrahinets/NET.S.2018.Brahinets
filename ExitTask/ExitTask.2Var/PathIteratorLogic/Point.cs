using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathIteratorLogic
{
    public struct Point:IEquatable<Point>
    {
        public int i;
        public int j;

        public Point(int i, int j)
        {
            this.i = i;
            this.j = j;
        }

        public override bool Equals(object obj)
        {
            if(obj.GetType() != typeof(Point))
            {
                return false;
            }
            
            Point other = (Point)obj;

            return Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = -118560031;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + i.GetHashCode();
            hashCode = hashCode * -1521134295 + j.GetHashCode();
            return hashCode;
        }

        public bool Equals(Point other)
        {
            return other.i == i && other.j == j;
        }


        public static Point operator+(Point left, Point right)
        {
            return Add(left, right);
        }

        public static Point Add(Point left, Point right)
        {
            return new Point(left.i + right.i, left.j + right.j);
        }

        public static Point operator-(Point left, Point right)
        {
            return Substract(left, right);
        }

        public static Point Substract(Point left, Point right)
        {
            return new Point(left.i - right.i, left.j - right.j);
        }

    }
}
