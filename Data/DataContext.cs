using Microsoft.EntityFrameworkCore;
using ss_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ss_backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
