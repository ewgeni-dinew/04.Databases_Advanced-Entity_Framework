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
                var addresses = dbContext.Addresses
                    .Include(a => a.Employees)
                    .Include(a => a.Town)
                    .OrderByDescending(e => e.Employees.Count)
                    .ThenBy(t => t.Town.Name)
                    .Take(10)
                    .ToList();

                foreach (var adr in addresses)
                {
                    Console.WriteLine($"{adr.AddressText}, {adr.Town.Name} - {adr.Employees.Count} employees");
                }

            }
        }
    }
}
