namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ivrseProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exigences", "IDExigence", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exigences", "IDExigence");
        }
    }
}
