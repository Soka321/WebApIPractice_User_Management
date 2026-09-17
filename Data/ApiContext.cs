using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApIPractice.Models;

namespace WebApIPractice.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }

        public DbSet<User> users { get; set; }
        //public DBSet<User> users { get; set; }
    }
}
