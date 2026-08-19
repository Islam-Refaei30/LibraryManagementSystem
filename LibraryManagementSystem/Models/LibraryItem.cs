using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public abstract class LibraryItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime AddedDate { get; set; }

        protected LibraryItem(int id, string title)
        {
            Id = id;
            Title = title;
            AddedDate = DateTime.Now;
        }

        public abstract string GetInfo();
    }
}
