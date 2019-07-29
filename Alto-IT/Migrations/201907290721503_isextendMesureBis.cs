namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isextendMesureBis : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exigences", "IsNodeExpanded", c => c.Boolean(nullable: false));
            AddColumn("dbo.Mesures", "IsNodeExpanded", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mesures", "IsNodeExpanded");
            DropColumn("dbo.Exigences", "IsNodeExpanded");
        }
    }
}
