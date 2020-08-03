using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TestApp.Models
{
    public class EmployeesContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Relation> Relations { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=staff.db");
    }
}
