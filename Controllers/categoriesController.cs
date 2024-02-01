using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using prueba.entities;

namespace prueba.Controllers
{
    [ApiController]
    [Route("category")]
    public class categoriesController : contrCatalogueBase<Category>
    {
        public categoriesController(AplicationDBContex context, IMapper mapper)
       : base(context, mapper)
        { }
    }
}