using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using prueba.interfaces;

namespace prueba.entities
{
    public class rolEntity : IdentityRole, ICommonModel<string>
    {
        public DateTime updateAt { get; set; }
        public string userUpdateId { get; set; }

        public DateTime? deleteAt { get; set; }
        public userEntity userUpdate { get; set; }
    }
}