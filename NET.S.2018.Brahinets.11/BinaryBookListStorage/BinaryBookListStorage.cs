using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Logic;
using Logic.Storage;

public class BinaryBookListStorage : IBookListStorage
{ 
    private string FilePath { get; }

    public BinaryBookListStorage(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"{filePath} is not found");
        }

        FilePath = filePath;
    }

    public List<Book> GetBooks()
    {
        List<Book> booksList;

        using (FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        using (BinaryReader reader = new BinaryReader(fileStream))
        {
            int count = reader.ReadInt32();

            booksList = new List<Book>(count);

            for(int i = 0; i < count; i++)
            {
                Book book = ReadBook(reader);

                booksList.Add(book);
            }
        }

        return booksList;
    }

    public void SaveBooks(List<Book> books)
    {
        using (FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
        using (BinaryWriter writer = new BinaryWriter(fileStream))
        {
            writer.Write(books.Count);

            foreach(Book book in books)
            {
                WriteBook(book, writer);
            }
        }
    }

    private void WriteBook(Book book, BinaryWriter binaryWriter)
    {
        binaryWriter.Write(book.ISBN);
        binaryWriter.Write(book.Author);
        binaryWriter.Write(book.Title);
        binaryWriter.Write(book.Publisher);
        binaryWriter.Write(book.PublishYear);
        binaryWriter.Write(book.NumberOfPages);
        binaryWriter.Write(book.Price);
    }

    private Book ReadBook(BinaryReader binaryReader)
    {
        string isbn = binaryReader.ReadString();
        string author = binaryReader.ReadString();
        string title = binaryReader.ReadString();
        string publisher = binaryReader.ReadString();
        int publishYear = binaryReader.ReadInt32();
        int numberOfPages = binaryReader.ReadInt32();
        Decimal price = binaryReader.ReadDecimal();

        return new Book(isbn, author, title, publisher, publishYear, numberOfPages, price);
    }
}

