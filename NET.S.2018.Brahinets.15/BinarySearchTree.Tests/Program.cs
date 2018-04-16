using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class BinarySearchTreeTests
{

    private static void TestEnumerable<T>(IEnumerable<T> enumerable)
    {
        foreach(T elem in enumerable)
        {
            Console.WriteLine(elem);
        }
    }
    static void Main(string[] args)
    {
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Add(5); bst.Add(10); bst.Add(20);

        TestEnumerable(bst.GetInfixEnumerable());
        TestEnumerable(bst.GetPostfixEnumerable());
        TestEnumerable(bst.GetPrefixEnumerable());

        Console.ReadKey();

    }
}

