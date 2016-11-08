namespace streaming_inż.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removetable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "UserInfo_Id", "dbo.UserInfoes");
            DropIndex("dbo.AspNetUsers", new[] { "UserInfo_Id" });
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            AddColumn("dbo.AspNetUsers", "Surname", c => c.String());
            AddColumn("dbo.AspNetUsers", "Country", c => c.String());
            AddColumn("dbo.AspNetUsers", "City", c => c.String());
            AddColumn("dbo.AspNetUsers", "DayOfBirth", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "MonthOfBirth", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "YearOfBirth", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "UserInfo_Id");
            DropTable("dbo.UserInfoes");
        }
        
        public override void Down()
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
            DropColumn("dbo.AspNetUsers", "YearOfBirth");
            DropColumn("dbo.AspNetUsers", "MonthOfBirth");
            DropColumn("dbo.AspNetUsers", "DayOfBirth");
            DropColumn("dbo.AspNetUsers", "City");
            DropColumn("dbo.AspNetUsers", "Country");
            DropColumn("dbo.AspNetUsers", "Surname");
            DropColumn("dbo.AspNetUsers", "Name");
            CreateIndex("dbo.AspNetUsers", "UserInfo_Id");
            AddForeignKey("dbo.AspNetUsers", "UserInfo_Id", "dbo.UserInfoes", "Id");
        }
    }
}
