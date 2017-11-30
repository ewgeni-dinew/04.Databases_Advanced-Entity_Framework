namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    using Models;
    using Data;
    using Utilities;

    public class AddTagCommand
    {
        // AddTag <tag>
        public static string Execute(string[] data, Session session)
        {
            if (!session.IsLoggedIn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            string tag = data[0].ValidateOrTransform();

            using (PhotoShareContext context = new PhotoShareContext())
            {
                if(context.Tags.Any(t => t.Name == tag))
                {
                    throw new ArgumentException($"Tag {tag} exists!");
                }

                Tag newTag = new Tag()
                {
                    Name = tag
                };

                context.Tags.Add(newTag);
                context.SaveChanges();
            }

            return $"Tag {tag} was added successfully to database!";
        }
    }
}
