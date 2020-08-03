using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using TestApp.Models;

namespace TestApp
{
    static class SalaryCalculator
    {
        private static double eployeeYearRate = 0.03;
        private static double eployeeMaxYearRate = 0.3;
        private static double managerYearRate = 0.05;
        private static double managerMaxYearRate = 0.4;
        private static double managerSubordinateRate = 0.005;
        private static double salesmanYearRate = 0.01;
        private static double salesmanMaxYearRate = 0.35;
        private static double salesmanSubordinateRate = 0.003;
        public static double GetEployeeSalary(int employeeID, DateTime dateTime)
        {
            Employee employee = GetEmployee(employeeID);
            Group group = GetEmployeeGroup(employee.GroupId);
            int workingYears = GetWorkingYears(dateTime, employee.DateOfEmployment);

            switch (employee.GroupId)
            {
                case 1:
                    double rate = (workingYears * eployeeYearRate) < eployeeMaxYearRate ? (workingYears * eployeeYearRate) : eployeeMaxYearRate;
                    double salary = group.BaseSalary + group.BaseSalary * rate;
                    return salary;
                case 2:
                    rate = (workingYears * managerYearRate) < managerMaxYearRate ? (workingYears * managerYearRate) : managerMaxYearRate;
                    double subordinatesSalary = GetSubordinatesSalary(employeeID, dateTime);
                    salary = group.BaseSalary + group.BaseSalary * rate + subordinatesSalary * managerSubordinateRate;
                    return salary;
                case 3:
                    rate = (workingYears * salesmanYearRate) < salesmanMaxYearRate ? (workingYears * salesmanYearRate) : salesmanMaxYearRate;
                    subordinatesSalary = GetSubordinatesSalary(employeeID, dateTime);
                    salary = group.BaseSalary + group.BaseSalary * rate + subordinatesSalary * salesmanSubordinateRate;
                    return salary;
            }
            return 0;
        }

        private static double GetSubordinatesSalary(int employeeID, DateTime dateTime) {
            using (var db = new EmployeesContext())
            {
                double subordinateSsalary = 0;
                var subordinates = db.Relations.Where(p => p.SupervisorId == employeeID);

                foreach (Relation subordinate in subordinates)
                {
                    subordinateSsalary += GetEployeeSalary(subordinate.SubordinateId, dateTime);
                }

                return subordinateSsalary;
            }
        }

        private static int GetWorkingYears(DateTime dateTime, DateTime dateOfEmployment)
        {
            return (dateTime.Year - dateOfEmployment.Year) > 0 ? (dateTime.Year - dateOfEmployment.Year) : 0;
        }

        private static Employee GetEmployee(int employeeID) 
        {
            using (var db = new EmployeesContext())
            {
                return db.Employees.Find(employeeID);
            }
        }

        private static Group GetEmployeeGroup(int groupID)
        {
            using (var db = new EmployeesContext())
            {
                return db.Groups.Find(groupID);
            }
        }

        public static double TotalSalary()
        {
            double salary = 0;

            using (var db = new EmployeesContext())
            {
                foreach (Employee employee in db.Employees)
                {
                    salary += GetEployeeSalary(employee.Id, new DateTime(2050, 1, 1));
                }
            }

            return salary;
        }
    }
}
