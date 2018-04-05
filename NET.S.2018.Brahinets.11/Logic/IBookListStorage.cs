using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Storage
{
    public interface IBookListStorage
    {
        void SaveBooks(List<Book> books);
        List<Book> GetBooks();
    }
}
