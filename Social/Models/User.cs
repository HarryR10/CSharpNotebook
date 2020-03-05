namespace Social.Models
{
    using System;
    using System.Collections.Generic;

    public struct User
    {
        public DateTime DateOfBirth { get; set; }

        public int Gender { get; set; }

        public DateTime LastVisit { get; set; }

        public string Name { get; set; }

        public bool Online { get; set; }

        public int UserId { get; set; }
    }
}









//var filtered =
//            from match in matchings
//            join tag in tags 
//                on match.PageIndex equals tag.PageIndex
//            where match.Quantity >= 5
//            orderby match.PageIndex
//            select new
//            {
//                match.PageIndex,
//                match.Word,
//                Tag = tag.TagName
//            };

//var groupsCount = (from match in matchings
//                       	where match.Quantity >= 5 && match.Quantity != 11
//                       	group match by match.PageIndex).Count();


//var query = from person in people
//join pet in pets on person equals pet.Owner into gj
//from subpet in gj.DefaultIfEmpty()
//select new { person.FirstName, PetName = subpet?.Name ?? String.Empty };



//var crossFriends =
//    from friend in _friends
//    where userContext.User.UserId == friend.FromUserId &
//          (friend.Status == 0 | friend.Status == 1)
//    join cross in _friends
//    on friend.ToUserId equals cross.FromUserId
//    where userContext.User.UserId == cross.ToUserId &
//          (cross.Status == 0 | cross.Status == 1)
//    select new
//    {
//        friend, //ToUserID
//                    cross
//    };