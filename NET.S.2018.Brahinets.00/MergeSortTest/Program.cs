using System;
using System.Collections.Generic;
using IListExtensions;

namespace MergeSortTest
{
    public class Program
    {   
        public static int ReverseCompare(int x, int y)
        {
            return y - x;
        }

        public static void RandFilling(List<int> array, int count, int maxValue)
        {
            Random randomGen = new Random(DateTime.Now.GetHashCode());

            for (int i = 0; i < count; i++)
            {
                array.Add(randomGen.Next() % maxValue);
            }
        }

        public static void PrintArray(List<int> array)
        {
            for (int i = 0; i < array.Count; i++)
            {
                Console.Write(array[i].ToString() + " ");
            }

            Console.WriteLine();
        }

        public static void Main(string[] args)
        {
            List<int> array = new List<int>();

            int maxValue = 1000;
            int count = 20;

            RandFilling(array, count, maxValue);
            array.MergeSort();
            Console.WriteLine("DefaultCompare:");
            PrintArray(array);
            array.Clear();

            RandFilling(array, count, maxValue);
            array.MergeSort(ReverseCompare);
            Console.WriteLine("ReverseCompare:");
            PrintArray(array);
            array.Clear();

            RandFilling(array, count, maxValue);
            array.MergeSort(0, (count / 2) - 1, null);
            Console.WriteLine("Sort the first half of array:");
            PrintArray(array);
            array.Clear();
            Console.ReadKey();
        }
    }
}
