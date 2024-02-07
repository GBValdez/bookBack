using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prueba.DTOS
{
    public class bookQueryDto
    {
        public string titleCont { get; set; }
        public DateTime? dateCreationGreat { get; set; }
        public DateTime? dateCreationSmall { get; set; }
        public int? numPages { get; set; }
        public int? languageId { get; set; }
        public string categoriesId { get; set; }

    }
}