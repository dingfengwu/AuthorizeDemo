using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Db.Model
{
    public class User : IEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

    }
}
