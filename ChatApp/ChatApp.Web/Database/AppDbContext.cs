using Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Web
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("Data Source=localhost\\SQLEXPRESS;Initial Catalog=chatAppDB3;Integrated Security=True")
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
