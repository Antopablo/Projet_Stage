namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mesures1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mesures", "Mesures_Id", c => c.Int());
            CreateIndex("dbo.Mesures", "Mesures_Id");
            AddForeignKey("dbo.Mesures", "Mesures_Id", "dbo.Mesures", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mesures", "Mesures_Id", "dbo.Mesures");
            DropIndex("dbo.Mesures", new[] { "Mesures_Id" });
            DropColumn("dbo.Mesures", "Mesures_Id");
        }
    }
}
