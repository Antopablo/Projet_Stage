namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FKtoprojpourlesexigenes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exigences", "ForeignKey_TO_Projet", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exigences", "ForeignKey_TO_Projet");
        }
    }
}
