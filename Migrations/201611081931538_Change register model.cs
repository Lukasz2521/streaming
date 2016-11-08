namespace streaming_inż.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changeregistermodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Country = c.String(),
                        City = c.String(),
                        DayOfBirth = c.Int(nullable: false),
                        MonthOfBirth = c.Int(nullable: false),
                        YearOfBirth = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "UserInfo_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "UserInfo_Id");
            AddForeignKey("dbo.AspNetUsers", "UserInfo_Id", "dbo.UserInfoes", "Id");
            DropColumn("dbo.AspNetUsers", "DayOfBirth");
            DropColumn("dbo.AspNetUsers", "MonthOfBirth");
            DropColumn("dbo.AspNetUsers", "YearOfBirth");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "YearOfBirth", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "MonthOfBirth", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "DayOfBirth", c => c.Int(nullable: false));
            DropForeignKey("dbo.AspNetUsers", "UserInfo_Id", "dbo.UserInfoes");
            DropIndex("dbo.AspNetUsers", new[] { "UserInfo_Id" });
            DropColumn("dbo.AspNetUsers", "UserInfo_Id");
            DropTable("dbo.UserInfoes");
        }
    }
}
