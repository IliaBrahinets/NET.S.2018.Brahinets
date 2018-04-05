using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Exceptions;
using Logic.Storage;

namespace Logic.Tests
{

    public class FindByISBN : IPredicate<Book>
    {
        private string ISBN { get; set; }
        
        public FindByISBN(string isbn)
        {
            ISBN = isbn;
        }
        public bool Result(Book elem)
        {
            return elem.ISBN == ISBN;
        }
    }

    class LogicTests
    {
        static void Main(string[] args)
        {
            BookListService bookService = new BookListService();

            Book book1 = new Book("1491987650", "Joseph Albahari&Ben Albahari", "C# 7.0 in a Nutshell: The Definitive Reference", "O'Reilly Media", 2018, 1092, 44.99m);
            Book book2 = new Book("978-0-7356-6745-7", "Jeffrey Richter", "CLR via C#", "Microsoft Press", 2012, 826, 59.99m);

            bookService.Add(book1);
            Console.WriteLine(book1 + " was Added");
            bookService.Add(book2);
            Console.WriteLine(book2 + " was Added");

            Console.WriteLine("Adding added book, assuming Exception");
            try
            {
                bookService.Add(book1);
            }
            catch (BookAlreadyExistsException ex)
            {
                Console.WriteLine("Exception was thrown:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            string testISBN = "978-0-7356-6745-7";
            Console.WriteLine("Finding by ISBN: " + testISBN);
            Console.WriteLine("Found book: " + bookService.FindBookByTag(new FindByISBN(testISBN)).ToString());

            const string storageFile = @"C:\Users\user\Desktop\EPAM\public folder\Epam-training\NET.S.2018.Brahinets.11\BinaryBookListStorage\StorageFile.txt";
            IBookListStorage storage = new BinaryBookListStorage(storageFile);

            Console.WriteLine("Saving to the binary storage");
            bookService.SaveIntoStorage(storage);

            Console.WriteLine("Try to get books from the binary storage");
            try
            {
                bookService.GetFromStorage(storage);
            }
            catch (BookAlreadyExistsException ex)
            {
                Console.WriteLine("Books was readed from storage sucesfully");
            }
            Console.ReadKey();
        }

    }
}
