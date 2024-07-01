using LibraryManagement.Entities;
using LibraryManagement.Models;
using Spectre.Console;
using Spectre.Console.Extensions;

Library libraryEntity = new Library();

#region  Seeding some data

var CLR_Via_Csharp = new Book();
CLR_Via_Csharp.ISBN = "9780735668737";
CLR_Via_Csharp.Title = "CLR via C#";
CLR_Via_Csharp.Author = "Jeffrey Richter";
CLR_Via_Csharp.IsAvailable = true;
CLR_Via_Csharp.Language = "ENG";
CLR_Via_Csharp.PublicationYear = new DateOnly(2012, 11, 1);
CLR_Via_Csharp.SmallDescribtion = "Dig deep and master the intricacies of the common language runtime, C#, and .NET development. Led by programming expert Jeffrey Richter, a longtime consultant to the Microsoft .NET team - you’ll gain pragmatic insights for building robust, reliable, and responsive apps and components.";


var _1984 = new Book();
_1984.ISBN = "9780451524935";
_1984.Title = "1984";
_1984.Author = "George Orwell";
_1984.IsAvailable = true;
_1984.Language = "ENG";
_1984.PublicationYear = new DateOnly(1961, 11, 1);
_1984.SmallDescribtion = "A startling and haunting novel, 1984 creates an imaginary world that is completely convincing from start to finish. No one can deny the novel’s hold on the imaginations of whole generations, or the power of its admonitions—a power that seems to grow, not lessen, with the passage of time.";

var dune = new Book();
dune.ISBN = "9780441013597";
dune.Title = "Dune (Dune Chronicles, Book 1)";
dune.Author = "Frank Herbert";
dune.IsAvailable = true;
dune.Language = "ENG";
dune.PublicationYear = new DateOnly(2005, 11, 1);
dune.SmallDescribtion = "Set on the desert planet Arrakis, Dune is the story of Paul Atreides—who would become known as Muad'Dib—and of a great family's ambition to bring to fruition humankind's most ancient and unattainable dream.";

var Csharp_10 = new Book();
Csharp_10.ISBN = "9781801077361";
Csharp_10.Title = "C# 10 and .NET 6";
Csharp_10.Author = "Mark J. Price";
Csharp_10.IsAvailable = true;
Csharp_10.Language = "ENG";
Csharp_10.PublicationYear = new DateOnly(2021, 11, 1);
Csharp_10.SmallDescribtion = "Publisher’s Note: Microsoft will stop supporting .NET 6 from November 2024. The newer 8th edition of the book is available that covers .NET 8 (end-of-life November 2026) with C# 12 and EF Core 8. Purchase of the print or Kindle book includes a free PDF eBook.";


libraryEntity.AddBook(CLR_Via_Csharp);
libraryEntity.AddBook(_1984);
libraryEntity.AddBook(dune);
libraryEntity.AddBook(Csharp_10);

#endregion


AnsiConsole.MarkupLine("You can move through [green]MENU[/] using [red]Up[/] and [red]Down[/] buttons \n");

while (true)
{
    var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Main Menu")
            .PageSize(15)
            .AddChoices(new[] {
            "Menu of the Books",
            "Menu of the Users",
            "Clean up the console",
            "Close the App"
            }));

    if (choice == "Close the App")
        break;

    switch (choice)
    {
        case "Menu of the Books":
            BooksMenu();
            break;
        case "Menu of the Users":
            UserMenu();
            break;
        case "Clean up the console":
            AnsiConsole.Clear();
            break;
    }
}

void UserMenu()
{
    while (true)
    {
        var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Menu of Users")
                .PageSize(10)
                .AddChoices(new[] {
            "Register a user",
            "List of all users",
            "Find a user",
            "User wants to borrow a book",
            "User wants to return a book",
            "Clean up the console",
            "Back to Main Menu"
                }));

        if (choice == "Back to Main Menu")
            break;

        switch (choice)
        {
            case "Register a user":
                libraryEntity.RegisterUser();
                break;
            case "List of all users":
                DisplayAllUsers();
                break;
            case "Find a user":
                var query = AnsiConsole.Ask<string>("Enter info of the user which you want to find [gray](ex:Fullname/Id)[/]:");
                DisplayAUser(query);
                break;
            case "User wants to borrow a book":
                var userQuery = AnsiConsole.Ask<string>("Enter info of the user [gray](ex:Fullname/Id)[/]:");
                var bookQuery = AnsiConsole.Ask<string>("Enter info of the book [gray](ex:ISBN/Title)[/]:");
                libraryEntity.BorrowBook(userQuery, bookQuery);
                break;
            case "User wants to return a book":
                var userQuery2 = AnsiConsole.Ask<string>("Enter info of the user [gray](ex:Fullname/Id)[/]:");
                var bookQuery2 = AnsiConsole.Ask<string>("Enter info of the book [gray](ex:ISBN/Title)[/]:");
                libraryEntity.ReturnBook(userQuery2, bookQuery2);
                break;
            case "Clean up the console":
                AnsiConsole.Clear();
                break;
        }
    }
}

