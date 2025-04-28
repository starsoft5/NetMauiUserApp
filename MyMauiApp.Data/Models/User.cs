using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMauiApp.Data.Models
{
    [Table("Users")]
    public class User
    {
        public int Id { get; set; } // Auto-increment by default in SQLite
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
