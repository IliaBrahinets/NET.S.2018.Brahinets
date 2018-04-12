using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class CountDownClockTests
{
    static void Main(string[] args)
    {
        Console.WriteLine("Number of seconds:");

        int numberOfSeconds = Int32.Parse(Console.ReadLine());
        CountDownClock countDown = new CountDownClock(new TimeSpan(0, 0, 0, numberOfSeconds));

        countDown.End += CountDown_End;
        countDown.Start();

        Console.WriteLine(countDown);

        Console.ReadKey();
    }

    private static void CountDown_End(object sender, EventArgs e)
    {
        Console.WriteLine("CountDown's event end fired");
        Console.WriteLine(sender);
    }
}

