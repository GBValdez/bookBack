using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prueba.entities
{
    public class Category : CommonsModel<int>
    {
        public string name { get; set; }
    }
}