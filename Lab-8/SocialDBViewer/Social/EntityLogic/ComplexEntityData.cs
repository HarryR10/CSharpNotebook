using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialDb
{
    public struct ComplexEntityData
    {
        public User User { get; set; }

        public List<User> Friends { get; set; }

        public List<User> FriendsOnline { get; set; }

        public List<User> Subscribers { get; set; }

        public List<User> Offers { get; set; }

        public List<Message> Messages { get; set; }
    }
}
