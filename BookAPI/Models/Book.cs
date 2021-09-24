using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookAPI.Models
{
    public class Book
    {
        public int Id { get; private set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public String Author { get; set; }
        public float Price { get; set; }

        public Book()
        {
            ISBN = "";
            Title = "";
            Author = "";
            Price = 0;
        }

        public void SetId(int id)
        {
            this.Id = id;
        }

        public void PatchUp(Book book)
        {
            this.ISBN = (book.ISBN != null) ? book.ISBN : this.ISBN;
            this.Title = (book.Title != null) ? book.Title : this.Title;
            this.Author = (book.Author != null) ? book.Author : this.Author;
            this.Price = (book.Price != 0.0) ? book.Price : this.Price;
        }
    }
}