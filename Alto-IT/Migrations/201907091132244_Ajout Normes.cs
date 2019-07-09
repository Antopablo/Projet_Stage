namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjoutNormes : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Normes", "Norme_Id", "dbo.Normes");
            DropIndex("dbo.Normes", new[] { "Norme_Id" });
            DropTable("dbo.Normes");
        }
    }
}
