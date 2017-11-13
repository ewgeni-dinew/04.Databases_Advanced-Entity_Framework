using System;
using System.Collections.Generic;
using System.Text;

namespace P01_StudentSystem.Data
{
    public class Configuration
    {
        public static string ConnectionStr { get; set; } = "Server=.;Database=StudentSystem;Integrated Security=True";
    }
}
