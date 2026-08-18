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
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime JoinDate { get; set; }
        public Bookk[] BorrowedBook { get; set; }
        public bool MatchesQuery(string query)
        {
            throw new NotImplementedException();
        }
        
        public virtual string GetInfo()
        {
            throw new NotImplementedException();
        }
    }
}
