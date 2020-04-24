using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialDb;

namespace UnitTests
{
    [TestClass]
    public class FriendsTest
    {
        [TestMethod]
        public void GetFriends_Check_Friend()
        {
            using (DataModel db = new DataModel("name=DataModelForTesting"))
            {
                var extractor = new EntityDataExtractors(db);
                var usr = extractor.GetUser("Lars");
                var mt = extractor.GetFriends(usr).Select(x => x.Name);

                Assert.IsTrue(mt.Contains("James"));
                Assert.IsTrue(mt.Contains("Kirk"));
                Assert.IsTrue(mt.Contains("Robert"));
            }
        }

        [TestMethod]
        public void GetSubscribers_Check_Subscriber()
        {
            using (DataModel db = new DataModel("name=DataModelForTesting"))
            {
                var extractor = new EntityDataExtractors(db);
                var usr = extractor.GetUser("Lars");
                var md = extractor.GetSubscribers(usr, extractor.GetFriends(usr))
                    .Select(x => x.Name);

                Assert.IsTrue(md.Contains("Dave"));
                Assert.IsTrue(!md.Contains("James"));
                Assert.IsTrue(!md.Contains("Kirk"));
                Assert.IsTrue(!md.Contains("Robert"));
            }
        }
    }
}
