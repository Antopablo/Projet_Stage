namespace Alto_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test_majVue2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.NormesDatabase", newName: "Normes");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Normes", newName: "NormesDatabase");
        }
    }
}
