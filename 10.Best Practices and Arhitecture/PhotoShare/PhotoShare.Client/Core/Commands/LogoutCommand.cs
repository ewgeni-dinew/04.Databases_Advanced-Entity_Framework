namespace PhotoShare.Client.Core.Commands
{
    using System;

    using PhotoShare.Models;

    class LogoutCommand
    {
        public static string Execute(Session session)
        {
            if (!session.IsLoggedIn())
            {
                throw new InvalidOperationException("You should log in first in order to logout.");
            }

            string output = $"User {session.User.Username} successfully logged out!";
            session.Logout();

            return output;
        }
    }
}
