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

        public override string GetInfo()
        {
            throw new NotImplementedException();
        }

        public bool MatchesQuery(string query)
        {
            throw new NotImplementedException();
        }
    }
}
