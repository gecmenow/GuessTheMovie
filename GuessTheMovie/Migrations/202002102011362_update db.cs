namespace GuessTheMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdminDBs", "AdminCode", c => c.Int(nullable: false));
            AddColumn("dbo.AdminDBs", "Login", c => c.String());
            DropColumn("dbo.AdminDBs", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdminDBs", "Name", c => c.String());
            DropColumn("dbo.AdminDBs", "Login");
            DropColumn("dbo.AdminDBs", "AdminCode");
        }
    }
}
