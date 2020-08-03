using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using TestApp.Models;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new EmployeesContext())
            {
                /*db.Add(new Group { Name = "Employee" , BaseSalary = 100 });
                db.Add(new Group { Name = "Manager", BaseSalary = 200 });
                db.Add(new Group { Name = "Salesman", BaseSalary = 300 });

                db.Add(new Employee { Name = "Vasya", DateOfEmployment = new DateTime(2020, 1, 1), GroupId = 1 });
                db.Add(new Employee { Name = "Petya", DateOfEmployment = new DateTime(2010, 1, 1), GroupId = 2 });
                db.Add(new Employee { Name = "Kolya", DateOfEmployment = new DateTime(2000, 1, 1), GroupId = 3 });
                db.SaveChanges();

                db.Add(new Relation { SupervisorId = 3, SubordinateId = 2 });
                db.Add(new Relation { SupervisorId = 2, SubordinateId = 1 });
                db.SaveChanges();*/

                Console.WriteLine("Группы:");

                foreach (Group group in db.Groups)
                {
                    Console.WriteLine(group.Id + " " + group.Name + " " + group.BaseSalary);
                }

                Console.WriteLine("Работники:");

                foreach (Employee employee in db.Employees)
                {
                    Console.WriteLine(employee.Id + " " + employee.Name + " " + employee.DateOfEmployment + " " + employee.GroupId);
                }

                Console.WriteLine("Отношения:");

                foreach (Relation relation in db.Relations)
                {
                    Console.WriteLine(relation.Id + " " + relation.SupervisorId + " " + relation.SubordinateId);
                }

                Console.WriteLine("--------------------------------------");

                var employees = db.Employees.Join(db.Groups,
                    p => p.Id,
                    c => c.Id,
                    (p, c) => new // результат
                    {
                        Name = p.Name,
                        Group = c.Name,
                        BaseSalary = c.BaseSalary
                    });
                foreach (var employee in employees)
                {
                    Console.WriteLine("{0} - {1} - {2}", employee.Name, employee.Group, employee.BaseSalary);
                }
            }

            Console.WriteLine("--------------------------------------");

            Console.WriteLine(SalaryCalculator.GetEployeeSalary(1, new DateTime(2050, 1, 1)));

            Console.WriteLine(SalaryCalculator.GetEployeeSalary(2, new DateTime(2050, 1, 1)));

            Console.WriteLine(SalaryCalculator.GetEployeeSalary(3, new DateTime(2050, 1, 1)));

            Console.WriteLine("--------------------------------------");

            Console.WriteLine("все сотрудники компании и их зарплаты за полгода(месяца горизонтально)");

            Console.WriteLine(SalaryCalculator.TotalSalary());

            Console.ReadKey();
        }
    }
}
