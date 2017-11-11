using P03_SalesDatabase.Data;
using System;

namespace SalesDbStartUp
{
    class StartUp
    {
        static void Main()
        {
            var db = new SalesContext();

            using (db)
            {
                db.Database.EnsureCreated();
            }
        }
    }
}
