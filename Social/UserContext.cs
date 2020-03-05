namespace Social
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Social.Models;

    public class UserContext
    {
        public User User { get; set; }

        public List<UserInformation> Friends { get; set; }

        public List<UserInformation> OnlineFriends { get; set; }

        public List<UserInformation> FriendshipOffers { get; set; }

        public List<UserInformation> Subscribers { get; set; }

        public List<News> News { get; set; }

        public string SayHello()
        {
            var welcome = new StringBuilder();

            welcome.AppendFormat("Hello, {0}, {1} years old!",
                User.Name,
                DateTime.Now.Year - User.DateOfBirth.Year);

            return welcome.ToString();
        }

        public string TotalFriends()
        {
            var aboutFriends = new StringBuilder();

            aboutFriends.AppendFormat("You have {0} friends:\n", Friends.Count);

            int i = 1;

            aboutFriends.Append(
                string.Join(Environment.NewLine,
                Friends.Select(x => $"{i++} {x.Name}"))
                );

            return aboutFriends.ToString();
        }
    }
}
