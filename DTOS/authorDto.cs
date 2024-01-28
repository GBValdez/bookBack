using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prueba.entities;

namespace prueba.DTOS
{
    public class authorDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<bookDto> books { get; set; }
        public Country country { get; set; }
        public string biography { get; set; }
        public DateTime birthDate { get; set; }
    }
}