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
        }
    }
}
