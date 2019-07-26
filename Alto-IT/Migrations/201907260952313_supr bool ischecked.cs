namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class suprboolischecked : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Exigences", "isChecked");
            DropColumn("dbo.Mesures", "isChecked");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Mesures", "isChecked", c => c.Boolean(nullable: false));
            AddColumn("dbo.Exigences", "isChecked", c => c.Boolean(nullable: false));
        }
    }
}
