using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BooksStore.Domain.Entities;
namespace BooksStore.Domain.Concrete
{
  public  class EFDbContext: DbContext
    {
        public EFDbContext() : base("BooksStore")
        { }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

    }

    public class UserDbInitializer : DropCreateDatabaseAlways<EFDbContext>
    {
        protected override void Seed(EFDbContext db)
        {
            Role admin = new Role { Name = "admin" };
            Role user = new Role { Name = "user" };
            db.Roles.Add(admin);
            db.Roles.Add(user);
            db.Users.Add(new User
            {
                Email = "somemail@gmail.com",
                Password = "123456",
                Role = admin
            });
            base.Seed(db);
        }
    }
}
