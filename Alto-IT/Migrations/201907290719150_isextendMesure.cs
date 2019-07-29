namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isextendMesure : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Exigences", "IsNodeExpanded");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Exigences", "IsNodeExpanded", c => c.Boolean(nullable: false));
        }
    }
}
