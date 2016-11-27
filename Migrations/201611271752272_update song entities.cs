namespace streaming_inż.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesongentities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Songs", "AvatarPath", c => c.String());
            AlterColumn("dbo.Songs", "Title", c => c.String(nullable: false));
            DropColumn("dbo.Songs", "Avatar");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Songs", "Avatar", c => c.Int(nullable: false));
            AlterColumn("dbo.Songs", "Title", c => c.String());
            DropColumn("dbo.Songs", "AvatarPath");
        }
    }
}
