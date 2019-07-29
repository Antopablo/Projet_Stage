namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class binding1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Exigences", "Ischecked");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Exigences", "Ischecked", c => c.Boolean(nullable: false));
        }
    }
}
