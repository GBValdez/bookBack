using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using prueba.interfaces;

namespace prueba.entities
{
    public class userEntity : IdentityUser, ICommonModel<string>
    {
        public DateTime updateAt { get; set; } = DateTime.Now.ToUniversalTime();
        public string? userUpdateId { get; set; }
        public DateTime? deleteAt { get; set; }
        public userEntity userUpdate { get; set; }
    }
}