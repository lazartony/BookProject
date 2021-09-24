using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookAPI.Models
{
    public class BookSQLImplementation : IBookRepository
    {
        public Book AddBook(Book book)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["mydb"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "insert into Books(Title, ISBN, Author, Price) values (@title, @isbn, @author, @price); SELECT SCOPE_IDENTITY()";
                comm.Parameters.AddWithValue("@title", book.Title);
                comm.Parameters.AddWithValue("@isbn", book.ISBN);
                comm.Parameters.AddWithValue("@author", book.Author);
                comm.Parameters.AddWithValue("@price", book.Price);
                conn.Open();
                int id = Convert.ToInt32(comm.ExecuteScalar());
                book.SetId(id);
            }
            return book;
        }

        public Book ChangeOrAddBookById(int id, Book book)
        {
            Book oldBook = FetchBookById(id);
            string connectionString = ConfigurationManager.ConnectionStrings["mydb"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                if (oldBook == null)
                {
                    comm.CommandText = "insert into Books(Title, ISBN, Author, Price) values (@title, @isbn, @author, @price); SELECT SCOPE_IDENTITY()";
                }
                else
                {
                    comm.CommandText = "update Books set Title = @title, ISBN = @isbn, Author = @author, Price = @price where Id = @id";
                    comm.Parameters.AddWithValue("@id", id);
                }
                comm.Parameters.AddWithValue("@title", book.Title);
                comm.Parameters.AddWithValue("@isbn", book.ISBN);
                comm.Parameters.AddWithValue("@author", book.Author);
                comm.Parameters.AddWithValue("@price", book.Price);
                conn.Open();
                if (oldBook == null)
                {
                    id = Convert.ToInt32(comm.ExecuteScalar());
                }
                else
                {
                    comm.ExecuteNonQuery();
                }
                book.SetId(id);
            }
            return book;
        }

        public void DeleteBookById(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["mydb"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "delete from Books where id = @id";
                comm.Parameters.AddWithValue("@id", id);
                conn.Open();
                int rows = comm.ExecuteNonQuery();
            }
        }

        public Book FetchBookById(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["mydb"].ConnectionString;
            Book book = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "select * from Books where Id = @id";
                comm.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader dr = comm.ExecuteReader();
                if (dr.Read())
                {
                    book = new Book() { Title = dr["Title"].ToString(), ISBN = dr["ISBN"].ToString(), Author = dr["Author"].ToString(), Price = (float)Convert.ToDouble(dr["Price"]) };
                    book.SetId(Convert.ToInt32(dr["Id"]));
                }
            }
            return book;
        }

        public List<Book> FetchBooks()
        {
            List<Book> books = new List<Book>();
            string connectionString = ConfigurationManager.ConnectionStrings["mydb"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "select * from Books";
                conn.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Book book = new Book() { Title = dr["Title"].ToString(), ISBN = dr["ISBN"].ToString(), Author = dr["Author"].ToString(), Price = (float)Convert.ToDouble(dr["Price"]) };
                    book.SetId(Convert.ToInt32(dr["Id"]));
                    books.Add(book);
                }
            }
            return books;
        }

        public Book UpdateBookById(int id, Book book)
        {
            Book oldBook = FetchBookById(id);
            string connectionString = ConfigurationManager.ConnectionStrings["mydb"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                if (oldBook != null)
                {
                    comm.CommandText = "update Books set Title = @title, ISBN = @isbn, Author = @author, Price = @price where Id = @id";
                    comm.Parameters.AddWithValue("@id", id);
                    comm.Parameters.AddWithValue("@title", (book.Title != "" && book.Title != null) ? book.Title : oldBook.Title);
                    comm.Parameters.AddWithValue("@isbn", (book.ISBN != "" && book.ISBN != null) ? book.ISBN : oldBook.ISBN);
                    comm.Parameters.AddWithValue("@author", (book.Author != "" && book.Author != null) ? book.Author : oldBook.Author);
                    comm.Parameters.AddWithValue("@price", (book.Price != 0) ? book.Price : oldBook.Price);
                    conn.Open();
                    comm.ExecuteNonQuery();
                    book.SetId(id);
                }

            }
            if (oldBook != null)
            {
                return book;
            }
            else
            {
                return null;
            }
        }
    }
}