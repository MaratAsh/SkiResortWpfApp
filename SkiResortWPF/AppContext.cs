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
        public DbSet<Models.Employee> Employees { get; set; }
        public DbSet<Models.LogActivityEmployee> LogActivityEmployees { get; set; }
        public DbSet<Models.Service> Services { get; set; }
    }
}
