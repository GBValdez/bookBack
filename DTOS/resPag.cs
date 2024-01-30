using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prueba.DTOS
{
    public class resPag<T>
    {

        public List<T> items { get; set; }
        public int total { get; set; }
        public int index { get; set; }
    }
}