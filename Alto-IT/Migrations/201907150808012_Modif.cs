namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modif : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Exigences", "Couleur");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Exigences", "Couleur", c => c.String());
        }
    }
}
