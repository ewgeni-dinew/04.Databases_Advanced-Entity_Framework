using System;
using System.Collections.Generic;
using System.Text;

namespace ProductShop.Data
{
    public class DbConfigure
    {
        public static string ConnectionStr { get; set; } = 
            "Server=DESKTOP-B6V15QN\\SQLEXPRESS;Database=ProductsShop;Integrated Security=True";
    }
}
