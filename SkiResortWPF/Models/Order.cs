using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiResortWPF.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [NotMapped]
        public String OrderCode { get => ""; }
        public DateTime CreationDate { get; set; }
        public DateTime OrderTime { get; set; }
        public int ClientId { get; set; }
        public byte? Status { get; set; }
        public DateTime CloseDate { get; set; }
        public int TimeCount { get; set; }
        public virtual Client Client { get; set; }
    }
}
