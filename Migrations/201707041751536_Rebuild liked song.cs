namespace streaming_inż.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rebuildlikedsong : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LikedSongs", "Song_SongID", "dbo.Songs");
            DropIndex("dbo.LikedSongs", new[] { "Song_SongID" });
            DropColumn("dbo.LikedSongs", "SongId");
            RenameColumn(table: "dbo.LikedSongs", name: "Song_SongID", newName: "SongId");
            AlterColumn("dbo.LikedSongs", "SongId", c => c.Int(nullable: false));
            AlterColumn("dbo.LikedSongs", "SongId", c => c.Int(nullable: false));
            CreateIndex("dbo.LikedSongs", "SongId");
            AddForeignKey("dbo.LikedSongs", "SongId", "dbo.Songs", "SongID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LikedSongs", "SongId", "dbo.Songs");
            DropIndex("dbo.LikedSongs", new[] { "SongId" });
            AlterColumn("dbo.LikedSongs", "SongId", c => c.Int());
            AlterColumn("dbo.LikedSongs", "SongId", c => c.String());
            RenameColumn(table: "dbo.LikedSongs", name: "SongId", newName: "Song_SongID");
            AddColumn("dbo.LikedSongs", "SongId", c => c.String());
            CreateIndex("dbo.LikedSongs", "Song_SongID");
            AddForeignKey("dbo.LikedSongs", "Song_SongID", "dbo.Songs", "SongID");
        }
    }
}
