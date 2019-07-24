namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sjgvdsnj : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mesures", "FKToProjets", c => c.Int(nullable: false));
            AddColumn("dbo.Mesures", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mesures", "Status");
            DropColumn("dbo.Mesures", "FKToProjets");
        }
    }
}
