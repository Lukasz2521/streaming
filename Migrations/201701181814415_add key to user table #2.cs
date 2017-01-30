namespace streaming_inż.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addkeytousertable2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "UserId" });
            CreateIndex("dbo.AspNetUsers", "UserId", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", new[] { "UserId" });
            CreateIndex("dbo.AspNetUsers", "UserId");
        }
    }
}
