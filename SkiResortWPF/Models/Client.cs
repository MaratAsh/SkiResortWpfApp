using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SkiResortWPF.Models
{
    [Table("Clients")]
    public class Client
    {
        [Key]
        [Required]
        public int id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Patronymic { get; set; }
        public int PassportSeries { get; set; }
        public int PassportNumber { get; set; }
        public DateTime Birthday { get; set; }
        public String Address { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }

        public bool CheckPassword(string Password)
        {
            return this.Password == Password;
        }
    }
}
