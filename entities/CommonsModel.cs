using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using prueba.interfaces;

namespace prueba.entities
{
    public class CommonsModel<idClass> : ICommonModel<idClass>
    {
        public idClass Id { get; set; }

        public string userUpdateId { get; set; }

        [ForeignKey("userUpdateId")]
        public userEntity userUpdate { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}