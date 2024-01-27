using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace prueba.entities
{
    public class Book
    {
        public int id { get; set; }
        [Required]
        [StringLength(30)]
        public string title { get; set; }
        public string description { get; set; }
        public DateTime? dateCreation { get; set; }
        public int numPages { get; set; }
        public int languageId { get; set; }
        public Language language { get; set; }
        public List<Comments> comments { get; set; }
        public List<Author_Book> Author_Book { get; set; }
        public List<Book_Category> Book_Category { get; set; }

    }
}