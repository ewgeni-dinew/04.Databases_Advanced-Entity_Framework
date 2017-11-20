using P03_FootballBetting.Data;
using P03_FootballBetting.Data.Models;
using System;

namespace FootballBetting.App
{
    public class StartUp
    {
        static void Main()
        {
            using (var db = new FootballBettingContext())
            {
                db.Database.EnsureCreated();
            }
        }
    }
}
