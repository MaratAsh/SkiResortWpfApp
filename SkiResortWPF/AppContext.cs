using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiResortWPF
{
    public class AppContext: DbContext
    {
        public AppContext() : base("DefaultConnection")
        {

        }
        public DbSet<Models.Client> Clients { get; set; }
    }
}
