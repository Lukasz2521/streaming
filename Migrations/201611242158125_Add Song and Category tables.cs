namespace streaming_inż.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSongandCategorytables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        SongID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Avatar = c.Int(nullable: false),
                        PublicDate = c.Int(nullable: false),
                        Users_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.SongID)
                .ForeignKey("dbo.AspNetUsers", t => t.Users_Id)
                .Index(t => t.Users_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        Songs_SongID = c.Int(),
                    })
                .PrimaryKey(t => t.CategoryID)
                .ForeignKey("dbo.Songs", t => t.Songs_SongID)
                .Index(t => t.Songs_SongID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Songs", "Users_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Categories", "Songs_SongID", "dbo.Songs");
            DropIndex("dbo.Categories", new[] { "Songs_SongID" });
            DropIndex("dbo.Songs", new[] { "Users_Id" });
            DropTable("dbo.Categories");
            DropTable("dbo.Songs");
        }
    }
}
