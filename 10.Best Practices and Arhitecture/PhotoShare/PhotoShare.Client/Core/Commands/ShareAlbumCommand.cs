namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    using PhotoShare.Data;
    using PhotoShare.Models;

    public class ShareAlbumCommand
    {
        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        public static string Execute(string[] data, Session session)
        {
            if (!session.IsLoggedIn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            int albumId = int.Parse(data[0]);
            string username = data[1];
            string permissionInput = data[2];

            using(PhotoShareContext context = new PhotoShareContext())
            {
                Album album = context.Albums
                    .Find(albumId);

                if (album == null)
                {
                    throw new ArgumentException($"Album with Id {albumId} not found!");
                }

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

                Role permission;
                if (!Enum.TryParse(permissionInput, out permission))
                {
                    throw new ArgumentException("Permission must be either “Owner” or “Viewer”!");
                }

                AlbumRole newAlbumRole = new AlbumRole()
                {
                    Album = album,
                    User = user,
                    Role = permission
                };

                if (context.AlbumRoles.Any(ar => ar.Album == newAlbumRole.Album && ar.User == newAlbumRole.User))
                {
                    throw new ArgumentException($"Album {album.Name} already shared to {user.Username} with role {permission}");
                }

                context.AlbumRoles.Add(newAlbumRole);
                context.SaveChanges();

                return $"Username {user.Username} added to album {album.Name} ({permission})";
            }
        }
    }
}
