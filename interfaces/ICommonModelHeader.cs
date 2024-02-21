using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prueba.entities;

namespace prueba.interfaces
{
    public interface ICommonModelHeader
    {
        public string userUpdateId { get; set; }

        public DateTime? deleteAt { get; set; }
        public userEntity userUpdate { get; set; }

    }
}