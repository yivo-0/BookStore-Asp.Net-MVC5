using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BooksStore.Domain.Entities
{
   public class Book
    {
        [HiddenInput(DisplayValue = false)]
        public int BookID { get; set; }

        [Required(ErrorMessage = "Please enter a book name")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }


        public string Author { get; set; }

        [Required(ErrorMessage = "Please specify a category")]
        public string Genre { get; set; }

        public int Year { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }

        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
    }
}
