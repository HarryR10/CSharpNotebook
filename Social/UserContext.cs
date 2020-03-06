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

        public ColorScheme ColorSettings { get; internal set; }

        //todo: DefaultColor, т.к. на MacOs цвет консоли по умолчанию белый
        public ConsoleColor DefaultColor { get; internal set; }

        // в следующие два метода можно записать правила оформления
        // для вывода в другие источники
        internal void SetColor()
        {
            if (ColorSettings == ColorScheme.Warm)
                Console.BackgroundColor = ConsoleColor.Yellow;
            if (ColorSettings == ColorScheme.Cold)
                Console.BackgroundColor = ConsoleColor.Cyan;
            else
                return;
        }

        internal void ResetColor() => Console.BackgroundColor = DefaultColor;

        private string BuildUsersList(List<UserInformation> data)
        {
            ResetColor();

            var aboutFriends = new StringBuilder();

            int i = 1;

            aboutFriends.Append(
                string.Join(Environment.NewLine,
                data.Select(x => $"{i++} {x.Name}"))
                );

            aboutFriends.Append("\n");

            return aboutFriends.ToString();
        }

        internal string SayHello()
        {
            SetColor();

            var welcome = new StringBuilder();

            welcome.AppendFormat("\nHello, {0}, {1} years old!\n",
                User.Name,
                DateTime.Now.Year - User.DateOfBirth.Year);

            return welcome.ToString();
        }

        internal string TotalFriends()
        {
            SetColor();

            var aboutFriends = new StringBuilder();

            aboutFriends.AppendFormat("You have {0} friends:\n", Friends.Count);

            return aboutFriends.ToString();
        }

        internal string FriendsList() => BuildUsersList(Friends);

        internal string TotalFriendsOnline()
        {
            SetColor();

            return $"{OnlineFriends.Count} friends is online:\n";
        }

        internal string FriendsListOnline() => BuildUsersList(OnlineFriends);

        internal string TotalSubscribers()
        {
            SetColor();

            return $"You have {Subscribers.Count} subscribers:\n";
        }

        internal string SubscribersList() => BuildUsersList(Subscribers);

        internal string NewFriendRequests()
        {
            SetColor();

            return $"You have {FriendshipOffers.Count} new friendship offers from:\n";
        }

        internal string FriendRequestsList() => BuildUsersList(FriendshipOffers);

        internal string NewsChapter()
        {
            SetColor();

            return "New posts:\n";
        }

        internal string GetPosts()
        {
            ResetColor();

            var str = new StringBuilder();

            //todo: по не понятной мне причине здесь происходит баг с цветом,
            //фон становиться цветным в этой строке, после \n
            //одинаково работает и с Console.Writeline и с Write (в методе DefineDelegate() )


            str.Append("---------------------------------------------------\n");

            str.Append(
                string.Join(Environment.NewLine,
                News.Select(x => $"{x.AuthorName} said: {x.Text} ({x.Likes.Count} Likes)" +
                $"\n---------------------------------------------------"))
                );

            return str.ToString() + "\n";
        }
    }
}
