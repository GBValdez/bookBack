using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using prueba.entities;

namespace prueba.interfaces
{
    public interface ICommonModel<IdClass>
    {
        public IdClass Id { get; set; }

        public DateTime updateAt { get; set; }
        public string userUpdateId { get; set; }

        public DateTime? deleteAt { get; set; }
        public userEntity userUpdate { get; set; }

    }
}