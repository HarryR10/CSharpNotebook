using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests;
using SocialDb.Exceptions;

namespace SocialDb
{
    public static class SqlEntityGenerator
    {
        public static void GenerateUserInfo(int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                var usr = new User();

                usr.Name = RandomMechanics.RandomString();
                usr.Gender = RandomMechanics.RandomBool();
                usr.DateOfBirth = RandomMechanics.RandomDay();
                usr.IsOnline = RandomMechanics.RandomBool();
                usr.LastVisit = RandomMechanics.RandomDay();

                using (DataModel db = new DataModel("name=DataModelForTesting"))
                {
                    db.Users.Add(usr);
                    db.SaveChanges();
                }

            }
        }

        public static void GenerateMessageInfo(int quantity)
        {
            using (DataModel db = new DataModel("name=DataModelForTesting"))
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
                    msg.SendDate = RandomMechanics.RandomDay();
                    msg.MessageText = RandomMechanics.RandomString();

                    db.Messages.Add(msg);
                    db.SaveChanges();
                }
            }
        }

        public static void GenerateUserLikes(int quantity)
        {
            using (DataModel db = new DataModel("name=DataModelForTesting"))
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
            using (DataModel db = new DataModel("name=DataModelForTesting"))
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
                    friend.FriendStatus = (short)RandomMechanics.RandomNumber(4);
                    friend.SendDate = RandomMechanics.RandomDay();

                    db.Friends.Add(friend);
                    db.SaveChanges();
                }
            }
        }
    }
}
