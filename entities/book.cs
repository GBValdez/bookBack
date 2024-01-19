using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prueba.entities
{
    public class book
    {
        public int id { get; set; }
        public string title { get; set; }
        public int AuthorId { get; set; }
        public Author author { get; set; }
    }
}