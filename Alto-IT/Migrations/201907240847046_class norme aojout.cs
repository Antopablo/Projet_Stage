namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class classnormeaojout : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Normes", "FK_to_Projet", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Normes", "FK_to_Projet");
        }
    }
}
