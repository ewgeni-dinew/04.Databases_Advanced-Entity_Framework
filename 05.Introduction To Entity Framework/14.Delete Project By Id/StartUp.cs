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
                Project projectToBeDeleted = dbContext.Projects.Find(2);

                var employeesWorkingOnTheDeletedProject = dbContext.Employees
                    .Include(e => e.EmployeesProjects)
                    .ThenInclude(ep => ep.Project)
                    .ToList();

                foreach (var emp in employeesWorkingOnTheDeletedProject)
                {
                    foreach (var ep in emp.EmployeesProjects.ToList())
                    {
                        if (ep.Project.Equals(projectToBeDeleted))
                        {
                            emp.EmployeesProjects.Remove(ep);
                        }
                    }
                }

                dbContext.Projects.Remove(projectToBeDeleted);
                dbContext.SaveChanges();

                foreach (Project project in dbContext.Projects.Take(10))
                {
                    Console.WriteLine(project.Name);
                }
            }
        }
    }
}
