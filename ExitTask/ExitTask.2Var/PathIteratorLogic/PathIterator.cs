using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathIteratorLogic
{
    public class PathIterator
    {
        public static IEnumerable<Direction> GetWay(Direction[,] labyrinth, Point startPoint, Point endPoint)
        {
            Validation(labyrinth, startPoint);

            return WayIterator(labyrinth, startPoint, endPoint);
        }

        private static void Validation(Direction[,] labyrinth, Point startPoint)
        {

        }

        private static IEnumerable<Direction> WayIterator(Direction[,] labyrinth, Point startPoint, Point endPoint)
        {
            Point curr = startPoint;

            while (!curr.Equals(endPoint))
            {
                yield return labyrinth[curr.i, curr.j];

                curr = curr + NextShift(labyrinth[curr.i, curr.j]);
            }

            yield return labyrinth[endPoint.i, endPoint.j];
        }    

        private static Point NextShift(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Point(1, 0);
                case Direction.Down:
                    return new Point(-1, 0);
                case Direction.Left:
                    return new Point(0, -1);
                case Direction.Right:
                    return new Point(0, 1);
                default:
                    return new Point(0, 0);
            }
        }

    }
}
