using System;

namespace TestApp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
