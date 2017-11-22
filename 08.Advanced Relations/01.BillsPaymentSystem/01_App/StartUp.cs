using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;
using System;
using System.Linq;

namespace _01_App
{
    public class StartUp
    {
        static void Main()
        {
            var db = new BillsPaymentSystemContext();
            using(db){

                //db.Database.EnsureDeleted();
                //db.Database.EnsureCreated();

                //Task 02->
                //Seed(db);
                //<--

                //Task 03->
                var inputUserId = int.Parse(Console.ReadLine());

                var user = db.Users
                    .Where(u => u.UserId == inputUserId)
                    .Select(u => new
                    {
                        Name = $"{u.FirstName} {u.LastName}",

                        CreditCards = u.PaymentMethods
                        .Where(pm => pm.Type == PaymentMethodType.CreditCard)
                        .Select(pm => pm.CreditCard)
                        .ToList(),

                        BankAccounts = u.PaymentMethods
                        .Where(pm => pm.Type == PaymentMethodType.BankAccount)
                        .Select(pm => pm.BankAccount)
                        .ToList()
                    })
                    .FirstOrDefault();

                var bankAccounts = user.BankAccounts;
                var creditCards = user.CreditCards;

                Console.WriteLine($"User: {user.Name}");

                if (bankAccounts.Any())
                {
                    Console.WriteLine("Bank Accounts:");
                    foreach (var ba in bankAccounts)
                    {
                        Console.WriteLine($"--ID: {ba.BankAccountId}");
                        Console.WriteLine($"--- Balance: {ba.Balance:f2}");
                        Console.WriteLine($"--- Bank: {ba.BankName}");
                        Console.WriteLine($"--- SWIFT: {ba.SwiftCode}");
                    }
                }

                if (creditCards.Any())
                {
                    Console.WriteLine("Credit Cards:");
                    foreach (var cc in creditCards)
                    {
                        Console.WriteLine($"--ID: {cc.CreditCardId}");
                        Console.WriteLine($"--- Limit: {cc.Limit:f2}");
                        Console.WriteLine($"--- Money Owed: {cc.MoneyOwed:f2}");
                        Console.WriteLine($"--- Limit Left: {cc.LimitLeft:f2}");
                        Console.WriteLine($"--- Expiration Date: {cc.ExpirationDate.Year}/{cc.ExpirationDate.Month}");
                    }
                }
                //<-  
            }
        }

        //Task 02->
        private static void Seed(BillsPaymentSystemContext db)
        {
            using (db)
            {
                var user = new User()
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "john.smith@example.com",
                    Password = "dlkjfsdhg"
                };

                var creditCards = new CreditCard[]
                {
                    new CreditCard()
                    {
                        ExpirationDate=DateTime.ParseExact("11/02/1978","dd/MM/yyyy",null),
                        Limit=1000m,
                        MoneyOwed=50m
                    },

                    new CreditCard()
                    {
                        ExpirationDate=DateTime.ParseExact("21/10/1979","dd/MM/yyyy",null),
                        Limit=4000m,
                        MoneyOwed=250m
                    }
                };

                var bankAccount = new BankAccount()
                {
                    Balance = 10000m,
                    BankName = "Swiss Bank",
                    SwiftCode = "SWSSBANK"
                };

                var paymentMethods = new PaymentMethod[]
                {
                    new PaymentMethod()
                    {
                        User=user,
                        CreditCard=creditCards[0],
                        Type=PaymentMethodType.CreditCard
                    },

                    new PaymentMethod()
                    {
                        User=user,
                        CreditCard=creditCards[1],
                        Type=PaymentMethodType.CreditCard
                    },

                    new PaymentMethod()
                    {
                        User=user,
                        BankAccount=bankAccount,
                        Type=PaymentMethodType.BankAccount
                    }
                };

                db.Users.Add(user);
                db.CreditCards.AddRange(creditCards);
                db.BankAccounts.Add(bankAccount);
                db.PaymentMethods.AddRange(paymentMethods);

                db.SaveChanges();
            }
        }
        //<-
    }
}
