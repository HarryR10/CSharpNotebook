namespace Social
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.Json;
    using Social.Exceptions;
    using Social.Models;

    public class SocialDataSource
    {
        private List<User> _users = new List<User>();

        private List<Friend> _friends = new List<Friend>();

        private List<Message> _messages = new List<Message>();

        public SocialDataSource(string pathUsers, string pathFriends, string pathMessages)
        {
            GetUsers(pathUsers);
            GetFriends(pathFriends);
            GetMessages(pathMessages);
        }

        public UserContext GetUserContext(string userName)
        {
            var userContext = new UserContext();
            
            ContextUser(userContext, userName);
            
            ContextFriends(userContext);

            ContextFriendsOnline(userContext);

            ContextSubscribers(userContext);

            ContextFriendshipOffers(userContext);

            ContextMessages(userContext);
            
            return userContext;
        }

        private void ContextUser(UserContext userContext, string userName)
        {
            IEnumerable<User> filteredUser =
                from user in _users
                where user.Name == userName
                select user;

            if (filteredUser.Count() == 0)
            {
                throw new UserNameNotFoundException(userName);
            }
            //имя может быть не уникально

            userContext.User = filteredUser.First();
        }

        private void ContextFriends(UserContext userContext)
        {
            var filteredFriends =
                from friend in _friends
                where (userContext.User.UserId == friend.ToUserId |
                       userContext.User.UserId == friend.FromUserId) &
                       friend.Status == 2
                select friend;

            var crossFriends =
                from friend in _friends
                where userContext.User.UserId == friend.FromUserId &
                      (friend.Status == 0 | friend.Status == 1) &
                      friend.FromUserId != friend.ToUserId

                join cross in _friends
                on friend.ToUserId equals cross.FromUserId
                where userContext.User.UserId == cross.ToUserId &
                      (cross.Status == 0 | cross.Status == 1) &
                      friend.FromUserId != friend.ToUserId

                select friend;

            var concat = filteredFriends.Concat(crossFriends);

            var friendList = concat.Select(x =>
                (x.FromUserId == userContext.User.UserId) ? x.ToUserId : x.FromUserId);

            var friends =
                from id in friendList
                join user in _users on id equals user.UserId
                select new UserInformation
                {
                    Name = user.Name,
                    Online = user.Online,
                    UserId = user.UserId
                };

            userContext.Friends = friends.Distinct().ToList();

            // crossFriends понадобиться для определения подписчиков
            //return crossFriends; //уже нет
        }

        private void ContextFriendsOnline(UserContext userContext)
        {
            var friendsOnline =
                from friend in userContext.Friends
                where friend.Online
                select new UserInformation
                {
                    Name = friend.Name,
                    Online = friend.Online,
                    UserId = friend.UserId
                };

            userContext.OnlineFriends = friendsOnline.ToList();
        }

        private void ContextSubscribers(UserContext userContext)
        {
            var sbscrberIDs =
                from sbscrber in _friends
                where userContext.User.UserId == sbscrber.ToUserId &
                      (sbscrber.Status == 0 | sbscrber.Status == 1) &
                      !userContext.Friends.Select(x => x.UserId).Contains(sbscrber.FromUserId) &
                      sbscrber.FromUserId != userContext.User.UserId

                select sbscrber.FromUserId;
            { 
            //notRejected = notRejected.Distinct();

            //нам нужна выборка именно "перекрестных" друзей 
            //var crossFriendList = crossFriends.Select(x =>
            //    (x.FromUserId == userContext.User.UserId) ? x.ToUserId : x.FromUserId);

            //crossFriendList = crossFriendList.Distinct();

            //var sbscrberIDs = crossFriendList.Concat(notRejected).Distinct()...
            //как вариант поиска повторяющихся конкретное число раз
            //var sbscrberIDs = crossFriendList.Concat(notRejected).GroupBy(x => x).
            //                  Where(x => x.Count() == 1).Select(x => x.Key);
            }

            var subscribers =
                from id in sbscrberIDs
                join user in _users on id equals user.UserId
                select new UserInformation
                {
                    Name = user.Name,
                    Online = user.Online,
                    UserId = user.UserId
                };

            userContext.Subscribers = subscribers.ToList();
        }

        private void ContextFriendshipOffers(UserContext userContext)
        {
            var offers =
                from offer in _friends
                where userContext.User.UserId == offer.ToUserId &
                      offer.Status == 0 &
                      userContext.User.UserId != offer.FromUserId
                join man in _users on offer.FromUserId equals man.UserId
                where offer.SendDate >= userContext.User.LastVisit
                select new UserInformation
                {
                    Name = man.Name,
                    Online = man.Online,
                    UserId = man.UserId
                };

            userContext.FriendshipOffers = offers.ToList();
        }

        private void ContextMessages(UserContext userContext)
        {
            //ищем всех друзей, присоединяем
            //сопоставляем с сообщениями по дате
            var messages =
                from friend in userContext.Friends
                join msg in _messages on friend.UserId equals msg.AuthorId
                where msg.SendDate >= userContext.User.LastVisit
                select new News
                {
                    AuthorId = msg.AuthorId,
                    AuthorName = friend.Name,
                    Likes = msg.Likes,
                    Text = msg.Text
                };

            userContext.News = messages.ToList();
        }

        // м.б. и так 
        private void GetUsers(string path) => FillList<User>(path, _users);
                                                // и так 
        private void GetFriends(string path) => FillList(path, _friends);

        private void GetMessages(string path) => FillList(path, _messages);

        public static void FillList<T>(string path, IList<T> list)
        {
            if (File.Exists(path))
            {
                //string fromFile = file.ReadToEnd();
                string line;
                string fromFile = "";

                using (StreamReader file = File.OpenText(path))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        try
                        {
                            fromFile += line;
                        }
                        catch (OutOfMemoryException)
                        {
                            //todo: будет ли этот вариант более безопасным, или лучше считать весь текст сразу
                            //или оставить как есть, но не использовать try?
                        }
                    }
                }

                using (JsonDocument document = JsonDocument.Parse(fromFile))
                {
                    JsonElement root = document.RootElement;
                    foreach (JsonElement el in root.EnumerateArray())
                    {
                        list.Add(JsonSerializer.Deserialize<T>(el.GetRawText()));
                    }

                }

            }
            else
            {
                throw new JsonFileNotFoundException(path);
            }
        }
    }
}
