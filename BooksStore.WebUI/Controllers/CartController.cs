using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BooksStore.Domain.Abstract;
using BooksStore.Domain.Entities;
using BooksStore.WebUI.Models;

namespace BooksStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IBookRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(IBookRepository repo, IOrderProcessor proc)
        {
            repository = repo;
            orderProcessor = proc;
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Index(Cart cart,string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }  
            else
            {
                return View(shippingDetails);
            }
        }


        public RedirectToRouteResult AddToCart(Cart cart,int bookId, string returnUrl)
        {
            Book book = repository.Books
            .FirstOrDefault(b => b.BookID== bookId);
            if (book != null)
            {
               cart.AddItem(book, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart,int bookId, string returnUrl)
        {
            Book book = repository.Books
            .FirstOrDefault(b => b.BookID == bookId);
            if (book != null)
            {
                cart.RemoveLine(book);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
    }
}