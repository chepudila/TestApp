using System.Collections.Generic;

namespace TestApp.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double BaseSalary { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public Group()
        {
            Employees = new List<Employee>();
        }
    }
}
