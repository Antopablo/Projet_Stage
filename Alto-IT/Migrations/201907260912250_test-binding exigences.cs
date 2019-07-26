namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testbindingexigences : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exigences", "isChecked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exigences", "isChecked");
        }
    }
}
