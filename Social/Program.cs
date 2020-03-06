namespace Social
{
    using System;
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            SettingUpdater session = new SettingUpdater();

            string PathDirectory = session.CurrentSettings.PathDirectory;
            string PathUsers = PathDirectory + @"/users.json";
            string PathFriends = PathDirectory + @"/friends.json";
            string PathMessages = PathDirectory + @"/messages.json";

            session.PrintSettingsToConsole();
            session.DefineDelegate();

            Print Inform = session.Inform;

            Cycle();

            void Cycle()
            {
                Console.WriteLine("Write here username and press Enter:");

                var current = Console.ReadLine();

                if (current.Length == 0)
                {
                    Console.Write("exit to console");
                    for (int t = 0; t < 3; t++)
                    {
                        Thread.Sleep(500);
                        Console.Write(".");
                    }
                    return;
                }

                var socialDataSource = new SocialDataSource(PathUsers, PathFriends, PathMessages);

                var userContext = socialDataSource.GetUserContext(current);

                userContext.DefaultColor = Console.BackgroundColor;
                userContext.ColorSettings = session.CurrentSettings.Scheme;

                Inform(userContext.SayHello());
                Inform(userContext.TotalFriends());
                Inform(userContext.FriendsList());
                Inform(userContext.TotalFriendsOnline());
                Inform(userContext.FriendsListOnline());
                Inform(userContext.TotalSubscribers());
                Inform(userContext.SubscribersList());
                Inform(userContext.NewFriendRequests());
                Inform(userContext.FriendRequestsList());
                Inform(userContext.NewsChapter());
                Inform(userContext.GetPosts());

                Cycle();
            }
        }
    }
}
