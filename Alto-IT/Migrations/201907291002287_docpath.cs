namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class docpath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mesures", "DocumentName", c => c.String());
            AddColumn("dbo.Mesures", "DocumentPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mesures", "DocumentPath");
            DropColumn("dbo.Mesures", "DocumentName");
        }
    }
}
