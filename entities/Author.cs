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
    public class Author : IValidatableObject
    {
        public int id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} debe tener {1} caracteres")]
        [FirstCapitalLetter]
        public string name { get; set; }

        [NotMapped]
        public int age { get; set; }
        public List<book> books { get; set; }

        //Validaciones desde el modelo
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (age < 18)
                yield return new ValidationResult("La persona tiene que ser mayor de edad", new string[] { nameof(age) });
        }
    }
}