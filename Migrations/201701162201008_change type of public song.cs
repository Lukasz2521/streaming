namespace streaming_inż.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changetypeofpublicsong : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Songs", "PublicDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Songs", "PublicDate", c => c.Int(nullable: false));
        }
    }
}
