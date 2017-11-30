namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    using PhotoShare.Data;
    using PhotoShare.Models;
    using Microsoft.EntityFrameworkCore;

    public class AddFriendCommand
    {
        // AddFriend <username1> <username2>
        public static string Execute(string[] data, Session session)
        {
            if (!session.IsLoggedIn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            var userUsername = data[0];
            var friendUsername = data[1];

            using(PhotoShareContext context = new PhotoShareContext())
            {
                var user = context.Users
                    .Include(u => u.FriendsAdded)
                    .ThenInclude(f => f.User)
                    .FirstOrDefault(u => u.Username == userUsername);

                if (user == null)
                {
                    throw new ArgumentException($"{userUsername} not found!");
                }

                if (user.Username != session.User.Username)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                var friend = context.Users
                    .Include(u => u.FriendsAdded)
                    .ThenInclude(f => f.User)
                    .FirstOrDefault(u => u.Username == friendUsername);

                if (friend == null)
                {
                    throw new ArgumentException($"{friendUsername} not found!");
                }

                bool isTheUserAFriend = user.FriendsAdded.Any(f => f.Friend == friend);
                bool isTheFriendAUserFriend = friend.FriendsAdded.Any(f => f.Friend == user);

                if (isTheUserAFriend && isTheFriendAUserFriend)
                {
                    throw new InvalidOperationException($"{friend.Username} is already a friend to {user.Username}!");
                }

                if (isTheUserAFriend)
                {
                    throw new InvalidOperationException($"The request is already sent to {friend.Username}!");
                }
                
                if (isTheFriendAUserFriend)
                {
                    throw new InvalidOperationException($"The request is already sent to {user.Username}!" + 
                        Environment.NewLine + 
                        $"If you want to accept {friend.Username} as a friend, please insert the command \"AcceptFriend {user.Username} {friend.Username}\"!");
                }

                var newFriendship = new Friendship()
                {
                    User = user,
                    Friend = friend
                };
                
                user.FriendsAdded.Add(newFriendship);
                context.SaveChanges();

                return $"Friend {friend.Username} added to {user.Username}!";
            }
        }
    }
}
