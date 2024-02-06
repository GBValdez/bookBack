using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace prueba.DTOS
{
    public class commentsParams
    {
        [FromRoute]
        public int BookId { get; set; }
    }
}