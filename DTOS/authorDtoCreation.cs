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
        [StringLength(maximumLength: 50, ErrorMessage = "El campo {0} debe tener {1} caracteres")]
        [FirstCapitalLetter]
        public string name { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int countryId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 500, ErrorMessage = "El campo {0} debe tener {1} caracteres")]
        public string biography { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime birthDate { get; set; }
    }
}