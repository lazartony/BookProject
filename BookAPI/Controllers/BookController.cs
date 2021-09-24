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
    [EnableCors(origins:"*",headers:"*",methods:"*")]
    public class BookController : ApiController
    {
        public static List<Book> booksList = new List<Book>();
        public static string connectionString = @"server=localhost\MSSQLSERVER02; database=BookAPIDemo;trusted_connection=yes";

        [HttpGet]
        public Book GetBook(int id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;
            comm.CommandText = "select * from Books where id = @id";
            comm.Parameters.AddWithValue("@id", id);
            SqlDataReader dr = comm.ExecuteReader();
            Book book = null;
            if (dr.HasRows)
            {
                dr.Read();
                book = new Book() { Title = dr["Title"].ToString(), ISBN = dr["ISBN"].ToString(), Author = dr["Author"].ToString(), Price = (float)Convert.ToDouble(dr["Price"]) };
                book.SetId(Convert.ToInt32(dr["Id"]));
            }
            conn.Close();
            return book;
        }

        [HttpGet]
        public List<Book> GetBooks()
        {
            List<Book> booksList = new List<Book>();
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;
            comm.CommandText = "select * from Books";
            SqlDataReader dr = comm.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Book book = new Book() { Title = dr["Title"].ToString(), ISBN = dr["ISBN"].ToString(), Author = dr["Author"].ToString(), Price = (float)Convert.ToDouble(dr["Price"]) };
                    book.SetId(Convert.ToInt32(dr["Id"]));
                    booksList.Add(book);
                }
            }
            conn.Close();
            return booksList;
        }

        [HttpPost]
        public Book PostBook(Book book)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;
            comm.CommandText = "insert into Books(Title, ISBN, Author, Price) values (@title, @isbn, @author, @price); SELECT SCOPE_IDENTITY()";
            comm.Parameters.AddWithValue("@title",book.Title);
            comm.Parameters.AddWithValue("@isbn",book.ISBN);
            comm.Parameters.AddWithValue("@author",book.Author);
            comm.Parameters.AddWithValue("@price",book.Price);
            int id = Convert.ToInt32(comm.ExecuteScalar());
            book.SetId(id);
            conn.Close();
            return book;
        }

        [HttpPut]
        public Book PutBook(int id, Book book)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;
            comm.CommandText = "select * from Books where id = @id";
            comm.Parameters.AddWithValue("@id", id);
            int rows = Convert.ToInt32(comm.ExecuteScalar());
            conn.Close();

            conn.Open();
            comm = new SqlCommand();
            comm.Connection = conn;
            if (rows >= 1)
            {
                comm.CommandText = "update Books set Title = @title, ISBN = @isbn, Author = @author, Price = @price where Id = @id";
                comm.Parameters.AddWithValue("@title", book.Title);
                comm.Parameters.AddWithValue("@isbn", book.ISBN);
                comm.Parameters.AddWithValue("@author", book.Author);
                comm.Parameters.AddWithValue("@price", book.Price);
                comm.Parameters.AddWithValue("@id", id);
                comm.ExecuteNonQuery();
                book.SetId(id);
            }
            else
            {
                comm.CommandText = "insert into Books(Title, ISBN, Author, Price) values (@title, @isbn, @author, @price); SELECT SCOPE_IDENTITY()";
                comm.Parameters.AddWithValue("@title", book.Title);
                comm.Parameters.AddWithValue("@isbn", book.ISBN);
                comm.Parameters.AddWithValue("@author", book.Author);
                comm.Parameters.AddWithValue("@price", book.Price);
                int newId = Convert.ToInt32(comm.ExecuteScalar());
                book.SetId(newId);
            }
            conn.Close();
            return book;
        }

        [HttpDelete]
        public Book DeleteBook(int id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;
            comm.CommandText = "select * from Books where id = @id";
            comm.Parameters.AddWithValue("@id", id);
            SqlDataReader dr = comm.ExecuteReader();
            Book book = null;
            if (dr.HasRows)
            {
                dr.Read();
                book = new Book() { Title = dr["Title"].ToString(), ISBN = dr["ISBN"].ToString(), Author = dr["Author"].ToString(), Price = (float)Convert.ToDouble(dr["Price"]) };
                book.SetId(Convert.ToInt32(dr["Id"]));
            }
            conn.Close();
            if(book != null)
            {
                conn.Open();
                comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "delete from Books where id = @id";
                comm.Parameters.AddWithValue("@id", id);
                int rows = comm.ExecuteNonQuery();

            }
            return book;
        }
        //[HttpPatch]
        //public Book PatchBook(int id, Book book)
        //{
        //    Book oldBook = booksList.FirstOrDefault(b => b.Id == id);
        //    oldBook.PatchUp(book);
        //    return oldBook;
        //}
    }
}
