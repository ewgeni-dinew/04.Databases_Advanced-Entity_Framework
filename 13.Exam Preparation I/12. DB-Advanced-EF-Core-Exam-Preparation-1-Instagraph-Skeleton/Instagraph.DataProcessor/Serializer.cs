using System;

using Instagraph.Data;
using System.Linq;
using System.Xml.Linq;
using Instagraph.Models;
using System.Collections.Generic;
using Instagraph.DataProcessor.DtoModels;
using Newtonsoft.Json;

namespace Instagraph.DataProcessor
{
    public class Serializer
    {
        public static string ExportUncommentedPosts(InstagraphContext context)
        {
            var posts = context.Posts
                .Where(p => p.Comments.Count == 0)
                .OrderBy(p=>p.Id)
                .ToList();

            var postList = new List<UncommentedPostsDto>();

            foreach (var post in posts)
            {
                var picture = context.Pictures
                    .Where(p => p.Id == post.PictureId)
                    .Select(p=>p.Path)
                    .FirstOrDefault();

                var user = context.Users
                    .Where(u => u.Id == post.UserId)
                    .Select(u => u.Username)
                    .FirstOrDefault();

                var currentPostDto = new UncommentedPostsDto()
                {
                    Id = post.Id,
                    Picture = picture,
                    User = user
                };

                postList.Add(currentPostDto);
            }

            var jsonString = JsonConvert.SerializeObject(postList);
            return jsonString;
        }

        public static string ExportPopularUsers(InstagraphContext context)
        {
            
        }

        public static string ExportCommentsOnPosts(InstagraphContext context)
        {
            var users = context.Users
                .Select(u => new
                {
                    Username = u.Username,
                    Posts = u.Posts.Select(p => p.Comments.Count).ToList(),
                    PostId = u.Posts.Select(p => p.Id).FirstOrDefault()
                })
                .ToList();

            var outputUsers = new List<UsersTopCommentsDto>();

            var xDoc = new XDocument(new XElement("users"));
       
            foreach (var user in users)
            {
                var currentUsername = user.Username;
                var currentPostId = user.PostId;
                var mostComments = 0;

                if (user.Posts.Any())
                {
                    mostComments = user.Posts
                        .OrderByDescending(p => p)
                        .First();
                }

                var currentUser = new UsersTopCommentsDto()
                {
                    Username = currentUsername,
                    MostComments = mostComments
                };

                outputUsers.Add(currentUser);
            }

            foreach (var user in outputUsers
                .OrderByDescending(u=>u.MostComments)
                .ThenBy(u=>u.Username))
            {
                xDoc.Root.Add(new XElement("user",
                        new XElement("Username", user.Username),
                        new XElement("MostComments", user.MostComments)));
            }

            var result = xDoc.ToString();
            return result;
        }
    }
}
