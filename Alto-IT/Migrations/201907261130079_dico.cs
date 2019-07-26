namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dico : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Mesures", "Ischecked");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Mesures", "Ischecked", c => c.Boolean(nullable: false));
        }
    }
}
