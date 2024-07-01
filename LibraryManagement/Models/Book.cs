using System.Diagnostics.CodeAnalysis;
using LibraryManagement.AbstractClasses;
using LibraryManagement.Exceptions;
using LibraryManagement.Interfaces;
using Spectre.Console;

namespace LibraryManagement.Models;

public class Book : LibraryItem, IBorrowable
{
    private const int BorrowingPeriodDays = 14;
    private DateTime? BorrowedDate { get; set; }


    public override string ISBN { get; set; } = string.Empty;
    public override string Title { get; set; } = string.Empty;
    public override string Author { get; set; } = string.Empty;
    public override DateOnly PublicationYear { get; set; } = new DateOnly();
    public override string Language { get; set; } = string.Empty;
    public override string SmallDescribtion { get; set; } = string.Empty;
    public override bool IsAvailable { get; set; } = true;

    public Book() { }

    public override string ToString()
        => $"[red]Title:[/] {Title}.\n[red]Author:[/] {Author}.\n[red]ISBN:[/] {ISBN}.\n" +
        $"[red]Publication Date:[/]{PublicationYear.Year}\n" +
        $"[red]Is Available:[/] {IsAvailable}\n[red]Book description:[/] [gray]{SmallDescribtion}[/]\n";

    public LibraryItem Borrow(string query)
    {
        if (!IsAvailable)
        {
            throw new BookAlreadyBorrowed("This book is already borrowed.");
        }

        if (ISBN.Equals(query, StringComparison.OrdinalIgnoreCase) ||
            Title.Equals(query, StringComparison.OrdinalIgnoreCase))
        {
            IsAvailable = false;
            BorrowedDate = DateTime.Now;
            return this;
        }

        throw new BookNotFound("Book not found.");
    }

    public LibraryItem Return(string query)
    {
        // if (IsAvailable)
        //     throw new InvalidOperationException("This book is not currently borrowed.");

        if (ISBN.Equals(query, StringComparison.OrdinalIgnoreCase) ||
            Title.Equals(query, StringComparison.OrdinalIgnoreCase))
        {
            IsAvailable = true;
            BorrowedDate = null;
            return this;
        }

        throw new BookNotFound("Book not found.");
    }

    public int DaysLeftToReturn()
    {
        // if (!BorrowedDate.HasValue)
        // {
        //     throw new InvalidOperationException("This book is not currently borrowed.");
        // }

        var dueDate = BorrowedDate.Value.AddDays(BorrowingPeriodDays);
        var daysLeft = (dueDate - DateTime.Now).Days;

        return daysLeft;
    }
}

