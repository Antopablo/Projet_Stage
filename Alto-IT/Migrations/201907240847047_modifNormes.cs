namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifNormes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Normes", "FKToProjet", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Normes", "FKToProjet");
        }
    }
}
