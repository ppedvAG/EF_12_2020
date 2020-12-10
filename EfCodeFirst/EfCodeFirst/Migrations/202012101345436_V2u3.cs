namespace EfCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V2u3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mitarbeiter", "Ps", c => c.Int(nullable: false));
            AddColumn("dbo.Mitarbeiter", "AnzahlFinger", c => c.Int(nullable: false));
            AddColumn("dbo.Kunden", "Schuhgröße", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Kunden", "Schuhgröße");
            DropColumn("dbo.Mitarbeiter", "AnzahlFinger");
            DropColumn("dbo.Mitarbeiter", "Ps");
        }
    }
}
