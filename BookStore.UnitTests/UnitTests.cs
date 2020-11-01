﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BooksStore.Domain.Abstract;
using BooksStore.Domain.Entities;
using BooksStore.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BookStore.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            // Arrange - create some test products
            Book p1 = new Book { BookID = 1, Name = "P1" };
            Book p2 = new Book { BookID = 2, Name = "P2" };

            // Arrange - create a new cart
            Cart target = new Cart();
            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results = target.Lines.ToArray();

            // Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Book, p1);
            Assert.AreEqual(results[1].Book, p2);
        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            // Arrange - create some test products
            Book p1 = new Book { BookID = 1, Name = "P1" };
            Book p2 = new Book { BookID = 2, Name = "P2" };

            // Arrange - create a new cart
            Cart target = new Cart();
            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            CartLine[] results = target.Lines.OrderBy(c => c.Book.BookID).ToArray();

            // Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Quantity, 11);
            Assert.AreEqual(results[1].Quantity, 1);
        }

        [TestMethod]
        public void Can_Remove_Line()
        {
            // Arrange - create some test products
            Book p1 = new Book { BookID = 1, Name = "P1" };
            Book p2 = new Book { BookID = 2, Name = "P2" };
            Book p3 = new Book { BookID = 3, Name = "P3" };

            // Arrange - create a new cart
            Cart target = new Cart();

            // Arrange - add some products to the cart
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            // Act
            target.RemoveLine(p2);

            // Assert
            Assert.AreEqual(target.Lines.Where(c => c.Book == p2).Count(), 0);
            Assert.AreEqual(target.Lines.Count(), 2);
        }

        [TestMethod]
        public void Calculate_Cart_Total()
        {
            // Arrange - create some test products
            Book p1 = new Book { BookID = 1, Name = "P1", Price = 100M };
            Book p2 = new Book { BookID = 2, Name = "P2", Price = 50M };

            // Arrange - create a new cart
            Cart target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            decimal result = target.ComputeTotalValue();

            // Assert
            Assert.AreEqual(result, 450M);
        }

        [TestMethod]
        public void Can_Clear_Contents()
        {
            // Arrange - create some test products
            Book p1 = new Book { BookID = 1, Name = "P1", Price = 100M };
            Book p2 = new Book { BookID = 2, Name = "P2", Price = 50M };

            // Arrange - create a new cart
            Cart target = new Cart();

            // Arrange - add some items
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            // Act - reset the cart
            target.Clear();

            // Assert
            Assert.AreEqual(target.Lines.Count(), 0);
        }
    }

}

