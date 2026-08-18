using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public abstract class LibraryItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime AddedDate { get; set; }

        public abstract string GetInfo();
    }
}
