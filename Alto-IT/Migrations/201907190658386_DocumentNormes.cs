namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DocumentNormes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Normes", "DocumentPath", c => c.String());
            AddColumn("dbo.Normes", "DocumentName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Normes", "DocumentName");
            DropColumn("dbo.Normes", "DocumentPath");
        }
    }
}
