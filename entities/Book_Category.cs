using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prueba.entities
{
    public class Book_Category
    {
        public int bookId { get; set; }
        public Book book { get; set; }
        public int categoryId { get; set; }
        public Category category { get; set; }
    }
}