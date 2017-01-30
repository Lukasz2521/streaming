namespace streaming_inż.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addkeytousertable3 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "UserId" });
            AlterColumn("dbo.AspNetUsers", "UserId", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "UserId", unique: true);
        }
    }
}
