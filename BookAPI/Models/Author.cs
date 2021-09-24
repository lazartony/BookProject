using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookAPI.Models
{
    public class Author
    {
        public static int count = 0;
        public int Id { get; private set; }
        public string Name { get; set; }

        public Author()
        {
            this.Id = ++count;
        }
    }
}