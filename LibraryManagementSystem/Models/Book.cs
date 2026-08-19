using LibraryManagementSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class Book : LibraryItem, ISearchable
    {
        public string Author { get; set; } = string.Empty;
        public string Genere { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public int Year { get; set; }

        public Book(int id, string title, string author, string genre, int year) : base(id, title)
        {
            Author = author;
            Genere = genre;
            Year = year;
            IsAvailable = true;
        }

        /// <summary>
        /// Returns a string representation of the book's information,
        /// including title, author, genre, year, and availability status.
        /// </summary>
        /// <returns>string value containing the book's information</returns>
        public override string GetInfo()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            string Status = IsAvailable ? "Available" : "Not Available";
            return $"Id: {Id}, Title: {Title}, Author: {Author}, Genere: {Genere}, Year: {Year}, Status: {Status}";
        }

        /// <summary>
        /// Checks if the book's title, author, or genre matches the given query string (case-insensitive).
        /// </summary>
        /// <param name="query"></param>
        /// <returns>Boolean value indicating if the book matches the query</returns>
        public bool MatchesQuery(string query)
        {
            if(string.IsNullOrEmpty(query)) return false;
            return Title.Contains(query, StringComparison.OrdinalIgnoreCase)
                    || Author.Contains(query, StringComparison.OrdinalIgnoreCase)
                    || Genere.Contains(query, StringComparison.OrdinalIgnoreCase);
        }
    }
}
