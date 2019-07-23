namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sqrequestdocumentviewer : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Exigences", "DocumentWithoutExtension");
            DropColumn("dbo.Normes", "DocumentWithoutExtension");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Normes", "DocumentWithoutExtension", c => c.String());
            AddColumn("dbo.Exigences", "DocumentWithoutExtension", c => c.String());
        }
    }
}
