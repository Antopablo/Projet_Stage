namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifaffichagnormes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Normes", "DocumentName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Normes", "DocumentName");
        }
    }
}
