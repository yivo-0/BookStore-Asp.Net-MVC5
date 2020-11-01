using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksStore.Domain.Entities;

namespace BooksStore.Domain.Abstract
{
   public interface IBookRepository
    {

        IQueryable<Book>Books{ get; }
        void SaveBook(Book book);
        Book DeleteBook(int bookID);

    }
}
