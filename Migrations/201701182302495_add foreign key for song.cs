namespace streaming_inż.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addforeignkeyforsong : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Songs", name: "User_Id", newName: "userId");
            RenameIndex(table: "dbo.Songs", name: "IX_User_Id", newName: "IX_userId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Songs", name: "IX_userId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Songs", name: "userId", newName: "User_Id");
        }
    }
}
