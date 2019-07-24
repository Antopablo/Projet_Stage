namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relationsmesures : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mesures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Projets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        CloudProvider = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RelationsMesuresExigences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdExigence = c.Int(nullable: false),
                        IdMesures = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RelationsMesuresExigences");
            DropTable("dbo.Projets");
            DropTable("dbo.Mesures");
        }
    }
}
