namespace streaming_inż.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Extendregisterdata : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DayOfBirth", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "MonthOfBirth", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "YearOfBirth", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "YearOfBirth");
            DropColumn("dbo.AspNetUsers", "MonthOfBirth");
            DropColumn("dbo.AspNetUsers", "DayOfBirth");
        }
    }
}
