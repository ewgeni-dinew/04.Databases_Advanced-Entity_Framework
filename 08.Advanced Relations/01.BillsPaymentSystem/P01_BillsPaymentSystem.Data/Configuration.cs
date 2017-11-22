using System;
using System.Collections.Generic;
using System.Text;

namespace P01_BillsPaymentSystem.Data
{
    internal class Configuration
    {
        internal static string ConnectString { get; set; } = "Server=.;Database=BillsPaymentSystem;Integrated Security=True";
    }
}
