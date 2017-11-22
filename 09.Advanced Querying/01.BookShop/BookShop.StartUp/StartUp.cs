namespace BookShop
{
    using System;
    using BookShop.Data;
    using BookShop.Initializer;
    using BookShop.Models;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using System.Text.RegularExpressions;

    public class StartUp
    {
        public static void Main()
        {
            var dbContext = new BookShopContext();
            using (dbContext) {

                //--01.Age Restriction;
                //var inputCommand = Console.ReadLine().ToLower();
                //Console.WriteLine(GetBooksByAgeRestriction(dbContext, inputCommand));

                //--02.Golden Books
                //Console.WriteLine(GetGoldenBooks(dbContext));

                //--03.Books by Price
                //Console.WriteLine(GetBooksByPrice(dbContext));

                //--04.Not Released In
                //var inputYear = int.Parse(Console.ReadLine());
                //Console.WriteLine(GetBooksNotRealeasedIn(dbContext,inputYear));

                //--05.Book Titles by Category
                //var inputLine = Console.ReadLine();
                //Console.WriteLine(GetBooksByCategory(dbContext,inputLine));

                //--06.Released Before Date
                //var input = Console.ReadLine();
                //Console.WriteLine(GetBooksReleasedBefore(dbContext,input));

                //--07.Author Search
                //var input = Console.ReadLine();
                //Console.WriteLine(GetAuthorNamesEndingIn(dbContext,input));

                //--08.Book Search
                //var input =Console.ReadLine();
                //Console.WriteLine(GetBookTitlesContaining(dbContext,input));

                //--09.Author Search
                //var input = Console.ReadLine();
                //Console.WriteLine(GetBooksByAuthor(dbContext,input));

                //--10.Count Books
                //var input = int.Parse(Console.ReadLine());
                //Console.WriteLine(CountBooks(dbContext,input));

                //--11.Total Book Copies
                //Console.WriteLine(CountCopiesByAuthor(dbContext));

                //--12.Profit by Category
                //Console.WriteLine(GetTotalProfitByCategory(dbContext));

                //--13.Most Recent Books
                //Console.WriteLine(GetMostRecentBooks(dbContext));

                //--14.Increase Prices
                //IncreasePrices(dbContext);

                //--15.Remove Books
                //Console.WriteLine(RemoveBooks(dbContext));
            }
        }

        public static string GetBooksByAgeRestriction(BookShopContext context,string command)
        {
            int value;

            switch (command)
            {
                case "minor":value = 0;break;
                case "teen": value = 1;break;
                case "adult": value = 2;break;
                default:value = -1;break;
            }

            var books = context.Books
                .Where(e => e.AgeRestriction == (AgeRestriction)value)
                .Select(e => e.Title)
                .OrderBy(e => e)
                .ToList();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.EditionType == (EditionType)2 && b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .Select(b => new { b.Price, b.Title })
                .ToList();

            var listedBooks = new List<string>();
            foreach (var book in books)
            {
                var currentStr = $"{book.Title} - ${book.Price:f2}";
                listedBooks.Add(currentStr);
            }

            return string.Join(Environment.NewLine, listedBooks);
        }

        public static string GetBooksNotRealeasedIn(BookShopContext context,int year)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b=>b.Title)
                .ToList();

            return string.Join(Environment.NewLine,books);
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var categoryInput = input
                .ToLower()
                .Split(' ')
                .ToList();

            var books = context.Books
                .Where(b => b.BookCategories
                    .Select(bc => bc.Category
                        .Name
                        .ToLower())
                    .Intersect(categoryInput)
                    .Any())
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToList();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var convertedStr = DateTime.ParseExact(date, "dd-MM-yyyy", null);
            var books = context.Books
                .Where(b => b.ReleaseDate < convertedStr)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => new { b.Title, b.EditionType, b.Price })
                .ToList();

            var listedBooks = new List<string>();
            foreach (var book in books)
            {
                var currentLine = $"{book.Title} - {book.EditionType} - ${book.Price:f2}";
                listedBooks.Add(currentLine);
            }

            return string.Join(Environment.NewLine, listedBooks);
        }

        public static string GetBookTitlesContaining(BookShopContext context,string input)
        {
            var books = context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .OrderBy(b=>b.Title)
                .Select(b => b.Title)
                .ToList();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetBooksByAuthor(BookShopContext context,string input)
        {
            string pattern = $@"^{input}.*$";

            var books = context.Books
                .Where(b => Regex.IsMatch(b.Author.LastName, pattern, RegexOptions.IgnoreCase))
                .OrderBy(b => b.BookId)
                .Select(b => new { b.Author, b.Title })
                .ToList();

            var sortedBooks = new List<string>();

            foreach (var book in books)
            {
                var currentLn = $"{book.Title} ({book.Author.FirstName} {book.Author.LastName})";
                sortedBooks.Add(currentLn);
            }

            return string.Join(Environment.NewLine, sortedBooks);
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            
            var authors = context.Authors
                .Where(a => a.FirstName.ToLower().EndsWith(input.ToLower()))
                .OrderBy(a => a.FirstName)
                .ThenBy(a=>a.LastName)
                .ToList();

            return string.Join(Environment.NewLine, authors.Select(a => $"{a.FirstName} {a.LastName}"));
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authorsBookCopies = context.Books
                .Include(b => b.Author)
                .Select(b => new
                {
                    AuthorName = b.Author.FirstName + ' ' + b.Author.LastName,
                    b.Copies
                })
                .GroupBy(b => b.AuthorName)
                .OrderByDescending(a => a.Sum(b => b.Copies))
                .ToList();

            return string.Join(Environment.NewLine, authorsBookCopies
                .Select(a => $"{a.Key} - {a.Sum(b=>b.Copies)}"));
        }

        public static int CountBooks(BookShopContext context, int lenght)
        {
            var books = context.Books
                .Where(b => b.Title.Length > lenght)
                .ToList();
             
            return books.Count;
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    c.Name,
                    Profit = c.CategoryBooks.Sum(b => b.Book.Copies * b.Book.Price)
                })
                .OrderByDescending(b => b.Profit)
                .ThenBy(c => c.Name)
                .ToList();

            var categoriesList = new List<string>();

            foreach (var category in categories)
            {
                var currentLine = $"{category.Name} ${category.Profit:f2}";
                categoriesList.Add(currentLine);
            }

            return string.Join(Environment.NewLine, categoriesList);
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {

                 
                    CategoryName = c.Name,
                    categoryBookCount = c.CategoryBooks.Count(),
                    CategoryBooks = c.CategoryBooks
                       .OrderByDescending(x => x.Book.ReleaseDate.Value)
                       .Take(3)
                       .Select(y => new
                       {
                           y.Book.Title,
                           y.Book.ReleaseDate.Value.Year
                       }
                           )
                       .OrderByDescending(x => x.Year)
                })
                .OrderBy(x => x.CategoryName)
                .ToList();

            return string.Join(Environment.NewLine, categories.Select(x => $"--{x.CategoryName}"
            + Environment.NewLine 
            + string.Join(Environment.NewLine, x.CategoryBooks.Select(y => $"{y.Title} ({y.Year})"))));
        }

        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToList();

            foreach (var b in books)
            {
                b.Price += 5;
            }

            context.SaveChanges();
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Copies < 4200)
                .ToList();

            foreach (var book in books)
            {
                context.Books.Remove(book);
            }

            context.SaveChanges();

            return books.Count;
        }
    }
}
