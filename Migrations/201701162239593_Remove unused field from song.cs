namespace streaming_inż.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removeunusedfieldfromsong : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Songs", "AvatarPath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Songs", "AvatarPath", c => c.String());
        }
    }
}
