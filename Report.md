# Object-Oriented Programming Principles in Library Management System

## Overview

This document explains the principles of Object-Oriented Programming (OOP) used in the Library Management System project. The main principles of OOP are Encapsulation, Abstraction, Inheritance, and Polymorphism. Each of these principles is utilized in the project to create a robust, maintainable, and scalable codebase.

## Encapsulation

Encapsulation is the mechanism of restricting access to some of the object's components and protecting the object's integrity by preventing outsiders from setting internal data directly. This is achieved by using access modifiers such as `private`, `protected`, and `public`.
```csharp
    private List<Book> Books = new List<Book>();
    private List<Book> ListOfBorrowedBooks = new List<Book>();
    private List<User> Users = new List<User>();
```
### Example in Code

In the project, encapsulation is used in classes such as `Book` and `User`. The fields are kept private, and public properties or methods are provided to access and modify the fields.

```csharp
public class Book
{
    public string ISBN { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public bool IsAvailable { get; set; }
    public string Language { get; set; }
    public DateOnly PublicationYear { get; set; }
    public string SmallDescribtion { get; set; }
}
```
### Abstraction is implemented through interfaces in the project.
```csharp
public interface IBorrowable
{
    void Borrow(string borrower);
    void Return();
    int GetBorrowDuration();
}
```
