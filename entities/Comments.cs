using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace prueba.entities
{
    public class Comments : CommonsModel
    {
        public int id { get; set; }
        public string content { get; set; }
        public int BookId { get; set; }
        public Book book { get; set; }
        public string userId { get; set; }
        public IdentityUser user { get; set; }
    }
}