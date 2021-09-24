using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAPI.Models
{
    interface IBookRepository
    {
        List<Book> FetchBooks();
        Book FetchBookById(int id);
        Book AddBook(Book book);
        Book ChangeOrAddBookById(int id, Book book);
        void DeleteBookById(int id);
        Book UpdateBookById(int id, Book book);
    }
}
