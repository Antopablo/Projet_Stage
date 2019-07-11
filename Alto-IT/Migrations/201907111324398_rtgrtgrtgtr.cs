namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rtgrtgrtgtr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exigences", "orange", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exigences", "orange");
        }
    }
}
