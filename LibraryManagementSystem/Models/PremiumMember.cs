using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class PremiumMember : Member
    {
        public int MaxBorrowLimit { get; set; } = 10;
        public int LoanDays { get; set; } = 30;

        public PremiumMember(int id, string name, string email) : base(id, name, email)
        {
            
        }

        /// <summary>
        /// Overrides the GetInfo method to provide detailed information about the premium member,
        /// including their name, email, join date, maximum borrow limit, loan days,
        /// and the number of books currently borrowed.
        /// </summary>
        /// <returns>string value containing the member's information</returns>
        public override string GetInfo()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            return $"Premium Member: {Name}, Email: {Email}, Join Date: {JoinDate.ToShortDateString()}," +
                $" Max Borrow Limit: {MaxBorrowLimit}, Loan Days: {LoanDays}, Number of Books Borrowed: {BorrowedBook.Length}";
        }
    }
}
