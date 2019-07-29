namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isexpend : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exigences", "IsNodeExpanded", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exigences", "IsNodeExpanded");
        }
    }
}
