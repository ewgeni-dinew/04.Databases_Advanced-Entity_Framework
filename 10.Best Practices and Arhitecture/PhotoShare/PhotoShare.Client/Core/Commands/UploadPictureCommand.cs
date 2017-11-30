namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    using PhotoShare.Data;
    using PhotoShare.Models;
    using Microsoft.EntityFrameworkCore;

    public class UploadPictureCommand
    {
        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public static string Execute(string[] data, Session session)
        {
            if (!session.IsLoggedIn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            string albumName = data[0];
            string pictureTitle = data[1];
            string pictureFilePath = data[2];

            using (PhotoShareContext context = new PhotoShareContext())
            {
                Album album = context.Albums
                    .Include(a => a.AlbumRoles)
                    .ThenInclude(ar => ar.User)
                    .FirstOrDefault(a => a.Name == albumName);

                if (album == null)
                {
                    throw new ArgumentException($"Album {albumName} not found!");
                }

                bool isUserOwner = album.AlbumRoles
                    .Any(ar => ar.Role == Role.Owner && ar.User.Username == session.User.Username);

                if (!isUserOwner)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                Picture picture = new Picture()
                {
                    Album = album,
                    Title = pictureTitle,
                    Path = pictureFilePath
                };

                context.Pictures.Add(picture);
                context.SaveChanges();                
            }

            return $"Picture {pictureTitle} added to {albumName}!";
        }
    }
}
