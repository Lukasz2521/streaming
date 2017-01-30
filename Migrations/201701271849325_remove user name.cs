namespace streaming_inż.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeusername : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Songs", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Songs", "UserName", c => c.String());
        }
    }
}