void BooksMenu()
{
    while (true)
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Menu of the Books")
            .PageSize(15)
            .AddChoices(new[] {
            "List of all books",
            "Find a book",
            "Add a book",
            "Update a book",
            "Delete a book",
            "Search by aouthor",
            "Borrowed Books list",
            "Available Books to borrow",
            "Clean up the console",
            "Back to the Main Menu"
            }));

        if (choice == "Back to the Main Menu")
            break;

        switch (choice)
        {
            case "List of all books":
                DisplayAllBooks();
                break;
            case "Find a book":
                var query = AnsiConsole.Ask<string>("Enter info of the book which you want to find [gray](ex:ISBN/Title)[/]:");
                DisplayABook(query);
                break;
            case "Add a book":
                var newBook = CreateBook();
                var isCreatedSuccesfully = libraryEntity.AddBook(newBook);
                if (isCreatedSuccesfully)
                {
                    AnsiConsole.MarkupLine($"[green]{newBook.Title}[/] was added to the liblry.");
                    AnsiConsole.MarkupLine($"You can [yellow]Find[/] or [yellow]Borrow[/] it by choosing corresponded option in [green]\"Menu of the Books\"[/]\n");
                }
                else
                {
                    AnsiConsole.MarkupLine($"A book with this ISBN: [green]{newBook.ISBN}[/]  was [red]NOT[/] added to the liblry.");
                    AnsiConsole.MarkupLine($"Check on [green]\"See available books\"[/] if it was added already.\n");
                }
                break;
            case "Update a book":
                string query2 = AnsiConsole.Ask<string>("Enter info of the book which you want to update [gray](ex:ISBN/Title)[/]:");
                var bookForUpdate = CreateBook();
                libraryEntity.UpdateBook(query2, bookForUpdate);
                break;
            case "Delete a book":
                var query3 = AnsiConsole.Ask<string>("Enter info of the book which you want to delete [gray](ex:ISBN/Title)[/]:");
                libraryEntity.RemoveBook(query3);
                break;
            case "Search by aouthor":
                var authorName = AnsiConsole.Ask<string>("Enter [green]Author[/]'s name [gray]you'll get all books related to him/her[/]:");
                DisplayAllBooksOfAuthor(authorName);
                break;
            case "Borrowed Books list":
                DisplayAllBorrowedBooks();
                break;
            case "Available Books to borrow":
                DisplayAllAvailableBooks();
                break;
            case "Clean up the console":
                AnsiConsole.Clear();
                break;
        }
    }
}

#region Book's UI 

Book CreateBook()
{
    var newBook = new Book();
    newBook.ISBN = AnsiConsole.Ask<string>("Enter [green]ISBN[/][red]*[/]:");
    newBook.Title = AnsiConsole.Ask<string>("Enter [green]Title[/][red]*[/]:");
    newBook.Author = AnsiConsole.Ask<string>("Enter [green]Author[/][red]*[/]:");
    newBook.IsAvailable = true;
    newBook.Language = AnsiConsole.Ask<string>("Enter abreviation of [green]language[/][gray](ex:ENG/RU)[/]:").ToUpper();
    var publicationYear = AnsiConsole.Ask<int>("Enter publication [green]year[/][red]*[/]:");
    newBook.PublicationYear = new DateOnly(publicationYear, 1, 1);
    newBook.SmallDescribtion = AnsiConsole.Ask<string>("Enter [green]small description of the book[/]:");

    return newBook;

    /*
    if(newBook.ISBN is null)
        throw new Exception("ISBN cannot be null");
    */

}

void DisplayABook(string query)
{
    try
    {
        var bookForDisplay = libraryEntity.Search(query);

        if (bookForDisplay is null)
            AnsiConsole.MarkupLine("Unfortunately there [red]NO[/] such book.");
        else
            AnsiConsole.MarkupLine(bookForDisplay.ToString());
    }
    catch (Exception ex)
    {
        AnsiConsole.WriteLine($"[red]An error occurred[/]: {ex.Message}");
    }
}

void DisplayAllBooks()
    {
        var table = new Table();
        table.AddColumn("[green]ISBN[/]");
        table.AddColumn("[green]Title[/]");
        table.AddColumn("[green]Author[/]");
        table.AddColumn("[green]Publication year[/]");
        table.AddColumn("[green]Language[/]");
        table.AddColumn("[green]Is Available[/]");

        foreach (var book in libraryEntity.GetAllBooks())
        {
            var available = book.IsAvailable == true ? "Yes" : "No";
            table.AddRow(book.ISBN, book.Title, book.Author, book.PublicationYear.Year.ToString(), book.Language, available);
        }

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine(" ");
    }

