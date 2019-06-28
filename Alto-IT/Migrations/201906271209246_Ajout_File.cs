namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ajout_File : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "Category", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "Category");
        }
    }
}
