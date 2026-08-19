using LibraryManagementSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class Member : ISearchable
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime JoinDate { get; set; }
        public Book[] BorrowedBook { get; set; } = [];
        public Member(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
            JoinDate = DateTime.Now;
        }


        /// <summary>
        /// Checks if the member's name or email matches the given query string (case-insensitive).
        /// </summary>
        /// <param name="query"></param>
        /// <returns>Boolean value indicating if the member matches the query</returns>
        public bool MatchesQuery(string query)
        {
            if(string.IsNullOrEmpty(query)) return false;
            return Name.Contains(query, StringComparison.OrdinalIgnoreCase) || Email.Contains(query, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Returns a string representation of the member's information,
        /// including name, email, join date, and number of borrowed books.
        /// </summary>
        /// <returns>string value containing the member's information</returns>
        public virtual string GetInfo()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            return $"Regular Member\nName: {Name}, Email: {Email}, Join Date: {JoinDate.ToShortDateString()}" +
                $", Number of Books Borrowed: {BorrowedBook.Length}";
        }
    }
}
