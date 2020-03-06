using System;
namespace JsonGenerator
{
    using System.Collections.Generic;
    using Social;
    using Social.Models;

    public class DataGenerator
    {
        private List<User> _users = new List<User>();

        private List<Friend> _friends = new List<Friend>();

        private List<Message> _messages = new List<Message>();

        public DataGenerator()
        {
            SettingUpdater session = new SettingUpdater();

            string PathDirectory = session.CurrentSettings.PathDirectory;
            string PathUsers = PathDirectory + @"/users.json";
            string PathFriends = PathDirectory + @"/friends.json";
            string PathMessages = PathDirectory + @"/messages.json";

            SocialDataSource.FillList(PathUsers, _users);
            SocialDataSource.FillList(PathFriends, _friends);
            SocialDataSource.FillList(PathMessages, _messages);

            AddUsers(6, 100);
            AddFriends(6, 1000, _users.Count);
            AddMessages(5, 200, _users.Count);

            JsonFileWriter.Writer(PathUsers, _users);
            JsonFileWriter.Writer(PathFriends, _friends);
            JsonFileWriter.Writer(PathMessages, _messages);
        }

        private void AddUsers(int counterIn, int counterOut)
        {
            for (int UserId = counterIn; UserId <= counterOut; UserId++)
            {
                var current = new User();

                current.DateOfBirth = RandomMechanics.RandomDay();
                current.Gender = RandomMechanics.RandomNumber(2);
                current.LastVisit = RandomMechanics.RandomDay();
                current.Name = RandomMechanics.RandomString();
                current.Online = RandomMechanics.RandomBool();
                current.UserId = UserId;

                _users.Add(current);
            }
        }

        private void AddFriends(int counterIn, int counterOut, int counterUser)
        {
            for (int id = counterIn; id <= counterOut; id++)
            {
                var current = new Friend();

                current.FromUserId = RandomMechanics.RandomNumber(1, counterUser);
                current.SendDate = RandomMechanics.RandomDay();
                current.Status = RandomMechanics.RandomNumber(4);
                current.ToUserId = RandomMechanics.RandomNumber(1, counterUser);

                _friends.Add(current);
            }
        }

        private void AddMessages(int counterIn, int counterOut, int counterUser)
        {
            for (int id = counterIn; id <= counterOut; id++)
            {
                var current = new Message();

                current.AuthorId = RandomMechanics.RandomNumber(1, counterUser);
                current.MessageId = RandomMechanics.RandomNumber(id);
                current.SendDate = RandomMechanics.RandomDay();
                current.Text = RandomMechanics.RandomString();

                current.Likes = new List<int>();

                for (int l = 0; l <= RandomMechanics.RandomNumber(10); l++)
                {
                    current.Likes.Add(RandomMechanics.RandomNumber(counterUser));
                }

                _messages.Add(current);
            }
        }
    }
}
