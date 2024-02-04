using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using prueba.entities;

namespace prueba.DTOS
{
    public class CommentsDto
    {
        public int id { get; set; }
        public string content { get; set; }
        public userDto user { get; set; }


    }
}