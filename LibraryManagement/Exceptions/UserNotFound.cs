using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Exceptions
{
    public class UserNotFound : Exception
    {
        public UserNotFound() : base() { }

        public UserNotFound(string message) : base(message) { }
        
        public UserNotFound(string message, Exception innerException) : base(message, innerException) { }
    }
}