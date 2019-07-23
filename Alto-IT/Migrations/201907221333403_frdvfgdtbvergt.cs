namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class frdvfgdtbvergt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RelationMesureExigences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdMesure = c.Int(nullable: false),
                        IdExigence = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RelationMesureExigences");
        }
    }
}
