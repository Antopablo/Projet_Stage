namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConceptionTreeView : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "File_Id", c => c.Int());
            CreateIndex("dbo.Files", "File_Id");
            AddForeignKey("dbo.Files", "File_Id", "dbo.Files", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "File_Id", "dbo.Files");
            DropIndex("dbo.Files", new[] { "File_Id" });
            DropColumn("dbo.Files", "File_Id");
        }
    }
}
