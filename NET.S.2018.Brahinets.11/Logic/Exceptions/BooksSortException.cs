using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class BooksSortException : Exception
    {
        public BooksSortException()
        {
        }

        public BooksSortException(string message) : base(message)
        {
        }

        public BooksSortException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BooksSortException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
