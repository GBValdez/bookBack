using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace prueba.entities
{
    public class Book : CommonsModel<int>
    {
        [Required]
        [StringLength(50)]
        public string title { get; set; }
        [Required]
        [StringLength(250)]
        public string description { get; set; }
        [Required]
        public DateTime dateCreation { get; set; }
        [Required]
        public int numPages { get; set; }
        [Required]
        public int languageId { get; set; }
        public Language language { get; set; }
        public List<Comments> comments { get; set; }
        public List<Author_Book> Author_Book { get; set; }
        public List<Book_Category> Book_Category { get; set; }

    }
}