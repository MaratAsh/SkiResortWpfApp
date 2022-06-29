using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiResortWPF.Models
{
    [Table("Services")]
    public class Service
    {
        [Key]
        public int Id { get; set; }
        public String Title { get; set; }
        public String Code { get; set; }
        public Decimal Cost { get; set; }

        [NotMapped]
        public String CostStringFirst { get => ((int)Cost).ToString(); }
        [NotMapped]
        public String CostStringSecond { get => ((int) (Cost * 10 % 10)).ToString() + ((int)(Cost * 100 % 10)).ToString(); }
    }
}
