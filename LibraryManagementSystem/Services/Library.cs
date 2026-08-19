using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Services
{
    public class Library
    {
        // Implement the Library class with methods to manage books, members, and borrow records "Business Logic"
        private Book[] books = new Book[200];
        private Member[] members = new Member[100];
        private BorrowRecord[] borrowRecords = new BorrowRecord[100];

        private int bookCount = 0;
        private int memberCount = 0;
        private int borrowRecordCount = 0;

        private int CurrentBookId = 1;
        private int CurrentMemberId = 1;
        private int CurrentBorrowRecordId = 1;

        #region Add Book
        /// <summary>
        /// Adds a new book to the library with the specified title, author, genre, year, and availability status.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="author"></param>
        /// <param name="genre"></param>
        /// <param name="year"></param>
        /// <param name="isAvailable"></param>
        public void AddBook(string title, string author, string genre, int year)
        {
            if(string.IsNullOrEmpty(title) || string.IsNullOrEmpty(author) || string.IsNullOrEmpty(genre))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid book information.");
                return;
            }

            else if (bookCount >= books.Length)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cannot add more books to the library.");
            }

            else
            {
                books[bookCount] = new Book(CurrentBookId++, title, author, genre, year);
                bookCount++;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("----Book added successfully----");
            }
            Console.ResetColor();
        }
        #endregion
        #region Register Member
        /// <summary>
        /// Registers a new member to the library with the specified name, email, and premium status.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="isPremium"></param>
        public void RegisterMember(string name, string email, bool isPremium = false)
        {
            if(string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid member information.");
            }

            Member newMember;
            if(isPremium)
                newMember = new PremiumMember(CurrentMemberId++, name, email); 
            
            else
                newMember = new Member(CurrentMemberId++, name, email);

            if (memberCount >= members.Length)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cannot register more members to the library.");
            }
            else
            {
                members[memberCount] = newMember;
                memberCount++;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Member '{newMember.Name}' registered successfully with ID {newMember.Id}.");
            }
            Console.ResetColor();
        }
        #endregion
        #region Borrow Book
        /// <summary>
        /// Allows a member to borrow a book from the library by specifying the member ID, book ID, and borrow date
        /// Marks the book as unavailable and creates a new borrow record
        /// </summary>
        /// <param name="MemberId"></param>
        /// <param name="BookId"></param>
        /// <param name="borrowDate"></param>
        public void BorrowBook(int MemberId, int BookId, DateTime? borrowDate)
        {
            Member? member = members.FirstOrDefault(m => m?.Id == MemberId);
            Book? book = books.FirstOrDefault(b => b?.Id == BookId);
            if(member != null && book != null)
            {
                if(book.IsAvailable)
                {
                    book.IsAvailable = false;
                    borrowRecords[borrowRecordCount] = new BorrowRecord(CurrentBorrowRecordId++, book, member, borrowDate ?? DateTime.Now);
                    borrowRecordCount++;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Book '{book.Title}' borrowed successfully by {member.Name}.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Book is not available for borrowing.");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid member or book ID.");
            }
            Console.ResetColor();
        }
        #endregion
        #region Return Book
        /// <summary>
        /// Allows a member to return a borrowed book by specifying the book ID
        /// Updates the borrow record and marks the book as available
        /// </summary>
        /// <param name="BookId"></param>
        public void ReturnBook(int BookId)
        {
            BorrowRecord? OpenRecord = borrowRecords.FirstOrDefault(br => br?.Book.Id == BookId && br.ReturnDate == null);
            if (OpenRecord != null)
            {
                OpenRecord.ReturnDate = DateTime.Now;
                OpenRecord.Book.IsAvailable = true;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Book '{OpenRecord.Book.Title}' returned successfully.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid book ID or book is not currently borrowed.");
            }
                Console.ResetColor();
        }
        #endregion
        #region Search Member Or Book
        /// <summary>
        /// Searches for members and books that match the given query string
        /// (case-insensitive) and displays their information
        /// </summary>
        /// <param name="query">The query string to search for</param>
        public void SearchMemberOrBook(string query)
        {
            //Here we use LINQ to filter the members and books arrays based on the MatchesQuery method of each object
            // cuase would be more than one member or book that matches the query, we use ToArray() to convert the result to an array
            var matchingMembers = members.Where(m => m != null && m.MatchesQuery(query)).ToArray();
            var matchingBooks = books.Where(b => b != null && b.MatchesQuery(query)).ToArray();
            if (matchingMembers.Length > 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----Matching Members----");
                foreach (var member in matchingMembers)
                {
                    Console.WriteLine(member.GetInfo());
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No matching members found.");
            }
            if (matchingBooks.Length > 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----Matching Books-----");
                foreach (var book in matchingBooks)
                {
                    Console.WriteLine(book.GetInfo());
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No matching books found.");
            }
                Console.ResetColor();
        }
        #endregion
        #region Display Available Books
        /// <summary>
        /// Displays all available books in the library 
        /// by filtering the books array for those that are marked as available
        /// </summary>
        public void DisplayAvailableBooks()
        {
            var availableBooks = books.Where(b => b != null && b.IsAvailable).ToArray();
            if (availableBooks.Length > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("----Available Books----");
                foreach (var book in availableBooks)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{book.GetInfo()}\n");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No available books found.");
            }
            Console.ResetColor();
        }
        #endregion

        #region Member Borrowing Record
        /// <summary>
        /// Displays the borrowing records for a specific member by filtering the borrowRecords array 
        /// for those that match the given member ID
        /// </summary>
        /// <param name="MemberId"></param>
        public void MemberBorrowingRecord(int MemberId)
        {
               
            if (!members.Any(m => m?.Id == MemberId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid member ID.");
                return;
            }

            var MemberBorrowRecords = borrowRecords.Where(br => br != null && br.Member.Id == MemberId).ToArray();
            if (MemberBorrowRecords.Length > 0)
            {
                Member? member = members.FirstOrDefault(m => m.Id == MemberId);
                if (member is PremiumMember)
                    Console.WriteLine("-----Premium Member Records----");
                else
                    Console.WriteLine("-----Regular Member Records----");

                foreach (var record in MemberBorrowRecords)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    string returnDate = record.ReturnDate.HasValue ? record.ReturnDate.Value.ToString("dd/MM/yyyy") : "Not Returned";
                    Console.WriteLine($"Record ID: {record.Id}, Book: {record.Book.Title}, Borrow Date: {record.BorrowDate}, Return Date: {returnDate}");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No borrowing records found for the specified member.");
            }
            Console.ResetColor();
        }
        #endregion

        #region Lateing Report
        /// <summary>
        /// Reports all late returns by filtering the borrowRecords array for those that are late and displaying their information
        /// </summary>
        public void LateingReport()
        {
            var lateRecords = borrowRecords.Where(br => br != null && br.IsLate()).ToArray();
            if (lateRecords.Length > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("----Late Returns----");
                foreach (var record in lateRecords)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    string MemberType = record.Member is PremiumMember ? "Premium Member" : "Regular Member";
                    Console.WriteLine($"Book: {record.Book.Title}, {MemberType}: {record.Member.Name}, Delayed Days: {record.DaysLate()}");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No late returns found.");
            }
            Console.ResetColor();
        }
        #endregion

        #region Data Seeding
        public void SeedData()
        {
            // Add some books
            var booksToSeed = new (string Title, string Author, string Genre, int Year)[]
            {
                ("Design Patterns", "Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides", "Software Architecture", 1994),
                ("Clean Code", "Robert C. Martin", "Software Engineering", 2008),
                ("The Pragmatic Programmer", "Andrew Hunt, David Thomas", "Software Engineering", 1999),
                ("Introduction to Algorithms", "Thomas H. Cormen", "Computer Science", 2009),
                ("CLR via C#", "Jeffrey Richter", "Programming Languages", 2012),
                ("Refactoring", "Martin Fowler", "Software Engineering", 1999)
            };

            foreach (var b in booksToSeed)
            {
                AddBook(b.Title, b.Author, b.Genre, b.Year);
            }

            // Register some members
            var membersToSeed = new (string Name, string Email, bool IsPremium)[]
            {
                ("Ahmed Mahmoud", "ahmed.mahmoud@example.com", false),
                ("Mohamed Mostafa", "mohamed.m@example.com", false),
                ("Mariam Ibrahim", "mariam.ibrahim@example.com", false),
                ("Sara Hassan", "sara.hassan@example.com", true),
                ("Omar Khalid", "omar.khalid@example.com", true)
            };

            foreach (var m in membersToSeed)
            {
                RegisterMember(m.Name, m.Email, m.IsPremium);
            }

            // Borrow some books
            var borrowsToSeed = new (int MemberId, int BookId, DateTime BorrowDate)[]
            {
                 (1, 1, DateTime.Now.AddDays(-5)),  // this is not late (regular member < 14 days)
                 (2, 2, DateTime.Now.AddDays(-20)), // this is late (regular member > 14 days)
                 (4, 3, DateTime.Now.AddDays(-20)), // this is not late (premium member < 30 days)
                 (5, 4, DateTime.Now.AddDays(-40))  // this is late (premium member > 30 days)
            };

            foreach (var br in borrowsToSeed)
            {
                BorrowBook(br.MemberId, br.BookId, br.BorrowDate);
            }
            Console.Clear();
        }
        #endregion



    }
}
