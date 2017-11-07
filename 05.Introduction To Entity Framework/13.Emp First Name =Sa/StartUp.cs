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
                var employees = dbContext
                    .Employees
                    .Include(e => e.Department)
                    .Where(e => e.FirstName.StartsWith("Sa"))
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName)
                    .ToList();

                foreach (var emp in employees)
                {
                    if (emp.Department.Name == "Engineering" || emp.Department.Name == "Tool Design"
                    || emp.Department.Name == "Marketing" || emp.Department.Name == "Information Services")
                    {
                        emp.Salary *= 1.12m;
                    }
                    Console.WriteLine($"{emp.FirstName} {emp.LastName} - {emp.JobTitle}" +
                        $" - (${emp.Salary:f2})");
                }
            }
        }
    }
}
