using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

using Newtonsoft.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Instagraph.Data;
using Instagraph.Models;
using Instagraph.DataProcessor.DtoModels;

namespace Instagraph.DataProcessor
{
    public class Deserializer
    {
        private static string errorMsg = "Error: Invalid data.";

        public static string ImportPictures(InstagraphContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var pictures = JsonConvert.DeserializeObject<Picture[]>(jsonString);

            foreach (var p in pictures)
            {
                bool pictureExists = context.Pictures.Any(e => e.Path == p.Path);
                bool pathIsValid = !String.IsNullOrWhiteSpace(p.Path);
                bool sizeIsValid = p.Size > 0;

                if (!pathIsValid||pictureExists||!sizeIsValid)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                context.Pictures.Add(p);
                context.SaveChanges();

                sb.AppendLine($"Successfully imported Picture {p.Path}.");
            }

            var result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            var usersDto = JsonConvert.DeserializeObject<UserDto[]>(jsonString);

            var sb = new StringBuilder();

            foreach (var user in usersDto)
            {
                var userIsValid = !String.IsNullOrWhiteSpace(user.Username);
                var passIsValid = !String.IsNullOrWhiteSpace(user.Password);
                var picIsValid = !String.IsNullOrWhiteSpace(user.ProfilePicture);

                if (!userIsValid||!passIsValid||!picIsValid)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var picIdIsValid = context.Pictures.Any(p => p.Path == user.ProfilePicture);

                if (!picIdIsValid)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var pictureId = context.Pictures
                    .Where(p => p.Path == user.ProfilePicture)
                    .FirstOrDefault().Id;

                var currentUser = new User()
                {
                    Username = user.Username,
                    Password = user.Password,
                    ProfilePictureId = pictureId,
                };

                context.Users.Add(currentUser);
                context.SaveChanges();
                sb.AppendLine($"Successfully imported User {user.Username}.");
            }
            var result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var deserializedFollowers = JsonConvert.DeserializeObject<UserFollowerDto[]>(jsonString);

            var followers = new List<UserFollower>();

            foreach (var dto in deserializedFollowers)
            {
                int? userId = context.Users
                    .Where(u => u.Username == dto.User)
                    .FirstOrDefault()?
                    .Id;

                int? followerId = context.Users
                    .Where(u => u.Username == dto.Follower)
                    .FirstOrDefault()?
                    .Id;

                if (followerId==null||userId==null)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                bool alreadyFollowed = followers.Any(f => f.UserId == userId && f.FollowerId == followerId);

                if (alreadyFollowed)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }
                else
                {
                    var currentUserFoll = new UserFollower()
                    {
                        UserId = userId.Value,
                        FollowerId = followerId.Value
                    };
                    followers.Add(currentUserFoll);

                    sb.AppendLine($"Successfully imported Follower {dto.Follower}" +
                $" to User {dto.User}.");
                }
            }

            context.UsersFollowers.AddRange(followers);
            context.SaveChanges();
            
            var result = sb.ToString().TrimEnd();
            return result;
            
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var xDoc = XDocument.Parse(xmlString);

            var posts = new List<Post>();

            foreach (var element in xDoc.Root.Elements())
            {
                var caption = element.Element("caption")?.Value;
                var user = element.Element("user")?.Value;
                var picture = element.Element("picture")?.Value;

                if (caption == null || user == null || picture == null)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var userAlreadyExist = context.Users
                    .Any(u => u.Username == user);

                var pictureAlreadyExist = context.Pictures
                    .Any(p => p.Path == picture);

                if (!userAlreadyExist || !pictureAlreadyExist)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var currentUserId = context.Users
                    .Where(u => u.Username == user)
                    .FirstOrDefault()
                    .Id;

                var currentPictureId = context.Pictures
                    .Where(p => p.Path == picture)
                    .FirstOrDefault()
                    .Id;

                var currentPost = new Post()
                {
                    Caption = caption,
                    UserId = currentUserId,
                    PictureId = currentPictureId,
                };

                posts.Add(currentPost);
                sb.AppendLine($"Successfully imported Post {caption}.");
            }

            context.Posts.AddRange(posts);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            var xDoc = XDocument.Parse(xmlString);

            var sb = new StringBuilder();

            foreach (var element in xDoc.Root.Elements())
            {
                var content = element.Element("content")?.Value;
                var user = element.Element("user")?.Value;
                var post = element.Element("post")?.Attribute("id")?.Value;

                if (content == null || user == null||post==null)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }


                var postExists = context.Posts
                    .Any(p => p.Id == int.Parse(post));

                var userExists = context.Users
                    .Any(u => u.Username == user);

                if (!postExists||!userExists)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var userId = context.Users
                    .Where(u => u.Username == user)
                    .FirstOrDefault()
                    .Id;

                var postId = context.Posts
                    .Where(p => p.Id == int.Parse(post))
                    .FirstOrDefault()
                    .Id;

                var comment = new Comment()
                {
                    Content = content,
                    UserId = userId,
                    PostId = postId
                };

                context.Comments.Add(comment);
                context.SaveChanges();
                sb.AppendLine($"Successfully imported Comment {content}.");
            }

            var result = sb.ToString().TrimEnd();
            return result;
        }
    }
}
