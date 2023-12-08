using Exercise.Models;
using Microsoft.EntityFrameworkCore;

namespace Exercise.DataBase
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

       
    }

}
