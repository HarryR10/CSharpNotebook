namespace Social
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;
    using Social.Exceptions;
    using Social.Models;

    public class SocialDataSource
    {
        private List<User> _users;

        private List<Friend> _friends;

        private List<Message> _messages;

        public SocialDataSource(string pathUsers, string pathFriends, string pathMessages)
        {
            GetUsers(pathUsers);
            GetFriends(pathFriends);
            GetMessages(pathMessages);
        }

        public UserContext GetUserContext(string userName)
        {
            var userContext = new UserContext();

            //userContext.User = ...
            //userContext.Friends = GetUserFriends(userContext.User);
            //todo: заполнить информацию

            return userContext;
        }

        private void GetUsers(string path)
        {
            if (File.Exists(path))
            {
                using (StreamReader file = File.OpenText(path))
                {
                    string fromFile = File.ReadAllText(path);
                    JsonConvert.DeserializeObject<User>(fromFile);
                }
            }
            else
            {
                throw new SettingsNotFoundException();
            }
        }

        private void GetFriends(string path)
        {
            //todo: Сделать метод
        }

        private void GetMessages(string path)
        {
            //todo: Сделать метод
        }
    }
}
