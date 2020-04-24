namespace UnitTests.DataAccess.EntityFramework.Migrations
{
    using SocialDb;
    using System;
    using System.Configuration;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTestDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        FriendId = c.Int(nullable: false, identity: true),
                        UserFrom = c.Int(nullable: false),
                        UserTo = c.Int(nullable: false),
                        FriendStatus = c.Short(),
                        SendDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.FriendId)
                .ForeignKey("dbo.Users", t => t.UserFrom)
                .ForeignKey("dbo.Users", t => t.UserTo)
                .Index(t => t.UserFrom)
                .Index(t => t.UserTo);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Gender = c.Boolean(),
                        DateOfBirth = c.DateTime(),
                        LastVisit = c.DateTime(),
                        IsOnline = c.Boolean(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        LikeId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        MessageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LikeId)
                .ForeignKey("dbo.Messages", t => t.MessageId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.MessageId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        AuthorId = c.Int(nullable: false),
                        SendDate = c.DateTime(nullable: false),
                        MessageText = c.String(),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.Users", t => t.AuthorId)
                .Index(t => t.AuthorId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Messages", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.Likes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Likes", "MessageId", "dbo.Messages");
            DropForeignKey("dbo.Friends", "UserTo", "dbo.Users");
            DropForeignKey("dbo.Friends", "UserFrom", "dbo.Users");
            DropIndex("dbo.Messages", new[] { "AuthorId" });
            DropIndex("dbo.Likes", new[] { "MessageId" });
            DropIndex("dbo.Likes", new[] { "UserId" });
            DropIndex("dbo.Friends", new[] { "UserTo" });
            DropIndex("dbo.Friends", new[] { "UserFrom" });
            DropTable("dbo.Messages");
            DropTable("dbo.Likes");
            DropTable("dbo.Users");
            DropTable("dbo.Friends");
        }
    }
}
