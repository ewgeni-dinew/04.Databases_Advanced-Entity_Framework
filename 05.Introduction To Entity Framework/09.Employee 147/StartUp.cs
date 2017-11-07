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
                var employee = dbContext
                     .Employees
                     .Include(e => e.EmployeesProjects)
                     .ThenInclude(e => e.Project)
                     .FirstOrDefault(e => e.EmployeeId == 147);

                Console.WriteLine($"{employee.FirstName} {employee.LastName} -" +
                    $" {employee.JobTitle}");

                foreach (var project in employee
                    .EmployeesProjects
                    .OrderBy(p => p.Project.Name))
                {
                    Console.WriteLine(project.Project.Name);
                }
            }
        }
    }
}
