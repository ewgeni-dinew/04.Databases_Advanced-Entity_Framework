namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    using PhotoShare.Data;
    using PhotoShare.Models;
    using PhotoShare.Client.Utilities;
    using Microsoft.EntityFrameworkCore;

    public class AddTagToCommand 
    {
        // AddTagTo <albumName> <tag>
        public static string Execute(string[] data, Session session)
        {
            if (!session.IsLoggedIn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            string albumName = data[0];
            string tagName = data[1].ValidateOrTransform();

            using (PhotoShareContext context = new PhotoShareContext())
            {
                Album album = context.Albums
                    .Include(a => a.AlbumTags)
                    .Include(a => a.AlbumRoles)
                    .ThenInclude(ar => ar.User)
                    .FirstOrDefault(a => a.Name == albumName);

                if (album == null)
                {
                    throw new ArgumentException($"Album {albumName} do not exist!");
                }

                bool isUserOwner = album.AlbumRoles
                    .Any(ar => ar.Role == Role.Owner && ar.User.Username == session.User.Username);

                if (!isUserOwner)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                Tag tag = context.Tags
                    .FirstOrDefault(t => t.Name == tagName);

                if (tag == null)
                {
                    throw new ArgumentException($"Tag {tagName} do not exist!");
                }

                AlbumTag albumTag = new AlbumTag()
                {
                    Album = album,
                    Tag = tag
                };

                if (album.AlbumTags.Any(at => at.AlbumId == album.Id && at.TagId == tag.Id))
                {
                    throw new InvalidOperationException($"Tag {tag.Name} is already added to album {album.Name}");
                }

                album.AlbumTags.Add(albumTag);
                context.SaveChanges();
            }

            return $"Tag {tagName} added to {albumName}!";
        }
    }
}
