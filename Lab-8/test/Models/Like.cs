namespace test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Like
    {
        public int likeId { get; set; }

        public int userId { get; set; }

        public int messageId { get; set; }

        public virtual Message Message { get; set; }

        public virtual User User { get; set; }
    }
}
