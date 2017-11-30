namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    using Data;
    using Models;

    public class AddTownCommand
    {
        // AddTown <townName> <countryName>
        public static string Execute(string[] data, Session session)
        {
            if (!session.IsLoggedIn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            string townName = data[0];
            string country = data[1];

            using (PhotoShareContext context = new PhotoShareContext())
            {
                if(context.Towns.Any(t => t.Name == townName))
                {
                    throw new ArgumentException($"Town {townName} was already added!");
                }

                Town town = new Town
                {
                    Name = townName,
                    Country = country
                };

                context.Towns.Add(town);
                context.SaveChanges();

                return $"Town {townName} was added to database!";
            }
        }
    }
}
