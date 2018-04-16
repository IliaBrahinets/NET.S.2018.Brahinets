using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

[TestFixture]
public class QueueTests
{
    public static TestContext TestContext { get; set; }

    [Test]
    public void TestMethod()
    {
        Queue<int> queue = new Queue<int>(2);

        queue.Enqueue(1); queue.Enqueue(2);
        queue.Enqueue(5); queue.Enqueue(10);
        queue.Dequeue(); queue.Dequeue();
        queue.Enqueue(6); queue.Enqueue(7);
        queue.Enqueue(10); queue.Enqueue(11);
        TestContext.WriteLine(queue.Capacity);
        foreach (var item in queue)
        {
            TestContext.WriteLine(item);
        }

        while(queue.Count != 0)
        {
            
        }

    }
}

