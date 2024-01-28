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
    public class CountryController : ControllerBase
    {
        private readonly AplicationDBContex context;
        private readonly IMapper mapper;
        public CountryController(AplicationDBContex context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Country>>> get()
        {
            return await context.Country.ToListAsync();

        }

        [HttpPost]
        public async Task<ActionResult<catalogueDto>> post(catalogueCreationDto country)
        {
            Country newCountry = mapper.Map<Country>(country);
            context.Add(newCountry);
            await context.SaveChangesAsync();
            return mapper.Map<catalogueDto>(newCountry);
        }

    }
}