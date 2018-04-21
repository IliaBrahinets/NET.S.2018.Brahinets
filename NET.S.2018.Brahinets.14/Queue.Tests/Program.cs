using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> queue = new Queue<int>(2);

            queue.Enqueue(1); queue.Enqueue(2);
            queue.Enqueue(5); queue.Enqueue(10);
            queue.Dequeue(); queue.Dequeue();
            queue.Enqueue(6); queue.Enqueue(7);
            queue.Enqueue(10); queue.Enqueue(11);

            Console.WriteLine(queue.Capacity);

            foreach (var item in queue)
            {
                Console.WriteLine(item);
            }

            while(queue.Count != 0)
            {
                Console.WriteLine(queue.Dequeue());
            }

            Console.ReadKey();
        }
    }
}
    