using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialDb.Exceptions;

namespace SocialDb
{
    public class EntityDataExtractors
    {
        public EntityDataExtractors(DataModel db)
        {
            _db = db;
        }

        private DataModel _db;

        public User GetUser(string userName)
        {
            IEnumerable<User> filteredUser =
                from user in _db.Users
                where user.Name == userName
                select user;

            if (filteredUser.Count() == 0)
            {
                throw new UserNameNotFoundException(userName);
            }

            return filteredUser.First();
        }

        public List<User> GetFriends(User crntUsr)
        {
            var result = new List<User>();

            var filteredFriends =
                from friend in _db.Friends
                where (crntUsr.UserId == friend.UserTo |
                       crntUsr.UserId == friend.UserFrom) &
                       friend.FriendStatus == 2
                select friend;

            var crossFriends =
                from friend in _db.Friends
                where crntUsr.UserId == friend.UserFrom &
                      (friend.FriendStatus == 0 | friend.FriendStatus == 1) &
                      friend.UserFrom != friend.UserTo

                join cross in _db.Friends
                on friend.UserTo equals cross.UserFrom
                where crntUsr.UserId == cross.UserTo &
                      (cross.FriendStatus == 0 | cross.FriendStatus == 1) &
                      friend.UserFrom != friend.UserTo

                select friend;

            var concat = filteredFriends.Concat(crossFriends);

            var friendList = concat.Select(x =>
                (x.UserFrom == crntUsr.UserId) ? x.UserTo : x.UserFrom);

            var friends =
                from id in friendList
                join user in _db.Users on id equals user.UserId
                select user;

            return friends.Distinct().ToList();
        }

        public List<User> GetFriendsOnline(List<User> friends)
        {
            var friendsOnline =
                from friend in friends
                where friend.IsOnline
                select friend;

            return friendsOnline.ToList();
        }

        public List<User> GetSubscribers(User crntUsr, List<User> friends)
        {
            var sbscrberIDs =
                from sbscrber in _db.Friends
                where crntUsr.UserId == sbscrber.UserTo & 
                      (sbscrber.FriendStatus == 0 | sbscrber.FriendStatus == 1) &
                      //!friends.Select(x => x.userId).Contains(sbscrber.userFrom) &
                      sbscrber.UserFrom != crntUsr.UserId

                select sbscrber.UserFrom;



            var subscribers =
                from id in sbscrberIDs
                join user in _db.Users on id equals user.UserId
                select user;

            var result =
                from man in subscribers.ToList()
                where !friends.Select(x => x.UserId).Contains(man.UserId)
                select man;

            return result.ToList();
        }

        public List<User> GetFriendshipOffers(User crntUsr)
        {
            var offers =
                from offer in _db.Friends
                where crntUsr.UserId == offer.UserTo &
                      offer.FriendStatus == 0 &
                      crntUsr.UserId != offer.UserFrom
                join man in _db.Users on offer.UserFrom equals man.UserId
                where offer.SendDate >= crntUsr.LastVisit
                select man;

            return offers.ToList();
        }

        public List<Message> GetMessages(User crntUsr, List<User> friends)
        {
            var messages =
                from friend in friends
                join msg in _db.Messages on friend.UserId equals msg.AuthorId
                where msg.SendDate >= crntUsr.LastVisit
                select msg;

            return messages.ToList();
        }

        public ComplexEntityData GetComplexSqlData(NewNameIsEnterEventArgs e)
        {
            var userName = e.Data;
            var crntUser = GetUser(userName);
            var friends = GetFriends(crntUser);
            var friendsOnline = GetFriendsOnline(friends);
            var subscribers = GetSubscribers(crntUser, friends);
            var offers = GetFriendshipOffers(crntUser);
            var messages = GetMessages(crntUser, friends);

            return new ComplexEntityData
            {
                User = crntUser,
                Friends = friends,
                FriendsOnline = friendsOnline,
                Subscribers = subscribers,
                Offers = offers,
                Messages = messages
            };
        }
    }
}
