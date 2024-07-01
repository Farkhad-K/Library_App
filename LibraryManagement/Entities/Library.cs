using System.Runtime.Serialization;
using LibraryManagement.AbstractClasses;
using LibraryManagement.Exceptions;
using LibraryManagement.Interfaces;
using LibraryManagement.Models;
using Spectre.Console;

namespace LibraryManagement.Entities;

public class Library : ISearchable
{
    private List<Book> Books = new List<Book>();
    private List<Book> ListOfBorrowedBooks = new List<Book>();
    private List<User> Users = new List<User>();

    #region Book 

    public List<Book> GetAllBooks() => Books;

    public bool AddBook(Book book)
    {
        var existingBook = Books.Find(b => b.ISBN == book.ISBN);
        if (existingBook is null)
        {
            Books.Add(book);
            return true;
        }

        return false;
    }

    public void UpdateBook(string query, Book updatedBook)
    {
        try
        {
            var bookForUpdate = Search(query);
            bookForUpdate.ISBN = updatedBook.ISBN;
            bookForUpdate.Title = updatedBook.Title;
            bookForUpdate.Author = updatedBook.Author;
            bookForUpdate.IsAvailable = updatedBook.IsAvailable;
            bookForUpdate.Language = updatedBook.Language;
            bookForUpdate.PublicationYear = updatedBook.PublicationYear;
            bookForUpdate.SmallDescribtion = updatedBook.SmallDescribtion;

            AnsiConsole.MarkupLine($"Book has been [green]updated[/].\n");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]An error occurred[/]: {ex.Message}\n");
        }
    }

    public void RemoveBook(string query)
    {
        try
        {
            var bookForDelete = Search(query);

            Books.Remove(bookForDelete);

            AnsiConsole.MarkupLine($"Book has been [green]removed[/].\n");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]An error occurred[/]: {ex.Message}\n");
        }
    }

    public Book Search(string query)
    {
        var existingBook = Books.FirstOrDefault(b => b.ISBN.ToLower().Trim(' ') == query.ToLower().Trim(' ') || b.Title.ToLower().Trim(' ') == query.ToLower().Trim(' '));

        if (existingBook is null)
            throw new BookNotFound("Book not found");

        return existingBook;
    }

    public List<Book> GetAuthorBooks(string query)
    {
        var existingBook = Books.Where(b => b.Author.ToLower().Trim(' ') == query.ToLower().Trim(' ')).ToList();

        if (existingBook is null)
            throw new InvalidOperationException("We don't have books of this author");

        return existingBook;
    }

    public List<Book> GetBorrowedBooks() => ListOfBorrowedBooks;
    public List<Book> GetAvailableBooks() => Books.Where(b => b.IsAvailable == true).ToList();

    #endregion

    #region User

    public void RegisterUser()
    {
        try
        {
            var user = new User();
            user.Id = int.Parse(AnsiConsole.Ask<string>("Enter [green]Id[/] of the user[red]*[/]:"));
            var existingUser = Users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser is not null)
            {
                AnsiConsole.MarkupLine($"[red]User[/] with this [green]id[/] already exists");
                return;
            }
            user.Fullname = AnsiConsole.Ask<string>("Enter [green]Full name[/] of the user[red]*[/]:");


            Users.Add(user);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]An error occurred[/]: {ex.Message}\n");
        }
    }

    public User SearchAUser(string query)
    {
        var existingUser = Users.FirstOrDefault(u => u.Fullname.ToLower().Trim(' ') == query.ToLower().Trim(' ') || u.Id == int.Parse(query.Trim(' ')));
        if (existingUser == null)
            throw new UserNotFound("There is no user with such name or id");

        return existingUser;
    }
    public List<User> GetAllUsers() => Users;

    public void RemoveUser(string query)
    {
        try
        {
            var userForDelete = SearchAUser(query);

            Users.Remove(userForDelete);

            AnsiConsole.MarkupLine($"User has been [green]removed[/].");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]An error occurred[/]: {ex.Message}\n");
        }
    }

    public void BorrowBook(string userQuery, string bookQuery)
    {
        try
        {
            var user = SearchAUser(userQuery);
            if (user.BorrowedBook != null && !string.IsNullOrEmpty(user.BorrowedBook.Title))
            {
                AnsiConsole.MarkupLine($"[red]User {user.Fullname} already has a borrowed book.[/]\n");
                return;
            }

            var bookToBorrow = Search(bookQuery);
            var borrowedBook = bookToBorrow.Borrow(bookQuery);

            user.BorrowedBook = (Book)borrowedBook;
            ListOfBorrowedBooks.Add((Book)borrowedBook);

            AnsiConsole.MarkupLine($"[red]{bookToBorrow.Title}[/] has been borrowed by [green]{user.Fullname}[/].");
            AnsiConsole.MarkupLine($"User should return the book after {bookToBorrow.DaysLeftToReturn().ToString()} days\n");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]An error occurred[/]: {ex.Message}\n");
        }
    }

    public void ReturnBook(string userQuery, string bookQuery)
    {
        try
        {
            var user = SearchAUser(userQuery);
            if (user.BorrowedBook == null || string.IsNullOrEmpty(user.BorrowedBook.Title))
            {
                AnsiConsole.MarkupLine($"[red]User {user.Fullname} does not have a borrowed book.[/]");
                return;
            }

            var bookToReturn = Search(bookQuery);

            if (bookToReturn.DaysLeftToReturn() <= 0)
            {
                var returnedBook = bookToReturn.Return(bookToReturn.ISBN);
                user.BorrowedBook = null!; // Ensure this is set to null
                AnsiConsole.MarkupLine($"[red]{bookToReturn.Title}[/] has not been returned on time");
                AnsiConsole.MarkupLine($"[green]{user.Fullname}[/] gets a warning. After [red]3[/] warnings user will be removed");
                user.Warnings++;
            }
            else
            {
                var returnedBook = bookToReturn.Return(bookToReturn.ISBN);
                user.BorrowedBook = null!; // Ensure this is set to null
                AnsiConsole.MarkupLine($"[red]{bookToReturn.Title}[/] has been returned by [green]{user.Fullname}[/].\n");
            }

            ListOfBorrowedBooks.Remove(bookToReturn);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]An error occurred[/]: {ex.Message}\n");
        }
    }

    public User GetBorrowedUser(string bookQuery)
    {
        var borrowedUser = Users.FirstOrDefault(b => b.BorrowedBook.ISBN.ToLower().Trim(' ') == bookQuery.ToLower().Trim(' ') || b.BorrowedBook.Title.ToLower().Trim(' ') == bookQuery.ToLower().Trim(' '));

        return borrowedUser!;
    }

    #endregion
}
