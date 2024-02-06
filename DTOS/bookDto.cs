using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prueba.entities;

namespace prueba.DTOS
{
    public class bookDto
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime dateCreation { get; set; }
        public int numPages { get; set; }
        public catalogueDto language { get; set; }
        // public List<CommentsDto> comments { get; set; }
        public List<authorDto> authors { get; set; }
        public List<catalogueDto> categories { get; set; }

    }
}