namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    using PhotoShare.Data;
    using PhotoShare.Models;

    public class ModifyUserCommand
    {
        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public static string Execute(string[] data, Session session)
        {
            if(!session.IsLoggedIn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }
            
            string username = data[0];
            string property = data[1].ToLower();
            string newValue = data[2];
            
            using(PhotoShareContext context = new PhotoShareContext())
            {
                User user = context.Users.FirstOrDefault(u => u.Username == username);

                if(user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                if(user.Username != session.User.Username)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                switch (property)
                {
                    case "password":
                        bool isContainLower = newValue.Any(c => Char.IsLower(c));
                        bool isContainDigit = newValue.Any(ch => Char.IsDigit(ch));

                        if (!isContainLower || !isContainDigit)
                        {
                            throw new ArgumentException($"Value {newValue} not valid." +
                                Environment.NewLine +
                                "Invalid Password");
                        }

                        user.Password = newValue;
                        context.SaveChanges();

                        break;
                    case "borntown":
                        Town bornTown = context.Towns.FirstOrDefault(t => t.Name == newValue);

                        if(bornTown == null)
                        {
                            throw new ArgumentException($"Value {newValue} not valid." +
                                Environment.NewLine +
                                $"Town {newValue} not found!");
                        }

                        user.BornTown = bornTown;
                        context.SaveChanges();

                        break;
                    case "currenttown":
                        Town currentTown = context.Towns.FirstOrDefault(t => t.Name == newValue);

                        if (currentTown == null)
                        {
                            throw new ArgumentException($"Value {newValue} not valid." +
                                Environment.NewLine +
                                $"Town {newValue} not found!");
                        }

                        user.CurrentTown = currentTown;
                        context.SaveChanges();

                        break;
                    default:
                        throw new 
                            ArgumentException($"Property {property} not supported!");
                }

                return $"User {user.Username} {property} is {newValue}.";
            }            
        }
    }
}
