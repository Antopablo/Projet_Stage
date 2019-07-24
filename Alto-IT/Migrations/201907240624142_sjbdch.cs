namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sjbdch : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mesures", "FKToMesure", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mesures", "FKToMesure");
        }
    }
}
