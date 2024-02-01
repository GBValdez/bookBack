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
    public abstract class controllerCommons<TEntity, TDtoCreation, TDto> : ControllerBase
    where TEntity : CommonsModel
    where TDto : class
    where TDtoCreation : class

    {
        protected readonly AplicationDBContex context;
        protected readonly IMapper mapper;
        public controllerCommons(AplicationDBContex context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<resPag<TDto>>> get([FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            int total = await context.Set<TEntity>()
                .Where(Db => Db.deleteAt == null)
                .CountAsync();
            double totalDB = total;
            if (pageNumber > Math.Ceiling(totalDB / pageSize))
                return BadRequest(new errorMessageDto("El indice de la pagina es mayor que el numero de paginas total"));
            IQueryable<TEntity> dbObjs = context.Set<TEntity>()
                .Where(db => db.deleteAt == null)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
            List<TEntity> listDb = await this.modifyGet(dbObjs).ToListAsync();
            List<TDto> listDto = mapper.Map<List<TDto>>(listDb);
            return new resPag<TDto>
            {
                items = listDto,
                total = total,
                index = pageNumber

            };

        }

        public abstract IQueryable<TEntity> modifyGet(IQueryable<TEntity> query);

    }
}