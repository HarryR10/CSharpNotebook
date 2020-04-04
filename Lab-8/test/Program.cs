using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using test.Exceptions;
using test.TsqlCommands;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            // реализована команда добавления одного пользователя в формате:
            // add one user [имя], [пол(male|female)], [дата рождения(дд.мм.гггг)], [дата последнего визита(дд.мм.гггг)], [сейчас онлайн(y|n)]
            /*SqlQueryLogic.MakeAndExecuteSQLQuery();*/

            // генераторы
            /*SqlEntityGenerator.GenerateUserInfo(2);
            SqlEntityGenerator.GenerateUserLikes(2);
            SqlEntityGenerator.GenerateMessageInfo(4);
            SqlEntityGenerator.GenerateFriendsInfo(3);*/

            using (DataModel db = new DataModel())
            {
                foreach (var u in db.Users)
                {
                    Console.WriteLine(u.userId);
                    Console.WriteLine(u.name);
                    Console.WriteLine(u.gender);
                    Console.WriteLine(u.dateOfBirth);
                    Console.WriteLine(u.isOnline);
                    Console.WriteLine(u.lastVisit);
                    Console.WriteLine("------------------------------------");
                }

                foreach (var m in db.Messages)
                {
                    Console.WriteLine(m.authorId);
                    foreach (var l in m.Likes)
                        Console.WriteLine("like from: " + l.User.name + " (" + l.userId + ")");
                }
                Console.WriteLine();

                foreach (var l in db.Likes)
                {
                    Console.WriteLine(l.userId);
                    Console.WriteLine(l.messageId);
                }

                Console.ReadLine();
            }
        }
    }
}
