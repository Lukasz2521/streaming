namespace streaming_inż.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changerelationkeybetweenuserandsong : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Songs", "UserName", c => c.String());
            DropColumn("dbo.Songs", "UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Songs", "UserID", c => c.Int(nullable: false));
            DropColumn("dbo.Songs", "UserName");
        }
    }
}
