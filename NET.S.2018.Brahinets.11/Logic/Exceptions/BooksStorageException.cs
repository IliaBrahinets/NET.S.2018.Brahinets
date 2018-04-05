using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    class BooksStorageException : Exception
    {
        public BooksStorageException()
        {
        }

        public BooksStorageException(string message) : base(message)
        {
        }

        public BooksStorageException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BooksStorageException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
