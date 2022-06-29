using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiResortWPF.Models
{
    [Table("OrderServices")]
    public class OrderService
    {
        [Key]
        [Required]
        [Column(Order = 1)]
        public int OrderId { get; set; }
        [Key]
        [Required]
        [Column(Order = 2)]
        public int ServiceId { get; set; }
        public virtual Order Order { get; set; }
        public virtual Service Service { get; set; }
    }
}
