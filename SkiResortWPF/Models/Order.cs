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
        public String OrderCode { get => String.Format("{0}/{1}", ClientId, CreationDate.ToString("dd.MM.yyyy")); }
        public DateTime CreationDate { get; set; }

        // [Column(TypeName = "TIME")]
        // public Nullable<DateTime> OrderTime { get; set; }
        public int ClientId { get; set; }
        public byte? Status { get; set; }
        public Nullable<DateTime> CloseDate { get; set; }
        public int TimeCount { get; set; }
        public virtual Client Client { get; set; }
        public virtual ICollection<OrderService> OrderServices { get; set; }
        public Order()
        {

        }
        public Order(DateTime dateTime)
        {
            if (CreationDate == null)
                return;
            CreationDate = dateTime;
        }
    }
}
