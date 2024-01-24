using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using prueba.validators;

namespace prueba.DTOS
{
    public class authorCreationDto
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} debe tener {1} caracteres")]
        [FirstCapitalLetter]
        public string name { get; set; }

    }
}