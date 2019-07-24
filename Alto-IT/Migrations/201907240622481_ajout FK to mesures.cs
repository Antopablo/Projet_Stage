namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajoutFKtomesures : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mesures", "FK_to_Mesures", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mesures", "FK_to_Mesures");
        }
    }
}
