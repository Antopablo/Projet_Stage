namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _checked : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mesures", "IsChecked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mesures", "IsChecked");
        }
    }
}
