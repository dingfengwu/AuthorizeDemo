using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Db.Model;
using System.Data.Entity;

namespace WebApplication3.Db
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext() : base() { }

        public ApplicationDbContext(string connectionString) : base(connectionString) { }

        public DbSet<User> Users { get; set; }

    }
}
