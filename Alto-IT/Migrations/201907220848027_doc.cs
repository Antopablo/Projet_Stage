namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exigences", "DocumentWithoutExtension", c => c.String());
            AddColumn("dbo.Normes", "DocumentWithoutExtension", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Normes", "DocumentWithoutExtension");
            DropColumn("dbo.Exigences", "DocumentWithoutExtension");
        }
    }
}
