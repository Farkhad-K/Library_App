using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Exceptions
{
    public class BookAlreadyBorrowed : Exception
    {
        public BookAlreadyBorrowed() : base() { }

        public BookAlreadyBorrowed(string message) : base(message) { }
        
        public BookAlreadyBorrowed(string message, Exception innerException) : base(message, innerException) { }
    }
}