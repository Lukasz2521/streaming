namespace streaming_inż.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addkeytousertable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", new[] { "UserId" });
            DropColumn("dbo.AspNetUsers", "UserId");
        }
    }
}
