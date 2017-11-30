namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using PhotoShare.Data;
    using PhotoShare.Models;
    using PhotoShare.Client.Utilities;

    public class CreateAlbumCommand
    {
        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public static string Execute(string[] data, Session session)
        {
            if (!session.IsLoggedIn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            string username = data[0];
            string albumTitle = data[1];
            string bgColor = data[2];
            string[] tags = data.Skip(3).Select(t => t.ValidateOrTransform()).ToArray();

            using(PhotoShareContext context = new PhotoShareContext())
            {
                User user = context.Users
                    .FirstOrDefault(u => u.Username == username);

                if (user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                if (user.Username != session.User.Username)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                if (context.Albums.Any(a => a.Name == albumTitle))
                {
                    throw new ArgumentException($"Album {albumTitle} exists!");
                }

                Color color;
                if(!Enum.TryParse(bgColor, out color))
                {
                    throw new ArgumentException($"Color {bgColor} not found!");
                }

                if (!tags.All(t => context.Tags.Any(ct => ct.Name == t)))
                {
                    throw new ArgumentException($"Invalid tags!");
                }

                Album album = new Album()
                {
                    Name = albumTitle,
                    BackgroundColor = color,
                    AlbumRoles = new List<AlbumRole>()
                    {
                        new AlbumRole()
                        {
                            User = user,
                            Role = Role.Owner
                        }
                    },
                    AlbumTags = tags.Select(t => new AlbumTag()
                    {
                        Tag = context.Tags
                            .FirstOrDefault(ct => ct.Name == t)
                    })
                    .ToArray()
                };

                context.Albums.Add(album);
                context.SaveChanges();
            }

            return $"Album {albumTitle} successfully created!";
        }
    }
}
