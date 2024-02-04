using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prueba.DTOS
{
    public class userCreationDto : credentialsDto
    {
        public string userName { get; set; }
    }
}