using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using WebApplication3.Db.Model;

namespace WebApplication3.Db
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext() : base() { }

        public DbSet<User> Users { get; set; }

    }
}
