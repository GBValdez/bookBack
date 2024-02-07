using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace prueba.DTOS
{
    public class authorQueryDto
    {
        [FromQuery()]
        public string nameCont { get; set; }
        public DateTime? birthDateGreat { get; set; }

        public DateTime? birthDateSmall { get; set; }

        public int? countryId { get; set; }

    }
}