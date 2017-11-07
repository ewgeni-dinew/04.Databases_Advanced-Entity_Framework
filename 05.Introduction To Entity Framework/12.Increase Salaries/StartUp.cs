using System;
using System.Linq;
using P02_DatabaseFirst.Data;
using Microsoft.EntityFrameworkCore;
using P02_DatabaseFirst.Data.Models;

namespace P02_DatabaseFirst
{
    public class StartUp
    {
        public static void Main()
        {
            var dbContext = new SoftUniContext();

            using (dbContext)
            {
                var employees = dbContext.Employees
                    .Include(d => d.Departments)
                    .Where(d => d.Department.Name == "Engineering" || d.Department.Name == "Tool Design"
                    || d.Department.Name == "Marketing" || d.Department.Name == "Information Services")
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName)
                    .ToList();

                foreach (var emp in employees)
                {
                    emp.Salary *= 1.12m;
                    Console.WriteLine($"{emp.FirstName} {emp.LastName} (${emp.Salary:f2})");
                }
            }
        }
    }
}
