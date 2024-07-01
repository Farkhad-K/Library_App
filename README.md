# Library Management Console Application

This is a console-based Library Management System developed in C#. The application allows users to manage a collection of books and users, handle borrowing and returning of books, and track user warnings for overdue returns. The application uses the [Spectre.Console](https://spectreconsole.net/) library for enhanced console input and output.

## Table of Contents
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [How to Compile and Run the Project](#how-to-compile-and-run-the-project)
- [Usage](#usage)
- [Possible Errors](#possible-errors)

## Features
- Add, update, delete, and search for books.
- Register, find, and manage users.
- Borrow and return books.
- List all books, borrowed books, and available books.
- Track user warnings for overdue book returns.

## Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) installed on your machine.

## Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/Farkhad-K/Library_App.git

## How to Compile and Run the Project
1. After cloning the repository Navigate to the project directory
2. Then run the
    ```bash
    dotnet run

## Usage
1. You should use directional keys like Up and Down.
2. When you choose the necessary option from menu press Enter key

## Possible Errors
1. "Object reference not set to an instance of an object" this error may occur when you try to get List of all users after Returning a book. But there is one way to awoid this error: you just need to delete this user which made a return. Unfurtunatelly I could not find a solution for this😓
2. "Book Already Borrowed" - which means you cannot borrow this book.
3. "Book not Found" - which means one of the information that you have provided, possibly be incorrect.
4. "We don't have books of this author" - you can encounter with this error when you try to search books of an author. 
5. "There is no user with such id or name" - it means either information is invalid or this user has not been registered yet.
6. "User with this id already exists" - occures when id which you've provided while creating a new user is similar to the id of the existing user

