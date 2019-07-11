namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class erefe : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Exigences", "orange");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Exigences", "orange", c => c.String());
        }
    }
}
