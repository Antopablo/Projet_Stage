namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class binding : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exigences", "Ischecked", c => c.Boolean(nullable: false));
            AddColumn("dbo.Mesures", "Ischecked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mesures", "Ischecked");
            DropColumn("dbo.Exigences", "Ischecked");
        }
    }
}
