using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// BinarySearchTree where key(left) is less than key(Node) and key(right) is more than key(Node).
/// Don't store items with the same keys.
/// </summary>
public class BinarySearchTree<T> : ICollection<T>
{
    private Node<T> head;
    private IComparer<T> comparer;

    public int Count { get; set; }

    public bool IsReadOnly => false;

    public BinarySearchTree()
    {
        HandleComparer(null);
        comparer = Comparer<T>.Default;
    }
    public BinarySearchTree(IComparer<T> comparer)
    {
        this.comparer = comparer;
    }
    public BinarySearchTree(Comparison<T> comparison)
    {
        this.comparer = Comparer<T>.Create(comparison);
    }
    public BinarySearchTree(IEnumerable<T> collection)
    {
        HandleComparer(null);
        comparer = Comparer<T>.Default;

        if (collection == null)
        {
            throw new ArgumentNullException($"{nameof(collection)} is null");
        }

        foreach (T item in collection)
        {
            Add(item);
        }
    }

    public void Add(T item)
    {
        Count++;
        if (head == null)
        {
            head = new Node<T> { Value = item, Left = null, Right = null };

            return;
        }

        Node<T> node = head;

        while (true)
        {
            int c = CleverComarasion(item, node.Value);

            if (c == 0)
            {
                Count--;
                node.Value = item;
                break;
            }

            if (c < 0)
            {
                if (node.Left == null)
                {
                    node.Left = new Node<T> { Value = item, Left = null, Right = null };
                    break;
                }
                else
                {
                    node = node.Left;
                }
            }
            else
            {
                if (node.Right == null)
                {
                    node.Right = new Node<T> { Value = item, Left = null, Right = null };
                    break;
                }
                else
                {
                    node = node.Right;
                }
            }
        }
    }

    public void Clear()
    {
        head = null;
    }

    public bool Contains(T item)
    {
        Node<T> found = Find(item);

        if(found == null)
        {
            return false;
        }

        EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;

        if (equalityComparer.Equals(item, found.Value))
        {
            return true;
        }

        return false;
    }

    public IEnumerable<T> GetInfixEnumerable()
    {
        if (head == null)
        {
            return EmptyEnumerable();
        }

        return InfixTraverse(head);
    }

    private IEnumerable<T> InfixTraverse(Node<T> node)
    {
        if(node.Left != null)
        {
            InfixTraverse(node.Left);
        }

        yield return node.Value;

        if (node.Right != null)
        {
            InfixTraverse(node.Right);
        }
    }

    public IEnumerable<T> GetPostfixEnumerable()
    {
        if (head == null)
        {
            return EmptyEnumerable();
        }

        return PostfixTraverse(head);
    }

    private IEnumerable<T> PostfixTraverse(Node<T> node)
    {
        if (node.Left != null)
        {
            InfixTraverse(node.Left);
        }

        if (node.Right != null)
        {
            InfixTraverse(node.Right);
        }

        yield return node.Value;
    }

    public IEnumerable<T> GetPrefixEnumerable()
    {
        if (head == null)
        {
            return EmptyEnumerable();
        }

        return PrefixTraverse(head);
    }

    private IEnumerable<T> PrefixTraverse(Node<T> node)
    {
        yield return node.Value;

        if (node.Left != null)
        {
            InfixTraverse(node.Left);
        }

        if (node.Right != null)
        {
            InfixTraverse(node.Right);
        }
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return GetInfixEnumerable().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<T>)this).GetEnumerator();
    }

    private IEnumerable<T> EmptyEnumerable()
    {
        yield break;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool Remove(T item)
    {
        Node<T> found = Find(item);

        if(found == null)
        {
            return false;
        }

        Node<T> parent = FindParent(found);

        if (parent == null)
        {
            //the head case
        }

        if (found.Left == null && found.Right == null)
        {
            if (parent.Left == found)
            {
                parent.Left = null;
            }
            else
            {
                parent.Right = null;
            }

        }

        if (found.Left != null && found.Right != null)
        {

        }
        else
        {
            Node<T> cpyFrom = null;
            if (found.Left == null)
            {
                cpyFrom = found.Right;
            }
            else
            {
                cpyFrom = found.Left;
            }

            found.Value = cpyFrom.Value;
            found.Right = cpyFrom.Right;
            found.Left = cpyFrom.Left;
        }
        return true;
    }

    private Node<T> Find(T item)
    {
        Node<T> node = head;

        while (node != null)
        {
            int c = CleverComarasion(item, node.Value);

            if (c == 0)
            {
                return node;
            }

            if (c > 0)
            {
                node = node.Right;
            }
            else
            {
                node = node.Left;
            }
        }

        return null;
    }

    private Node<T> FindParent(Node<T> child)
    {
        if (child == head)
        {
            return null;
        }

        Node<T> node = head;

        while (node != null)
        {
            int c = CleverComarasion(child.Value, node.Value);

            if (c > 0)
            {
                if (child == node.Right)
                {
                    return node;
                }
                node = node.Right;
            }
            else
            {
                if (child == node.Left)
                {
                    return node;
                }
                node = node.Left;
            }
        }

        return null;
    }

    private int CleverComarasion(T item1, T item2)
    {
        int c;
        try
        {
            c = comparer.Compare(item1, item2);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error was occured during the comparasion", ex);
        }

        return c;
    }

    private void HandleComparer(IComparer<T> comparer)
    {
        if (comparer == null)
        {
            if (!typeof(IComparable).IsAssignableFrom(typeof(T))
             && !typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
            {
                throw new InvalidOperationException("can't find a comparer for elements");
            }
        }

    }

    private class Node<Titem>
    {
        public Titem Value;
        public Node<Titem> Left;
        public Node<Titem> Right;
    }

}

