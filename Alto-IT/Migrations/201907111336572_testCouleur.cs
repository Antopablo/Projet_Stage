namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testCouleur : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exigences", "Couleur", c => c.String());
            DropColumn("dbo.Exigences", "orange");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Exigences", "orange", c => c.String());
            DropColumn("dbo.Exigences", "Couleur");
        }
    }
}
