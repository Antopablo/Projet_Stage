namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test_treeview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Normes", "Norme_Id", c => c.Int());
            CreateIndex("dbo.Normes", "Norme_Id");
            AddForeignKey("dbo.Normes", "Norme_Id", "dbo.Normes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Normes", "Norme_Id", "dbo.Normes");
            DropIndex("dbo.Normes", new[] { "Norme_Id" });
            DropColumn("dbo.Normes", "Norme_Id");
        }
    }
}
