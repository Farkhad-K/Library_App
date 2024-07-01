using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Exceptions
{
    public class BookNotFound : Exception
    {
        public BookNotFound() : base() { }

        public BookNotFound(string message) : base(message) { }

        public BookNotFound(string message, Exception innerException) : base(message, innerException) { }
    }
}