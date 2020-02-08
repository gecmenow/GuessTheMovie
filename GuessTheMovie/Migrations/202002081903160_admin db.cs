namespace GuessTheMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admindb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminDBs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AdminDBs");
        }
    }
}
