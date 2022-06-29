using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiResortWPF.Models
{
    [Table("LogActivityEmployees")]
    public class LogActivityEmployee
    {
        [Key]
        [Required]
        [Column(Order = 1)]
        public int EmployeeId { get; set; }
        [Key]
        [Required]
        [Column(Order = 2)]
        public DateTime Date { get; set; }
        [Column("Success")]
        public byte? SuccessByte { get; set; }
        public LogActivityEmployee(int EmployeeId, bool success)
        {
            this.EmployeeId = EmployeeId;
            this.Success = success;
            this.Date = DateTime.Now;
        }
        public LogActivityEmployee()
        {

        }
        [NotMapped]
        public bool Success
        {
            get => SuccessByte > 0;
            set { this.SuccessByte = (byte)(value ? 1 : 0); }
        }
        public virtual Employee Employee { get; set; }
    }
}
