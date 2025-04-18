using System;
using System.Collections.Generic;

public class Library
{
    private List<string> availableBooks = new List<string>();            // List of all available books
    private List<string> borrowedBooks = new List<string>();    // List of borrowed books
    private int capacityLibrary = 10;    // Maximum number of books that can be stored in the library
    private int capacityBorrowed = 5;   // Maximum number of books that can be borrowed

    public static string GetValidInput(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            Console.WriteLine(); // Add a blank line before the prompt
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
            else
            {
                return input; // Return valid input
            }
        }
    }

    public void AddBooks()
    {
        // Check if library is already full
        if (availableBooks.Count >= capacityLibrary)
        {
            Console.WriteLine("Library is full. Cannot add more books.");
            return;
        }

        // Ask user to input the titles of the books separated by semicolons.
        string input = GetValidInput("Please enter the titles of the books you want to add separated by semicolons.");
        string[] titles = input.Split(';');

        // Add each title to the library
        foreach (string title in titles)
        {
            // Check if library is full.
            if (availableBooks.Count >= capacityLibrary - borrowedBooks.Count)
            {
                Console.WriteLine("Library is full. Cannot add more books.");
                break;
            }
            // Clean title
            string trimmedTitle = title.Trim(); // Remove extra spaces

            // Check if title is valid
            if (string.IsNullOrWhiteSpace(trimmedTitle) && availableBooks.Count < capacityLibrary)
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
            else // Add book to library
            {
                availableBooks.Add(trimmedTitle);
                Console.WriteLine($"Book '{trimmedTitle}' added to the library.");
            }
        }
    }

    public void RemoveBooks()
    {
        // Ask user to input the titles of the books
        string input = GetValidInput("Enter the titles of the books you want to remove separated by semicolons.");
        
        // Split the input into individual titles
        string[] titles = input.Split(';');

        // Loop over all books
        foreach (string title in titles)
        {
            string trimmedTitle = title.Trim(); // Remove extra spaces
            // Check if input is valid
            if (string.IsNullOrWhiteSpace(trimmedTitle))
            {
                Console.WriteLine("Invalid Title, please try again.");
            }
            else
            {
                // Remove book if possible from available list
                if (availableBooks.Remove(trimmedTitle))
                {
                    Console.WriteLine($"Book '{trimmedTitle}' removed from the library.");
                }
                // Remove book if possible from borrowed list
                else if (borrowedBooks.Remove(trimmedTitle))
                {
                    Console.WriteLine($"Book '{trimmedTitle}' removed from the borrowed list.");
                }
                // Print message if book is not found
                else
                {
                    Console.WriteLine($"Book '{trimmedTitle}' not found in the library or borrowed list.");
                }
            }
        }
    }

    public void DisplayBooks()
    {
        if (availableBooks.Count == 0)
        {
            Console.WriteLine("No books are currently available in the library.");
        }
        else
        {
            Console.WriteLine("Books available in the library:");
            foreach (string book in availableBooks)
            {
                Console.WriteLine($"- {book}");
            }
        }

        if (borrowedBooks.Count == 0)
        {
            Console.WriteLine("You have not borrowed any books.");
        }
        else
        {
            Console.WriteLine("Books you have borrowed:");
            foreach (string book in borrowedBooks)
            {
                Console.WriteLine($"- {book}");
            }
        }
    }

    public void SearchBook()
    {
        // Prompt the user to input the title of the book to search for
        string title = GetValidInput("Enter the title of the book you want to search for: ");

        // Check if the book is available
        if (availableBooks.Contains(title))
        {
            Console.WriteLine($"The book '{title}' is available in the library.");
        }
        // Check if the book if borrowed
        else if (borrowedBooks.Contains(title))
        {
            Console.WriteLine($"The book '{title}' is currently borrowed.");
        }
        else
        {
            Console.WriteLine($"The book '{title}' is not in the library.");
        }
    }

    public void BorrowBooks()
    {
        if (borrowedBooks.Count >= capacityBorrowed)
        {
            Console.WriteLine("You have already borrowed the maximum number of books (3). Please return a book before borrowing more.");
            return;
        }

        // Prompt user to enter the titles of the books to borrow
        string input = GetValidInput("Enter the titles of the books you want to borrow separated by semicolons.");
        string[] titles = input.Split(';');

        // Loop over all books to be borrowed
        foreach (string title in titles)
        {
            if (borrowedBooks.Count >= capacityBorrowed)
            {
                Console.WriteLine("You have reached the maximum borrowing limit (3 books).");
                break;
            }

            // Clean title
            string trimmedTitle = title.Trim(); // Remove extra spaces

            // Check if title is valid
            if (string.IsNullOrWhiteSpace(trimmedTitle))
            {
                Console.WriteLine("Invalid Input, please try again.");
                continue; // Skip to the next title
            }

            // Remove the book if possible from available list
            if (availableBooks.Remove(trimmedTitle))
            {
                // Add the book to the borrowed list
                borrowedBooks.Add(trimmedTitle);
                Console.WriteLine($"You have successfully borrowed '{trimmedTitle}'.");
            }
            else
            {
                Console.WriteLine($"The book '{trimmedTitle}' is not available in the library.");
            }
        }
    }

    public void ReturnBooks()
    {
        // Check if there are books to return
        if (borrowedBooks.Count == 0)
        {
            Console.WriteLine("You have not borrowed any books to return.");
            return;
        }

        // Prompt user to input books to return
        string input = GetValidInput("Enter the titles of the books you want to return (separate multiple titles with a semicolon): ");
        string[] titles = input.Split(';');

        // Loop over all books to return
        foreach (string title in titles)
        {
            // Clean title
            string trimmedTitle = title.Trim(); // Remove extra spaces

            // Remove book if possible from list of borrowed books
            if (borrowedBooks.Remove(trimmedTitle))
            {
                // Add book to available list
                availableBooks.Add(trimmedTitle);
                Console.WriteLine($"You have successfully returned '{trimmedTitle}'.");
            }
            else
            {
                Console.WriteLine($"The book '{trimmedTitle}' is not in your borrowed list.");
            }
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Library library = new Library();

        // Welcome message
        Console.WriteLine("Welcome to the Library Management System!");

        while (true)
        {
            // Display Menu
            DisplayMenu();

            // Get user input
            string userOption = Library.GetValidInput("");

            // Execute the corresponding action
            // Use try-and-catch block to handle errors if the execution fails
            try
            {
                ExecuteOption(userOption, library);   
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                Console.WriteLine("Please try restart the program. Program exits now.");
                Environment.Exit(0); // Exit program immediately
            }         
        }
    }

    private static void DisplayMenu()
    {
        Console.WriteLine("\nLibrary Management System");
        Console.WriteLine("1. Add Books");
        Console.WriteLine("2. Remove Books");
        Console.WriteLine("3. Display Books");
        Console.WriteLine("4. Search for a Book");
        Console.WriteLine("5. Borrow Books");
        Console.WriteLine("6. Return Books");
        Console.WriteLine("7. Exit");
        Console.WriteLine("Please type the number of your desired option:");
    }
    
    private static void ExecuteOption(string userOption, Library library)
    {
        switch (userOption)
        {
            case "1": // Add a book to the library
                library.AddBooks();
                break;

            case "2": // Remove a book from the library
                library.RemoveBooks();
                break;

            case "3": // Display all books in the library
                library.DisplayBooks();
                break;

            case "4": // Search for a book in the library
                library.SearchBook();
                break;

            case "5": // Borrow a book
                library.BorrowBooks();
                break;

            case "6": // Return a book
                library.ReturnBooks();
                break;
            
            case "7": // Exit the program
                Console.Write("Are you sure you want to exit? (y/n): ");
                string confirm = Library.GetValidInput("");
                if (confirm.ToLower() == "y")
                {
                    Console.WriteLine("Exiting the program. Goodbye!");
                    Environment.Exit(0); // Exit the program immediately
                }
                break;

            default: // Handle invalid input
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }
    }
}