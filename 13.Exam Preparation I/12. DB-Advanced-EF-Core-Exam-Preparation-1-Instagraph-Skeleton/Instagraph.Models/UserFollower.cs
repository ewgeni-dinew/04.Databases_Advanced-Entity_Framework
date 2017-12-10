namespace Instagraph.Models
{
    public class UserFollower
    {
        public User User { get; set; }
        public int UserId { get; set; }

        public User Follower { get; set; }
        public int FollowerId { get; set; }
    }
}