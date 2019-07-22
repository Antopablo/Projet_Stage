namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zczv : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Normes", "DocumentPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Normes", "DocumentPath");
        }
    }
}
