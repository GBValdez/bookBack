using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using prueba.DTOS;
using prueba.entities;

namespace prueba.Controllers
{
    [ApiController]
    [Route("language")]
    public class languageController : controllerCommons<Language, catalogueCreationDto, catalogueDto, object, object, int>
    {
        public languageController(AplicationDBContex context, IMapper mapper)
       : base(context, mapper)
        { }
        protected override bool showDeleted { get; set; } = true;
    }
}