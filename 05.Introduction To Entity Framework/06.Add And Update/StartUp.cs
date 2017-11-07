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
                Address address = new Address
                {
                    AddressText = "Vitoshka 15",
                    TownId = 4
                };

                var employee = dbContext
                    .Employees
                    .FirstOrDefault(e => e.LastName.Equals("Nakov"));

                employee.Address = address;
                dbContext.SaveChanges();

                var employeesAddresses = dbContext.Employees
                    .Select(e => e.Address)
                    .OrderByDescending(a => a.AddressId)
                    .Take(10)
                    .Select(a => a.AddressText)
                    .ToList();

                foreach (string empAddress in employeesAddresses)
                {
                    Console.WriteLine(empAddress);
                }
            }
        }
    }
}
