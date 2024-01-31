using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using prueba.DTOS;

namespace prueba.Controllers
{
    public abstract class contrCatalogueBase<TEntity> : ControllerBase
    where TEntity : class
    {
        protected readonly AplicationDBContex context;
        protected readonly IMapper mapper;
        public contrCatalogueBase(AplicationDBContex context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<catalogueDto>>> get()
        {
            List<TEntity> list = await context.Set<TEntity>().ToListAsync();
            return mapper.Map<List<catalogueDto>>(list);
        }

        [HttpPost]
        public async Task<ActionResult<catalogueDto>> post(catalogueCreationDto country)
        {
            TEntity newValue = mapper.Map<TEntity>(country);
            context.Add(newValue);
            await context.SaveChangesAsync();
            return mapper.Map<catalogueDto>(newValue);
        }

    }
}