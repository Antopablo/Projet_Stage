namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class recupobjecombobox : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mesures", "FK_to_Projets", c => c.Int(nullable: false));
            AddColumn("dbo.Mesures", "DocumentPath", c => c.String());
            AddColumn("dbo.Mesures", "DocumentName", c => c.String());
            AddColumn("dbo.Mesures", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mesures", "Status");
            DropColumn("dbo.Mesures", "DocumentName");
            DropColumn("dbo.Mesures", "DocumentPath");
            DropColumn("dbo.Mesures", "FK_to_Projets");
        }
    }
}