void DisplayAllBooksOfAuthor(string authorName)
{
    try
    {
        var authorsBooks = libraryEntity.GetAuthorBooks(authorName);

        AnsiConsole.WriteLine($"{authorName.ToUpper()}'s books:");

        var table = new Table();
        table.AddColumn("[green]ISBN[/]");
        table.AddColumn("[green]Title[/]");
        table.AddColumn("[green]Publication year[/]");
        table.AddColumn("[green]Language[/]");
        table.AddColumn("[green]Is Available[/]");

        foreach (var book in authorsBooks)
        {
            var available = book.IsAvailable == true ? "Yes" : "No";

            table.AddRow(book.ISBN, book.Title, book.PublicationYear.Year.ToString(), book.Language, available);
        }

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine(" ");
    }
    catch (Exception ex)
    {
        AnsiConsole.MarkupLine($"[red]Error:[/] {ex.Message}\n");
    }
}

void DisplayAllBorrowedBooks()
    {
        var table = new Table();
        table.AddColumn("[green]ISBN[/]");
        table.AddColumn("[green]Title[/]");
        table.AddColumn("[green]Author[/]");
        table.AddColumn("[green]Publication year[/]");
        table.AddColumn("[green]Language[/]");
        table.AddColumn("[green]Is Available[/]");
        table.AddColumn("[green]Borrwed Person[/]");

        foreach (var book in libraryEntity.GetBorrowedBooks())
        {
            var user = libraryEntity.GetBorrowedUser(book.ISBN);
            var available = book.IsAvailable == true ? "Yes" : "No";

            table.AddRow(book.ISBN, book.Title, book.Author, book.PublicationYear.Year.ToString(), book.Language, available, user.Fullname);
        }

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine(" ");
    }

void DisplayAllAvailableBooks()
    {
        var table = new Table();
        table.AddColumn("[green]ISBN[/]");
        table.AddColumn("[green]Title[/]");
        table.AddColumn("[green]Author[/]");
        table.AddColumn("[green]Publication year[/]");
        table.AddColumn("[green]Language[/]");
        table.AddColumn("[green]Is Available[/]");

        foreach (var book in libraryEntity.GetAvailableBooks())
        {
            var user = libraryEntity.GetBorrowedUser(book.ISBN);
            var available = book.IsAvailable == true ? "Yes" : "No";

            table.AddRow(book.ISBN, book.Title, book.Author, book.PublicationYear.Year.ToString(), book.Language, available);
        }

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine(" ");
    }

#endregion

#region User's UI 

void DisplayAllUsers()
{
    // AnsiConsole.Write(" ");

    var table = new Table();
    table.AddColumn("[green]ID[/]");
    table.AddColumn("[green]Fullname[/]");
    table.AddColumn("[green]Title of borrowed book[/]");
    table.AddColumn("[green]Warnings Count[/]");

    foreach (var user in libraryEntity.GetAllUsers())
    {
        var borrowedBookTitle = user.BorrowedBook == null ? "No books borrowed" : user.BorrowedBook.Title;
        table.AddRow(user.Id.ToString(), user.Fullname, borrowedBookTitle, user.Warnings.ToString());
    }

    AnsiConsole.Write(table);
    AnsiConsole.WriteLine(" ");
}

void DisplayAUser(string query)
{
    try
    {
        var user = libraryEntity.SearchAUser(query);
        var borrowedBookTitle = user.BorrowedBook == null ? "No book borrowed" : user.BorrowedBook.Title;
        AnsiConsole.MarkupLine($"[red]ID[/]: {user.Id.ToString()}");
        AnsiConsole.MarkupLine($"[red]Fullname[/]: {user.Fullname}");
        AnsiConsole.MarkupLine($"[red]Title of the borrowed book[/]: {borrowedBookTitle}");
        AnsiConsole.MarkupLine($"[red]Count of warnings[/]: {user.Warnings.ToString()}\n");
        if (user.Warnings > 3)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title($"User got more than [red]3[/] warnings \n Do you want to remove [gray]{user.Fullname}[/]?")
                .PageSize(10)
                .AddChoices(new[] {
            "Yes",
            "No"
                }));

            if (choice == "Yes")
                libraryEntity.RemoveUser(user.Fullname);
            if (choice == "No")
                return;
        }
    }
    catch (Exception ex)
    {
        AnsiConsole.MarkupLine($"[red]Error:[/] {ex.Message}\n");
    }
}


#endregion
