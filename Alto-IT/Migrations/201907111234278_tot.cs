namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tot : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exigences", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exigences", "Status");
        }
    }
}
