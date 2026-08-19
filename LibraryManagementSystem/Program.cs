using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();
            library.SeedData();
            

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n====================================");
                Console.WriteLine("    LIBRARY MANAGEMENT SYSTEM       ");
                Console.WriteLine("====================================");
                Console.ResetColor();
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Register Member");
                Console.WriteLine("3. Borrow Book");
                Console.WriteLine("4. Return Book");
                Console.WriteLine("5. Search Member or Book");
                Console.WriteLine("6. Display Available Books");
                Console.WriteLine("7. Member Borrowing Record");
                Console.WriteLine("8. Late Report");
                Console.WriteLine("9. Exit");
                Console.Write("Select an option: ");
                int option = int.TryParse(Console.ReadLine(), out option) ? option : 0;
                switch (option)
                {
                    case 1:
                        string title = Validation.ReadNonEmptyString("Enter Title Of The Book: ");
                        string author = Validation.ReadNonEmptyString("Enter Author: ");
                        int year = Validation.ReadValidYear("Enter Year: ");
                        string genre = Validation.ReadNonEmptyString("Enter Genre: ");
                        library.AddBook(title, author, genre, year);
                        break;
                    case 2:
                        string name = Validation.ReadNonEmptyString("Enter Member Name: ");
                        string email = Validation.ReadValidEmail("Enter Member Email: ");
                        library.RegisterMember(name, email);
                        break;
                    case 3:
                        int memberId = Validation.ReadPositiveInteger("Enter Member ID: ");
                        int bookId = Validation.ReadPositiveInteger("Enter Book ID: ");
                        library.BorrowBook(memberId, bookId, DateTime.Now);
                        break;
                    case 4:
                        int returnBookId = Validation.ReadPositiveInteger("Enter Book ID to Return: ");
                        library.ReturnBook(returnBookId);
                        break;
                    case 5:
                        string query = Validation.ReadNonEmptyString("Enter Member Name or Book Title to Search: ");
                        library.SearchMemberOrBook(query);
                        break;
                    case 6:
                        library.DisplayAvailableBooks();
                        break;
                    case 7:
                        int recordMemberId = Validation.ReadPositiveInteger("Enter Member ID to View Borrowing Record: ");
                        library.MemberBorrowingRecord(recordMemberId);
                        break;
                    case 8:
                        library.LateingReport();
                        break;
                    case 9:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("Exiting program... Goodbye!");
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid option. Please try again.");
                        Console.ResetColor();
                        break;
                }
            }

        }
    }
}
