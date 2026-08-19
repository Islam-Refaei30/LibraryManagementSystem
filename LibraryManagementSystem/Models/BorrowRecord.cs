using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class BorrowRecord
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public Member Member { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public BorrowRecord(int id, Book book, Member member, DateTime borrowDate)
        {
            Id = id;
            Book = book;
            Member = member;
            BorrowDate = borrowDate;
            ReturnDate = null;
        }

        /// <summary>
        /// Determines if the borrowed book is returned late based on the member type and allowed loan days.
        /// </summary>
        /// <returns>Boolean value indicating if the book is late</returns>
        internal bool IsLate()
        {
            return (DateTime.Now - BorrowDate).TotalDays > AllowedDays();
        }
        internal int DaysLate()
        {
            int daysLate = (int)(DateTime.Now - BorrowDate).TotalDays - AllowedDays();
            return daysLate > 0 ? daysLate : 0;
        }

        private int AllowedDays()
        {
            return (this.Member is PremiumMember premiumMember) ? premiumMember.LoanDays : 14;
        }
    }
}
