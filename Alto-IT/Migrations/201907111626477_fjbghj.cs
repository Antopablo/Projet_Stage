namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fjbghj : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exigences", "Couleur", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exigences", "Couleur");
        }
    }
}
