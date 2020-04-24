namespace UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Friend
    {
        public int FriendId { get; set; }

        public int UserFrom { get; set; }

        public int UserTo { get; set; }

        public short? FriendStatus { get; set; }

        public DateTime? SendDate { get; set; }

        public virtual User UserF { get; set; }

        public virtual User UserT { get; set; }
    }
}
