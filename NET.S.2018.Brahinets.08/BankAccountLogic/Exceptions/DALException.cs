using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountLogic.Exceptions
{
    class DALException : Exception
    {
        public DALException()
        {
        }

        public DALException(string message) : base(message)
        {
        }

        public DALException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DALException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
