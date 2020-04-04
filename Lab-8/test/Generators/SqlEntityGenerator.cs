using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.Exceptions;

namespace test
{
    public static class SqlEntityGenerator
    {
        public static void GenerateUserInfo(int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                var usr = new User();

                usr.name = RandomMechanics.RandomString();
                usr.gender = RandomMechanics.RandomBool();
                usr.dateOfBirth = RandomMechanics.RandomDay();
                usr.isOnline = RandomMechanics.RandomBool();
                usr.lastVisit = RandomMechanics.RandomDay();

                using (DataModel db = new DataModel())
                {
                    db.Users.Add(usr);
                    db.SaveChanges();
                }

            }
        }

        public static void GenerateMessageInfo(int quantity)
        {
            using (DataModel db = new DataModel())
            {
                var senders = db.Users.AsEnumerable().ToArray();
                int border = db.Users.Count();

                if(border == 0)
                {
                    throw new NotEnoughSqlDataException();
                }

                for (int i = 0; i < quantity; i++)
                {
                    int randUsr = RandomMechanics.RandomNumber(border - 1);
                    var sender = senders[randUsr];

                    var msg = new Message();

                    msg.User = sender;
                    msg.sendDate = RandomMechanics.RandomDay();
                    msg.messageText = RandomMechanics.RandomString();

                    db.Messages.Add(msg);
                    db.SaveChanges();
                }
            }
        }

        public static void GenerateUserLikes(int quantity)
        {
            using (DataModel db = new DataModel())
            {
                var users = db.Users.AsEnumerable().ToArray();
                var messages = db.Messages.AsEnumerable().ToArray();

                int usrBorder = db.Users.Count();
                int msgBorder = db.Messages.Count();

                if (usrBorder == 0 || msgBorder == 0)
                {
                    throw new NotEnoughSqlDataException();
                }

                for (int i = 0; i < quantity; i++)
                {
                    int randUsr = RandomMechanics.RandomNumber(usrBorder - 1);
                    var sender = users[randUsr];

                    int randMsg = RandomMechanics.RandomNumber(msgBorder - 1);
                    var message = messages[randMsg];

                    var like = new Like();

                    like.Message = message;
                    like.User = sender;

                    db.Likes.Add(like);
                    db.SaveChanges();
                }
            }
        }

        public static void GenerateFriendsInfo(int quantity)
        {
            using (DataModel db = new DataModel())
            {
                var users = db.Users.AsEnumerable().ToArray();
                int usrBorder = db.Users.Count();

                if (usrBorder < 2)
                {
                    throw new NotEnoughSqlDataException();
                }

                for (int i = 0; i < quantity; i++)
                {
                    int usrIndxFrom = RandomMechanics.RandomNumber(usrBorder - 1);
                    int usrIndxTo = usrIndxFrom;

                    do
                    {
                        usrIndxTo = RandomMechanics.RandomNumber(usrBorder - 1);
                    }
                    while (usrIndxFrom == usrIndxTo);

                    var randUsrFrom = users[usrIndxFrom];
                    var randUsrTo = users[usrIndxTo];


                    var friend = new Friend();

                    friend.UserF = randUsrFrom;
                    friend.UserT = randUsrTo;
                    friend.friendStatus = (short)RandomMechanics.RandomNumber(4);
                    friend.sendDate = RandomMechanics.RandomDay();

                    db.Friends.Add(friend);
                    db.SaveChanges();
                }
            }
        }
    }
}
