using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prueba.DTOS
{
    public class authorDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<bookDto> books { get; set; }

    }
}