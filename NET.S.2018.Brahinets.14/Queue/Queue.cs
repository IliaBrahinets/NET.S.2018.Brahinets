using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Queue<T> : IEnumerable<T>
{
    private const int Default_Capacity = 8;

    //the concept of the circular buffer.
    private T[] items;
    private int head = -1;
    private int end = -1;

    private int version = int.MinValue;

    public int Capacity
    {
        get
        {
            return items.Length;
        }
    }
    public int Count
    {
        get
        {
            if(head == -1)
            {
                return 0;
            }

            if(end >= head)
            {
                return end - head + 1;
            }

            return Capacity - head + end + 1;
        }
    }

    /// <summary>
    /// Initialize a new instance of the queue, with the default capacity.
    /// </summary>
    public Queue()
    {
        items = new T[Default_Capacity];
    }

    /// <summary>
    /// Initialize a new instance of the queue, with the given capacity.
    /// </summary>
    public Queue(int capacity)
    {
        if(capacity < 0)
        {
            throw new ArgumentOutOfRangeException($"{nameof(capacity)} can't be <= 0");
        }

        items = new T[capacity];
    }

    /// <summary>
    /// Initialize a new instance of the queue, with the given collection.
    /// </summary>
    /// <param name="collection"></param>
    public Queue(IEnumerable<T> collection)
    {
        if(collection == null)
        {
            throw new ArgumentNullException($"{nameof(collection)} can't be null");
        }

        ICollection<T> tryICollection = collection as ICollection<T>;

        if (tryICollection != null)
        {
            items = new T[tryICollection.Count];
            tryICollection.CopyTo(items, 0);

            head = 0;
            end = items.Length - 1;
        }
        else
        {
            items = new T[Default_Capacity];
            foreach (T item in collection)
            {
                Enqueue(item);
            }
        }


    }

    /// <summary>
    /// Enqueue the item to a queue.
    /// </summary>
    /// <param name="item">The item to enqueue.</param>
    public void Enqueue(T item)
    {
        version++;

        if (Count != Capacity)
        {
            if (end == Capacity - 1)
            {
                end = 0;
                items[end] = item;
                return;
            }
        }
        else
        {
            Resize();
        }

        end++;

        if (head == -1)
        {
            head = end;
        }

        items[end] = item;

    }

    /// <summary>
    /// Dequeue an item from a queue.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when a queue is empty.</exception>
    /// <returns>Returns a dequeued item.</returns>
    public T Dequeue()
    {
        version++;

        if(Count == 0)
        {
            throw new InvalidOperationException("queue is empty");
        }

        if(head == end)
        {
            int ans = head;
            head = end = -1;
            return items[ans];
        }

        if(head == Capacity - 1)
        {
            head = 0;

            return items[Capacity - 1];
        }
        else
        {
            return items[head++];
        }
        
    }

    /// <summary>
    /// Returns value of the head item, without dequeuing it.
    /// </summary>
    public T Peek()
    {
        if(Count == 0)
        {
            throw new InvalidOperationException("queue is empty");
        }

        return items[head];
    }

    /// <summary>
    /// Makes a queue empty.
    /// </summary>
    public void Clear()
    {
        if (head < end)
        {
            Array.Clear(items, head, Count);
        }
        else
        {
            Array.Clear(items, head, Capacity - head);
            Array.Clear(items, 0, end + 1);
        }
    }

    /// <summary>
    /// Returns a IEnumerator for a queue.
    /// </summary>
    public Enumerator GetEnumerator()
    {
        return new Enumerator(this);
    }

    private void Resize()
    {
        int newLength = FloorPowerOf2(Capacity);
        T[] newItems = new T[newLength];

        Array.Copy(items, head, newItems, 0, Capacity - head);

        if(head != 0)
        {
            Array.Copy(items, 0, newItems, Capacity - head, end + 1);
        }

        end = Count - 1;
        head = 0;
        items = newItems;
        
    }

    private int FloorPowerOf2(int value)
    {
        int ans = 1;

        while(ans <= value)
        {
            ans *= 2;
        }

        return ans;
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<T>)(this)).GetEnumerator();
    }

    public struct Enumerator : IEnumerator<T>, IEnumerator
    {
        private readonly Queue<T> queue;
        private readonly int version;
        private int currIndex;
        private T currItem;
        
        public T Current
        {
            get
            {
                HandleVersion();

                if (currIndex == -2)
                {
                    throw new InvalidOperationException("Enumeration was not started.");
                }

                if(currIndex == -1)
                {
                    throw new InvalidOperationException("The end was reached.");
                }

                return currItem;
            }
        }
        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        public Enumerator(Queue<T> queue)
        {
            this.queue = queue;
            this.currIndex = -2;
            this.currItem = default(T);
            this.version = queue.version;
        }

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            HandleVersion();

            if (currIndex == -2)
            {
                currIndex = queue.head;

                if (currIndex == -1)
                {
                    return false;
                }

                currItem = queue.items[currIndex];
                return true;
            }

            if (currIndex == -1)
            {
                return false;
            }

            if (currIndex == queue.end)
            {
                currIndex = -1;
                return false;
            }

            if (currIndex == queue.Capacity - 1)
            {
                currIndex = 0;
            }
            else
            {
                currIndex++;
            }

            currItem = queue.items[currIndex];

            return true;
        }

        private void HandleVersion()
        {
            if(version != queue.version)
            {
                throw new InvalidOperationException("Queue was changed");
            }
        }

        public void Reset()
        {
            HandleVersion();
            currIndex = queue.head;
        }

        
    }

}

