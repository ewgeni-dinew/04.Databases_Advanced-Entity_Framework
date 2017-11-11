using P01_HospitalDatabase.Data;
using P01_HospitalDatabase.Data.Models;
using System;

namespace HospitalStartUp
{
    public class Program
    {
        static void Main()
        {
            //Installed Microsoft.EntityFrameworkCore.Tools
            var dbContext = new HospitalContext();
            using (dbContext)
            {
                dbContext.Database.EnsureCreated();
            }
        }
    }
}
