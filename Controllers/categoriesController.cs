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
    [Route("category")]
    public class categoriesController : controllerCommons<Category, catalogueCreationDto, catalogueDto, catalogueQueryDto, object, int>
    {
        public categoriesController(AplicationDBContex context, IMapper mapper)
       : base(context, mapper)
        { }
    }
}