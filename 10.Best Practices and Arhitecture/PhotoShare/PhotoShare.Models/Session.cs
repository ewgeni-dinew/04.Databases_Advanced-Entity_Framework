namespace PhotoShare.Models
{
    public class Session
    {
        public User User { get; private set; }

        public User Login(User user)
        {
            this.User = user;

            return this.User;
        }

        public void Logout()
        {
            this.User = null;
        }

        public bool IsLoggedIn()
        {
            if (this.User == null)
            {
                return false;
            }

            return true;
        }
    }
}
