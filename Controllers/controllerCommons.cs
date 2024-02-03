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
        // protected readonly ILogger<AuthorsController> logger;

        public controllerCommons(AplicationDBContex context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            // this.logger=logger;
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
                .Where(db => db.deleteAt == null);
            List<TEntity> listDb = await this.modifyGet(dbObjs)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            List<TDto> listDto = mapper.Map<List<TDto>>(listDb);
            return new resPag<TDto>
            {
                items = listDto,
                total = total,
                index = pageNumber

            };
        }
        protected abstract IQueryable<TEntity> modifyGet(IQueryable<TEntity> query);

        [HttpPost]
        public async Task<ActionResult<TDto>> post(TDtoCreation newRegister)
        {
            errorMessageDto error = await this.validPost(newRegister);
            if (error != null)
                return BadRequest(error);
            TEntity newRegisterEntity = this.mapper.Map<TEntity>(newRegister);
            context.Add(newRegisterEntity);
            await context.SaveChangesAsync();
            return this.mapper.Map<TDto>(newRegisterEntity);
        }
        protected abstract Task<errorMessageDto> validPost(TDtoCreation dtoNew);

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> delete(int id)
        {
            TEntity exits = await context.Set<TEntity>()
                .FirstOrDefaultAsync(Db => Db.id == id && Db.deleteAt == null);
            if (exits == null)
            {
                return NotFound();
            }
            exits.deleteAt = DateTime.Now.ToUniversalTime();
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}