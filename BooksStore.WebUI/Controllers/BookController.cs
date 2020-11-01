using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BooksStore.Domain.Entities;
using BooksStore.Domain.Abstract;
using BooksStore.WebUI.Models;


namespace BooksStore.WebUI.Controllers
{
    public class BookController : Controller
    {
            private IBookRepository repository;
            public int PageSize = 8;
            public BookController(IBookRepository bookRepository)
            {
                this.repository = bookRepository;
            }

        public FileContentResult GetImage(int bookId)
        {
            Book book = repository.Books.FirstOrDefault(p => p.BookID == bookId);
            if (book != null)
            {
                return File(book.ImageData, book.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        public ViewResult List(string genre, int page = 1)
        {
            BooksListViewModel model = new BooksListViewModel
            {
                Books = repository.Books
                 .Where(p => genre == null || p.Genre == genre  )
                 .OrderBy(p => p.BookID)
                 .Skip((page - 1) * PageSize)
                 .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = genre == null ?
                      repository.Books.Count() :
                      repository.Books.Where(e => e.Genre == genre).Count()
                },
                CurrentGenre = genre

            };
            return View(model);
            }

        public ViewResult Detail(int bookId)
        {
            Book book = repository.Books
                .FirstOrDefault(b => b.BookID == bookId);
            return View(book);
        }

    }
}