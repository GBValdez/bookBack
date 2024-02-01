using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace prueba.DTOS
{
    public class bookCreationDto
    {
        [Required]
        [StringLength(50)]
        public string title { get; set; }
        [Required]
        [StringLength(250)]
        public string description { get; set; }

        [Required]
        public DateTime dateCreation { get; set; }
        [Required]
        public List<int> authorIds { get; set; }
        [Required]
        public List<int> categoriesId { get; set; }
        [Required]
        public int numPages { get; set; }
        [Required]
        public int languageId { get; set; }

    }
}