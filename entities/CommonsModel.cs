using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace prueba.entities
{
    public class CommonsModel
    {
        public DateTime updateAt { get; set; } = DateTime.Now.ToUniversalTime();
        public string userUpdateId { get; set; }

        [ForeignKey("userUpdateId")]
        public virtual IdentityUser userUpdate { get; set; }
        public DateTime? deleteAt { get; set; }
    }
}