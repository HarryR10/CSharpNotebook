namespace UnitTests
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class DataModelForTest : DbContext
    {
        public DataModelForTest()
            : base("name=DataModelForTesting")
        {
        }

        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .HasMany(e => e.Likes)
                .WithRequired(e => e.Message)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.FriendsF)
                .WithRequired(e => e.UserF)
                .HasForeignKey(e => e.UserFrom)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.FriendsT)
                .WithRequired(e => e.UserT)
                .HasForeignKey(e => e.UserTo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Likes)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Messages)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.AuthorId)
                .WillCascadeOnDelete(false);
        }
    }
}
