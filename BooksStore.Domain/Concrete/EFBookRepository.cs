using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksStore.Domain.Abstract;
using BooksStore.Domain.Entities;

namespace BooksStore.Domain.Concrete
{
  public  class EFBookRepository : IBookRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Book> Books
        {
            get { return context.Books; }
        }

        public void SaveBook(Book book)
        {
            if (book.BookID == 0)
            {
                context.Books.Add(book);
            }
            else
            {
                Book dbEntry = context.Books.Find(book.BookID);
                if (dbEntry != null)
                {
                    dbEntry.Name = book.Name;
                    dbEntry.Description = book.Description;
                    dbEntry.Price = book.Price;
                    dbEntry.Genre = book.Genre;
                    dbEntry.Author = book.Author;
                    dbEntry.ImageData = book.ImageData;
                    dbEntry.ImageMimeType = book.ImageMimeType;
                }
            }
            context.SaveChanges();
        }

        public Book DeleteBook(int bookID)
        {
            Book dbEntry = context.Books.Find(bookID);
            if (dbEntry != null)
            {
                context.Books.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
