namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    using PhotoShare.Data;
    using PhotoShare.Models;

    class LoginCommand
    {
        public static string Execute(string[] data, Session session)
        {
            if (session.IsLoggedIn())
            {
                return $"You should logout first!";
            }

            string username = data[0];
            string password = data[1];

            using (PhotoShareContext context = new PhotoShareContext())
            {
                User user = context.Users
                    .FirstOrDefault(u => u.Username == username && u.Password == password);

                if (user == null)
                {
                    throw new ArgumentException("Invalid username or password!!");
                }

                session.Login(user);

                return $"User {user.Username} successfully logged in!";
            }
        }
    }
}
