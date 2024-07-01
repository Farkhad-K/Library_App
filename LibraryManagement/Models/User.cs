using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public Book BorrowedBook { get; set; } = new Book();
        public int Warnings { get; set; } = 0;

        public User()
        {
        }
    }
}