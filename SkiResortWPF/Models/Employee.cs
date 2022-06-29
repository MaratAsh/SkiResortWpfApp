using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiResortWPF.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Patronymic { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String Position { get; set; }
        public virtual ICollection<LogActivityEmployee> LogActivityEmployees { get; set; }
        public bool CheckPassword(string Password)
        {
            return this.Password == Password;
        }
    }
}
