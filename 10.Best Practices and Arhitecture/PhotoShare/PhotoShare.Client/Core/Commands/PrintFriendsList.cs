namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    using PhotoShare.Data;
    using PhotoShare.Models;
    using Microsoft.EntityFrameworkCore;

    public class PrintFriendsListCommand 
    {
        // PrintFriendsList <username>
        public static string Execute(string[] data, Session session)
        {
            // TODO prints all friends of user with given username.
            var username = data[0];

            using (PhotoShareContext context = new PhotoShareContext())
            {
                var user = context.Users
                    .Include(u => u.FriendsAdded)
                    .ThenInclude(fa => fa.Friend)
                    .FirstOrDefault(u => u.Username == username);

                if (user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                if (user.FriendsAdded.Count == 0)
                {
                    return $"No friends for this user. :(";
                }

                var friends = user.FriendsAdded
                    .OrderBy(fa => fa.Friend.Username)
                    .Select(fa => "-" + fa.Friend.Username);

                return "Friends: " + 
                    Environment.NewLine + 
                    string.Join(Environment.NewLine, friends);
            }
        }
    }
}
