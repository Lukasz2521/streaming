namespace streaming_inż.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatelikedsongtable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.LikedSongs", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.LikedSongs", name: "IX_User_Id", newName: "IX_UserId");
            AddColumn("dbo.LikedSongs", "SongId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LikedSongs", "SongId");
            RenameIndex(table: "dbo.LikedSongs", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.LikedSongs", name: "UserId", newName: "User_Id");
        }
    }
}
