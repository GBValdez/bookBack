using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prueba.DTOS
{
    public class userUpdateDto
    {
        public Boolean status { get; set; }
        public List<string> roles { get; set; }
    }
}