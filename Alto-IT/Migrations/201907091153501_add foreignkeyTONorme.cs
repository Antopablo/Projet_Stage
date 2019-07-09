namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addforeignkeyTONorme : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exigences", "ForeignKey_TO_Norme", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exigences", "ForeignKey_TO_Norme");
        }
    }
}
