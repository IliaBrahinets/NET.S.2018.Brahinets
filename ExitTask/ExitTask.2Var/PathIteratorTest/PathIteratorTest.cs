using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathIteratorLogic;

namespace PathIteratorTest
{
    class Program
    {
        static char[,] rawlabyrinth = new char[,]
        {
            {'U',' ',' ',' ',' ',' '},
            {'U',' ',' ',' ',' ',' '},
            {'U',' ',' ',' ','R','U'},
            {'U',' ',' ','R','D','U'},
            {'U',' ',' ','D',' ','U'},
            {'R','R','R','D',' ','U'},
        };

        static void Main(string[] args)
        {
            Point start = new Point(0, 0);
            Point end = new Point(5, 5);

            Direction[,] inputlabyrinth = new ToCommonFormatConverter().Convert(rawlabyrinth);

            IEnumerable<Direction> way = PathIterator.GetWay(inputlabyrinth, start, end);

            foreach (var step in way)
            {
                Console.WriteLine(step);
            }

            Console.ReadKey();
        }
    }
}
