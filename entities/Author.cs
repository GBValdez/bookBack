using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using prueba.validators;

namespace prueba.entities
{
    //Al heredar de IValidatableObject podemos hacer validaciones desde el modelo
    public class Author : CommonsModel<int>
    {

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} debe tener {1} caracteres")]
        [FirstCapitalLetter]
        public string name { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int countryId { get; set; }
        public Country country { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 500, ErrorMessage = "El campo {0} debe tener {1} caracteres")]
        public string biography { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime birthDate { get; set; }
        public List<Author_Book> Author_Book { get; set; }
    }
}