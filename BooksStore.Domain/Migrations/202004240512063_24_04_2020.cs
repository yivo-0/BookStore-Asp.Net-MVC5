namespace BooksStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _24_04_2020 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Author", c => c.String());
            DropColumn("dbo.Books", "AuthorID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "AuthorID", c => c.String());
            DropColumn("dbo.Books", "Author");
        }
    }
}
