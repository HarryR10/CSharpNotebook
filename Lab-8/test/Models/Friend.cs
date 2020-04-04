namespace test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Friend
    {
        public int friendId { get; set; }

        public int userFrom { get; set; }

        public int userTo { get; set; }

        public short? friendStatus { get; set; }

        public DateTime? sendDate { get; set; }

        public virtual User UserF { get; set; }

        public virtual User UserT { get; set; }
    }
}
