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
        public List<authorDto> authors { get; set; }
    }
}