namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    using Data;
    using PhotoShare.Models;

    public class DeleteUser
    {
        // DeleteUser <username>
        public static string Execute(string[] data, Session session)
        {
            if (!session.IsLoggedIn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            string username = data[0];

            using (PhotoShareContext context = new PhotoShareContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == username);

                if (user == null)
                {
                    throw new InvalidOperationException($"User with {username} was not found!");
                }
                
                if (user.Username != session.User.Username)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                // TODO: Delete User by username (only mark him as inactive)
                if (user.IsDeleted.Value)
                {
                    throw new InvalidOperationException($"User {user.Username} is already deleted!");
                }

                user.IsDeleted = true;
                context.SaveChanges();

                return $"User {username} was deleted from the database!";
            }
        }
    }
}
