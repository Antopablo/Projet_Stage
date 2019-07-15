namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modif2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exigences", "DocumentName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exigences", "DocumentName");
        }
    }
}
