using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Exceptions;
using Logic.Storage;

namespace Logic
{
    public class BookListService
    {
        private List<Book> BookList { get; set; }

        public BookListService()
        {
            BookList = new List<Book>();
        }

        /// <summary>
        /// Adding the given book to the collection.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when the given book is null</exception>
        /// <exception cref="BookAlreadyExistsException">Thrown when the given book is already exists in the collection.</exception>
        public void Add(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException($"{nameof(book)} is null");
            }

            if (BookList.Contains(book))
            {
                throw new BookAlreadyExistsException($"the given {nameof(book)} is already exists");
            }

            BookList.Add(book);
        }

        /// <summary>
        /// Finding a book by the given criteria. Returns the first match. if can't find mathced books returns null.
        /// </summary>
        /// <param name="criteria">A search criteria.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given criteria is null.</exception>
        public Book FindBookByTag(IPredicate<Book> criteria)
        {
            if (criteria == null)
            {
                throw new ArgumentNullException($"{nameof(criteria)} is null");
            }

            for (int i = 0; i < BookList.Count; i++)
            {
                Book curr = BookList[i];

                if (criteria.Result(curr))
                {
                    return curr;
                }
            }

            return null;
        }

        /// <summary>
        /// Sorting the books collection. As a sort criteria is used comparer. if it's null used Books inplementation of IComaparable.
        /// </summary>
        /// <param name="comparer">a Sorting criteria.</param>
        /// <exception cref="BooksSortException">Thrown when an error occured while sorting.</exception>
        public void SortBooksByTag(IComparer<Book> comparer)
        {
            try
            {
                BookList.Sort(comparer);
            }
            catch (Exception ex)
            {
                throw new BooksSortException("Error is occured when sorting", ex);
            }
        }

        /// <summary>
        /// Remove the given book from the collection.
        /// </summary>
        /// <param name="book">The book to remove.</param>
        /// <exception cref="BookNotFoundException">Thrown when book was not found.</exception>
        public void Remove(Book book)
        {
            int index = BookList.IndexOf(book);

            if (index == -1)
            {
                throw new BookNotFoundException($"{nameof(book)} is not found");
            }

            BookList.RemoveAt(index);
        }

        public void GetFromStorage(IBookListStorage storage)
        {
            List<Book> gettedBooks;

            try
            {
                gettedBooks = storage.GetBooks();
            }
            catch (Exception ex)
            {
                throw new BooksStorageException("Error was occured while getting books from the storage", ex);
            }

            foreach(Book book in gettedBooks)
            {
                Add(book);
            }
            
        }

        public void SaveIntoStorage(IBookListStorage storage)
        {
            try
            {
                storage.SaveBooks(BookList);
            }
            catch (Exception ex)
            {
                throw new BooksStorageException("Error was occured while saving books from the storage", ex);
            }
            
        }


    }
}
