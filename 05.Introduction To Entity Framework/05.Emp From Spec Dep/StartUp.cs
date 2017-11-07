using System;
using System.Linq;
using P02_DatabaseFirst.Data;
using Microsoft.EntityFrameworkCore;

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
                    .Where(d => d.DepartmentId == 6)
                    .OrderBy(e => e.Salary)
                    .ThenByDescending(e => e.FirstName)
                    .ToList();

                foreach (var em in employees)
                {
                    Console.WriteLine($"{em.FirstName} {em.LastName} from Research and Development - ${em.Salary:f2}");
                }
            }
        }
    }
}
