namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajoutdbSetmesureetproj : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mesures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Mesures_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mesures", t => t.Mesures_Id)
                .Index(t => t.Mesures_Id);
            
            CreateTable(
                "dbo.Projets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Provider = c.Int(nullable: false),
                        Projets_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projets", t => t.Projets_Id)
                .Index(t => t.Projets_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projets", "Projets_Id", "dbo.Projets");
            DropForeignKey("dbo.Mesures", "Mesures_Id", "dbo.Mesures");
            DropIndex("dbo.Projets", new[] { "Projets_Id" });
            DropIndex("dbo.Mesures", new[] { "Mesures_Id" });
            DropTable("dbo.Projets");
            DropTable("dbo.Mesures");
        }
    }
}
