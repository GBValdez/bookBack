using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace prueba.DTOS
{
    public class bookPatchDto
    {

        [Required]
        [StringLength(30)]
        public string title { get; set; }
        public DateTime? dateCreation { get; set; }
    }
}