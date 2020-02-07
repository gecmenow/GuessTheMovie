namespace GuessTheMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeddb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FilmsDBs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FilmCode = c.String(),
                        Name = c.String(),
                        Year = c.Int(nullable: false),
                        Genre = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FilmsDBs");
        }
    }
}
