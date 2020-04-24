using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialDb
{
    public static class DisplayEntityData
    {
        public static void DisplayOnConsole(ComplexEntityData data)
        {
            var DefaultBckg = Console.BackgroundColor;
            var DefaultFrg = Console.ForegroundColor;

            void reset()
            {
                Console.BackgroundColor = DefaultBckg;
                Console.ForegroundColor = DefaultFrg;
            }

            void colorize()
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            // name
            colorize();
            Console.WriteLine("Hello, {0}, {1} years old!",
                data.User.Name,
                DateTime.Now.Year - data.User.DateOfBirth.Value.Year);

            // friends
            Console.Write("You have {0} friends:\n", data.Friends.Count);
            reset();
            foreach(var frnd in data.Friends)
            {
                Console.WriteLine(frnd.Name);
            }

            // online
            colorize();
            Console.WriteLine($"{data.FriendsOnline.Count} friends is online:");
            reset();
            foreach (var frnd in data.FriendsOnline)
            {
                Console.WriteLine(frnd.Name);
            }

            // subscribers
            colorize();
            Console.WriteLine($"You have {data.Subscribers.Count} subscribers:");
            reset();
            foreach (var sb in data.Subscribers)
            {
                Console.WriteLine(sb.Name);
            }

            // offers
            colorize();
            Console.WriteLine($"You have {data.Offers.Count} new friendship offers from:");
            reset();
            foreach (var of in data.Offers)
            {
                Console.WriteLine(of.Name);
            }

            // messages
            colorize();
            Console.WriteLine("New posts:");
            reset();

            Console.WriteLine("---------------------------------------------------");
            foreach (var msg in data.Messages)
            {
                Console.Write($"{msg.User.Name} said: {msg.MessageText} ({msg.Likes.Count} Likes)" +
                    $"\n---------------------------------------------------");
            }
            Console.WriteLine();
        }
    }
}
