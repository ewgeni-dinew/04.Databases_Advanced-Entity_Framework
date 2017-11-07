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
                var employeesProjects = dbContext.Employees
                    .Include(p => p.EmployeesProjects)
                    .ThenInclude(p => p.Project)
                    .Where(e => e.EmployeesProjects.Any(p => p.Project.StartDate.Year >= 2001 &&
                      p.Project.StartDate.Year <= 2003))
                    .Take(30)
                    .ToList();

                foreach (var employee in employeesProjects)
                {
                    var managerId = employee.ManagerId;
                    var manager = dbContext.Employees.Find(managerId);

                    Console.WriteLine($"{employee.FirstName} {employee.LastName} " +
                        $"– Manager: {manager.FirstName} {manager.LastName}");

                    foreach (var project in employee.EmployeesProjects)
                    {
                        string format = "M/d/yyyy h:mm:ss tt";

                        var startDate = project.Project.StartDate.ToString(format, null);
                        var endDate = project.Project.EndDate.ToString();

                        if (string.IsNullOrWhiteSpace(endDate))
                        {
                            endDate = "not finished";
                        }
                        else
                        {
                            endDate = project.Project.EndDate.Value.ToString(format, null);
                        }
                        Console.WriteLine($"--{project.Project.Name} - {startDate} - {endDate}");
                    }
                }
            }
        }
    }
}
