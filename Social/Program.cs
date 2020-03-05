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
            string PathUsers = PathDirectory + @"Data/users.json";
            string PathFriends = PathDirectory + @"Data/friends.json";
            string PathMessages = PathDirectory + @"Data/messages.json";

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

                // todo:
                ConsoleColor currentBackground = Console.BackgroundColor;

                Inform(userContext.SayHello());
                Inform(userContext.TotalFriends());

                Cycle();
            }
        }
    }
}
