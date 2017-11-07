using System;
using System.Linq;
using P02_DatabaseFirst.Data;

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
                    .OrderBy(e => e.EmployeeId)
                    .ToList();

                foreach (var em in employees)
                {
                    Console.WriteLine($"{em.FirstName} {em.LastName} {em.MiddleName} " +
                        $"{em.JobTitle} {em.Salary}:F2");
                }
            }
        }
    }
}
