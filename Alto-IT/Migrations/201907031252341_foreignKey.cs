namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Normes", "ForeignKey", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Normes", "ForeignKey");
        }
    }
}
