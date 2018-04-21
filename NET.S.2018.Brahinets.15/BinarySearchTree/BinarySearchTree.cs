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
    #region Fields
    private Node<T> head;
    private IComparer<T> comparer;
    #endregion

    #region Constructors
    /// <summary>
    /// Initialize the instance of the BST.
    /// As a comparer the default comparer is used.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the T does't implement IComparable or IComparable<typeparamref name="T"/>.</exception>
    public BinarySearchTree()
    {
        HandleComparer(null);
        comparer = Comparer<T>.Default;
    }

    /// <summary>
    /// Initialize the instance of the BST.
    /// As a comparer the given comparer is used.
    /// </summary>
    public BinarySearchTree(IComparer<T> comparer)
    {
        this.comparer = comparer;
    }

    /// <summary>
    /// Initialize the instance of the BST.
    /// As a comparer the give comparison is used.
    /// </summary>
    public BinarySearchTree(Comparison<T> comparison)
    {
        this.comparer = Comparer<T>.Create(comparison);
    }

    /// <summary>
    /// Initialize the instance of the BST from the collection.
    /// As a comparer the default comparer is used.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the T does't implement IComparable or IComparable<typeparamref name="T"/>.</exception>
    public BinarySearchTree(IEnumerable<T> collection)
    {
        HandleComparer(null);

        comparer = Comparer<T>.Default;

        InitFromCollection(collection);
    }

    /// <summary>
    /// Initialize the instance of the BST from the collection.
    /// As a comparer the given comparer is used.
    /// </summary>
    public BinarySearchTree(IEnumerable<T> collection, IComparer<T> comparer)
    {
        this.comparer = comparer;

        InitFromCollection(collection);
    }

    /// <summary>
    /// Initialize the instance of the BST from the collection.
    /// As a comparer the given comparasion is used.
    /// </summary>
    public BinarySearchTree(IEnumerable<T> collection, Comparison<T> comparison)
    {
        this.comparer = Comparer<T>.Create(comparison);

        InitFromCollection(collection);
    }

    private void InitFromCollection(IEnumerable<T> collection)
    {
        if (collection == null)
        {
            throw new ArgumentNullException($"{nameof(collection)} is null");
        }

        foreach (T item in collection)
        {
            Add(item);
        }
    }
    #endregion

    #region Properties
    public int Count { get; set; }
    public bool IsReadOnly => false;
    #endregion

    #region Methods

    #region Public
    /// <summary>
    /// Add an item to the BST.
    /// Don't store items with the same keys,in such cases the new item overwrite the old item. 
    /// </summary>
    /// <param name="item">The item to add.</param>
    public void Add(T item)
    {
        if (head == null)
        {
            Count = 1;
            head = new Node<T> { Value = item, Left = null, Right = null };
            return;
        }

        Add(head, new Node<T> { Value = item, Left = null, Right = null });

    }

    /// <summary>
    /// Clear all the elements from the BST.
    /// </summary>
    public void Clear()
    {
        head = null;
    }

    /// <summary>
    /// Returns true if the item exists in the BST, false otherwise.
    /// Comparing is carried out by means of a order comparer and the default equality comparer.
    /// </summary>
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

    /// <summary>
    /// Copies the elements of the BST to an System.Array,
    /// starting at a particular System.Array index.
    /// </summary>
    public void CopyTo(T[] array, int arrayIndex)
    {
        if(array == null)
        {
            throw new ArgumentNullException($"{nameof(array)} is null");
        }

        if(arrayIndex < 0)
        {
            throw new ArgumentOutOfRangeException($"{nameof(arrayIndex)} can not be less than zero");
        }

        if(array.Length - arrayIndex < Count)
        {
            throw new ArgumentException($"the avaliable space from the {nameof(arrayIndex)} to the end of array, does't fit to the size of collection");
        }

        T[] from = this.ToArray();

        Array.Copy(from, 0, array, arrayIndex, from.Length);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool Remove(T item)
    {
        Node<T> removed = Find(item);

        if (removed == null)
        {
            return false;
        }

        Node<T> parent = FindParent(removed);

        if (parent == null)
        {
            //the head case
        }

        if (removed.Left == null && removed.Right == null)
        {
            if (parent.Left == removed)
            {
                parent.Left = null;
            }
            else
            {
                parent.Right = null;
            }

        }

        if (removed.Left != null && removed.Right != null)
        {
            if (removed.Right.Left == null)
            {
                removed.Right.Left = removed.Left;
            }
            else
            {

            }


        }
        else
        {
            Node<T> cpyFrom = null;
            if (removed.Left == null)
            {
                cpyFrom = removed.Right;
            }
            else
            {
                cpyFrom = removed.Left;
            }

            removed.Value = cpyFrom.Value;
            removed.Right = cpyFrom.Right;
            removed.Left = cpyFrom.Left;
        }
        return true;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return GetInfixEnumerable().GetEnumerator();
    }

    /// <summary>
    /// Return the enumerable that enumerates the BST by the infix traverse.
    /// </summary>
    public IEnumerable<T> GetInfixEnumerable()
    {
        if (head == null)
        {
            return EmptyEnumerable();
        }

        return InfixTraverse(head);
    }

    /// <summary>
    /// Return the enumerable that enumerates the BST by the postfix traverse.
    /// </summary>
    public IEnumerable<T> GetPostfixEnumerable()
    {
        if (head == null)
        {
            return EmptyEnumerable();
        }

        List<T> traversedSeq = new List<T>(Count);

        PostfixTraverse(head, item => traversedSeq.Add(item));

        return traversedSeq;
    }

    /// <summary>
    /// Return the enumerable that enumerates the BST by the prefix traverse.
    /// </summary>
    public IEnumerable<T> GetPrefixEnumerable()
    {
        if (head == null)
        {
            return EmptyEnumerable();
        }

        return PrefixTraverse(head);
    }

    #endregion

    #region Private

    private void Add(Node<T> root, Node<T> newNode)
    {
        Count++;

        Node<T> node = root;

        while (true)
        {
            int c = CleverComparasion(newNode.Value, node.Value);

            if (c == 0)
            {
                Count--;
                node.Value = newNode.Value;
                break;
            }

            if (c < 0)
            {
                if (node.Left == null)
                {
                    node.Left = newNode;
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
                    node.Right = newNode;
                    break;
                }
                else
                {
                    node = node.Right;
                }
            }
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

    private IEnumerable<T> InfixTraverse(Node<T> node)
    {
        if (node.Left != null)
        {
            foreach (T item in InfixTraverse(node.Left))
            {
                yield return item;
            }
        }

        yield return node.Value;

        if (node.Right != null)
        {
            foreach (T item in InfixTraverse(node.Right))
            {
                yield return item;
            }
        }
    }

    private void PostfixTraverse(Node<T> node, Action<T> callback)
    {
        if (node.Left != null)
        {
            PostfixTraverse(node.Left, callback);
        }

        if (node.Right != null)
        {
            PostfixTraverse(node.Right, callback);
        }

        callback(node.Value);
    }

    private IEnumerable<T> PrefixTraverse(Node<T> node)
    {
        yield return node.Value;

        if (node.Left != null)
        {
            foreach(T item in PrefixTraverse(node.Left))
            {
                yield return item;
            }
        }

        if (node.Right != null)
        {
            foreach (T item in PrefixTraverse(node.Right))
            {
                yield return item;
            }
        }
    }

    private Node<T> Find(T item)
    {
        Node<T> node = head;

        while (node != null)
        {
            int c = CleverComparasion(item, node.Value);

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
            int c = CleverComparasion(child.Value, node.Value);

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

    private int CleverComparasion(T item1, T item2)
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
    #endregion

    #endregion

    private class Node<Titem>
    {
        public Titem Value;
        public Node<Titem> Left;
        public Node<Titem> Right;
    }

}

