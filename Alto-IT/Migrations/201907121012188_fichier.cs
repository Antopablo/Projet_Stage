namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fichier : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exigences", "DocumentPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exigences", "DocumentPath");
        }
    }
}
