using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookAPI.Models;
using System.Web.Http.Cors;
using System.Data.SqlClient;

namespace BookAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BookController : ApiController
    {
        private IBookRepository repository;

        public BookController()
        {
            this.repository = new BookSQLImplementation();
        }
        [HttpGet]
        public Book Get(int id)
        {
            return repository.FetchBookById(id);
        }

        [HttpGet]
        public List<Book> Get()
        {
            return repository.FetchBooks();
        }

        [HttpPost]
        public Book Post(Book book)
        {
            return repository.AddBook(book);
        }

        [HttpPut]
        public Book Put(int id, Book book)
        {
            return repository.ChangeOrAddBookById(id, book);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            repository.DeleteBookById(id);
        }
        [HttpPatch]
        public string Patch(int id, Book book)
        {
            return "Not Implemented";
        }
    }
}
