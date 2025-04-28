using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMauiApp.Data.Models
{
    [Table("Task")]
    public class Task
    {
        public int Id { get; set; } // Auto-increment by default in SQLite
        public string TaskDescription { get; set; }
        public int Completed { get; set; }
    }
}
