using BooksStore.Domain.Abstract;
using BooksStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IBookRepository repository;

        public AdminController(IBookRepository repo)
        {
            repository = repo;
        }

        [Authorize(Roles = "admin")]
        public ViewResult Index()
        {
            return View(repository.Books);
        }

        public ViewResult Create()
        {
            return View("Edit", new Book());
        }

        public ViewResult Edit(int bookId)
        {
            Book book = repository.Books
                .FirstOrDefault(b => b.BookID == bookId);
            return View(book);
        }
        [HttpPost]
        public ActionResult Edit(Book book, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    book.ImageMimeType = image.ContentType;
                    book.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(book.ImageData, 0, image.ContentLength);
                }
                repository.SaveBook(book);
                TempData["message"] = string.Format("{0} has been saved", book.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(book);
            }
        }


        [HttpPost]
        public ActionResult Delete(int bookId)
        {
            Book deletedBook = repository.DeleteBook(bookId);
            if (deletedBook != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedBook.Name);
            }
            return RedirectToAction("Index");
        }
    }
}