using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathIteratorLogic;

namespace PathIteratorTest
{
    class ToCommonFormatConverter : ICoverter<char[,], Direction[,]>
    {
        public Direction[,] Convert(char[,] input)
        {
            int row = input.GetLength(0);
            int collumn = input.GetLength(1);

            var output = new Direction[row, collumn];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < collumn; j++)
                {
                    Direction tmp;

                    switch (input[i, j])
                    {
                        case 'U':
                            tmp = Direction.Up;
                            break;
                        case 'D':
                            tmp = Direction.Down;
                            break;
                        case 'R':
                            tmp = Direction.Right;
                            break;
                        case 'L':
                            tmp = Direction.Left;
                            break;
                        default:
                            tmp = Direction.None;
                            break;
                    }

                    output[i, j] = tmp;
                }
            }

            return output;
        }
    }
}
