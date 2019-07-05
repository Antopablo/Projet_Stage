namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class exigene : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exigences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ForeignKey = c.Int(nullable: false),
                        Exigence_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exigences", t => t.Exigence_Id)
                .Index(t => t.Exigence_Id);
            
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
            DropForeignKey("dbo.Exigences", "Exigence_Id", "dbo.Exigences");
            DropIndex("dbo.Exigences", new[] { "Exigence_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Exigences");
        }
    }
}
