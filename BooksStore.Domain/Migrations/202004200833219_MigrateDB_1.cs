namespace BooksStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB_1 : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Books",
            //    c => new
            //        {
            //            BookID = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //            Description = c.String(nullable: false),
            //            Author = c.String(),
            //            Genre = c.String(nullable: false),
            //            Year = c.Int(nullable: false),
            //            Price = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            ImageData = c.Binary(),
            //            ImageMimeType = c.String(),
            //        })
            //    .PrimaryKey(t => t.BookID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                        Age = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "RoleId" });
          //  DropTable("dbo.Books");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
        }
    }
}
