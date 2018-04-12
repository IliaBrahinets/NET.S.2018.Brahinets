using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

public class CountDownClock
{
    public TimeSpan Interval { get; }
    public TimeSpan RemainedTime { get; private set; }

    public bool IsRunning { get; private set; }

    public event EventHandler End = delegate { };

    private Timer Synchronizer;

    /// <summary>
    /// Initialize a instance, sets as the countDown interval the given interval.
    /// Clock's precision is up to seconds.
    /// </summary>
    /// <param name="interval">The countDown interval.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the interval is null.</exception>
    public CountDownClock(TimeSpan interval)
    {
        if(interval.Ticks < 0)
        {
            throw new ArgumentOutOfRangeException($"{nameof(interval)} can't be < 0");
        }

        Interval = interval;
        RemainedTime = interval;
        IsRunning = false;

        const double tickInterval = 1000;
        Synchronizer = new Timer(tickInterval);
        Synchronizer.Elapsed += Synchronizer_Elapsed;
    }

    /// <summary>
    /// Starting if it is not running the countDown clock.
    /// </summary>
    public void Start()
    {
        if (!IsRunning)
        {
            Synchronizer.Start();
            IsRunning = true;
        }
    }

    /// <summary>
    /// Return a string representation of the countDown clock.
    /// </summary>
    /// <returns>In format:"Interval:...,IsRunnung:...</returns>
    public override string ToString()
    {
        return $"Interval:{Interval}, IsRunning:{IsRunning}";
    }

    private void Synchronizer_Elapsed(object sender, ElapsedEventArgs e)
    {
        RemainedTime = RemainedTime.Subtract(new TimeSpan(0, 0, 0, 0, (int)Synchronizer.Interval));

        if(RemainedTime.Ticks < 0)
        {
            RemainedTime = TimeSpan.Zero;
            IsRunning = false;

            Synchronizer.Stop();
            Synchronizer.Dispose();

            OnEnd(new EventArgs());
        } 
    }

    protected virtual void OnEnd(EventArgs e)
    {
        End(this, e);
    }

}

