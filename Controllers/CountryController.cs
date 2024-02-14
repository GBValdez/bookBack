using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prueba.DTOS;
using prueba.entities;

namespace prueba.Controllers
{
    [ApiController]
    [Route("country")]
    public class CountryController : controllerCommons<Country, catalogueCreationDto, catalogueDto, object, object, int>
    {
        public CountryController(AplicationDBContex context, IMapper mapper)
       : base(context, mapper)
        { }
    }
}