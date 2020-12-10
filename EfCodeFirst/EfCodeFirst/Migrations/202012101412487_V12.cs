namespace EfCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mitarbeiter", "TolleFeld", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mitarbeiter", "TolleFeld");
        }
    }
}
