namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SQL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Normes", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Normes", "Description");
        }
    }
}
