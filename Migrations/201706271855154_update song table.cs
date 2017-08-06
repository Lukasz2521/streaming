namespace streaming_inż.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesongtable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.LikedSongs", new[] { "song_SongID" });
            CreateIndex("dbo.LikedSongs", "Song_SongID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.LikedSongs", new[] { "Song_SongID" });
            CreateIndex("dbo.LikedSongs", "song_SongID");
        }
    }
}
