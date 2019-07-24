namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FkToproj : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exigences", "FKToProjet", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exigences", "FKToProjet");
        }
    }
}
