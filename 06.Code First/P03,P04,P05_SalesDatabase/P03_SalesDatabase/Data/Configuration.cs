using System;
using System.Collections.Generic;
using System.Text;

namespace P03_SalesDatabase.Data
{
    public class Configuration
    {
        public static string ConnectionString { get; set; } = "Server=.;Database=SalesDatabase;Integrated Security=True";
    }
}
