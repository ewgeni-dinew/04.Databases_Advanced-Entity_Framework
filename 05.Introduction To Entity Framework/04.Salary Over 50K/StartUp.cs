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
                    .Where(e => e.Salary > 50000)
                    .OrderBy(e => e.FirstName)
                    .ToList();

                foreach (var em in employees)
                {
                    Console.WriteLine(em.FirstName);
                }
            }
        }
    }
}
