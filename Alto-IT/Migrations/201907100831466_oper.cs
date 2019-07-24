namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class oper : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exigences", "IdExigence", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exigences", "IdExigence");
        }
    }
}
