namespace streaming_inż.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlikedSongstable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LikedSongs",
                c => new
                    {
                        LikedSongsId = c.Int(nullable: false, identity: true),
                        song_SongID = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.LikedSongsId)
                .ForeignKey("dbo.Songs", t => t.song_SongID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.song_SongID)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.Songs", "isAccepted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LikedSongs", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.LikedSongs", "song_SongID", "dbo.Songs");
            DropIndex("dbo.LikedSongs", new[] { "User_Id" });
            DropIndex("dbo.LikedSongs", new[] { "song_SongID" });
            DropColumn("dbo.Songs", "isAccepted");
            DropTable("dbo.LikedSongs");
        }
    }
}
