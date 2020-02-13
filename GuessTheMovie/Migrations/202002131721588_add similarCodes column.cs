namespace GuessTheMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsimilarCodescolumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FilmsDBs", "SimilarFilmsCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FilmsDBs", "SimilarFilmsCode");
        }
    }
}
