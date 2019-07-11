namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initstatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exigences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IDExigence = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Status = c.Int(nullable: false),
                        ForeignKey = c.Int(nullable: false),
                        ForeignKey_TO_Norme = c.Int(nullable: false),
                        Exigence_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exigences", t => t.Exigence_Id)
                .Index(t => t.Exigence_Id);
            
            CreateTable(
                "dbo.Normes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom_Norme = c.String(),
                        IDNorme = c.Int(nullable: false),
                        Norme_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Normes", t => t.Norme_Id)
                .Index(t => t.Norme_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Identifiant = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Normes", "Norme_Id", "dbo.Normes");
            DropForeignKey("dbo.Exigences", "Exigence_Id", "dbo.Exigences");
            DropIndex("dbo.Normes", new[] { "Norme_Id" });
            DropIndex("dbo.Exigences", new[] { "Exigence_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Normes");
            DropTable("dbo.Exigences");
        }
    }
}
