using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prueba.DTOS
{
    public class userQueryDto
    {
        public string UserNameCont { get; set; }
        public string EmailCont { get; set; }
        public List<string> roles { get; set; }

        public Boolean? isActive { get; set; }

    }
}