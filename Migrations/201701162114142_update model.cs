namespace streaming_inż.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemodel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Songs", name: "Users_Id", newName: "User_Id");
            RenameIndex(table: "dbo.Songs", name: "IX_Users_Id", newName: "IX_User_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Songs", name: "IX_User_Id", newName: "IX_Users_Id");
            RenameColumn(table: "dbo.Songs", name: "User_Id", newName: "Users_Id");
        }
    }
}
