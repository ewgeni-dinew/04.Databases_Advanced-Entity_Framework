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
                var departments = dbContext
                    .Departments
                    .Where(d => d.Employees.Count > 5)
                    .OrderBy(e => e.Employees.Count)
                    .ThenBy(d => d.Name)
                    .Select(d => new
                    {
                        d.Name,
                        d.Manager,
                        d.Employees
                    })
                    .ToList();

                foreach (var dep in departments)
                {
                    Console.WriteLine($"{dep.Name} - {dep.Manager.FirstName} {dep.Manager.LastName}");

                    foreach (var emp in dep.Employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
                    {
                        Console.WriteLine($"{emp.FirstName} {emp.LastName} - {emp.JobTitle}");
                    }
                    Console.WriteLine(new string('-', 10));
                }
            }
        }
    }
}
