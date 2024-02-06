using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prueba.DTOS;
using prueba.entities;

namespace prueba.Controllers
{
    public class controllerCommons<TEntity, TDtoCreation, TDto, TQuery, TQueryCreation> : ControllerBase
    where TEntity : CommonsModel
    where TDto : class
    where TDtoCreation : class
    where TQuery : class
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
        public async Task<ActionResult<resPag<TDto>>> get([FromQuery] int pageSize, [FromQuery] int pageNumber, [FromQuery] TQuery queryParams)
        {
            IQueryable<TEntity> query = context.Set<TEntity>().Where(db => db.deleteAt == null);

            var queryableType = typeof(TEntity);
            var properties = typeof(TQuery).GetProperties()
                .Where(prop => prop.GetValue(queryParams) != null); // Considera solo propiedades no nulas

            foreach (var property in properties)
            {
                var value = property.GetValue(queryParams);
                var entityProperty = queryableType.GetProperty(property.Name);

                if (entityProperty != null)
                {
                    // Construye una expresión lambda dinámicamente y aplica el filtro
                    var parameter = Expression.Parameter(typeof(TEntity), "e");
                    var propertyAccess = Expression.MakeMemberAccess(parameter, entityProperty);
                    var constantValue = Expression.Constant(value);
                    var equality = Expression.Equal(propertyAccess, constantValue);
                    var lambda = Expression.Lambda<Func<TEntity, bool>>(equality, parameter);

                    query = query.Where(lambda);
                }
            }


            int total = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)total / pageSize);
            if (pageNumber > totalPages)
                return BadRequest(new errorMessageDto("El indice de la pagina es mayor que el numero de paginas total"));

            List<TEntity> listDb = await this.modifyGet(query, queryParams)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            modifyGetResult(listDb);
            listDb = listDb.OrderBy(db => db.updateAt).ToList();
            List<TDto> listDto = mapper.Map<List<TDto>>(listDb);
            return new resPag<TDto>
            {
                items = listDto,
                total = total,
                index = pageNumber,
                totalPages = totalPages
            };
        }
        protected virtual IQueryable<TEntity> modifyGet(IQueryable<TEntity> query, TQuery queryParams)
        {
            return query;
        }
        protected virtual void modifyGetResult(List<TEntity> list) { }

        [HttpPost]
        public async Task<ActionResult<TDto>> post(TDtoCreation newRegister, [FromQuery] TQueryCreation queryParams)
        {
            errorMessageDto error = await this.validPost(newRegister, queryParams);
            if (error != null)
                return BadRequest(error);
            TEntity newRegisterEntity = this.mapper.Map<TEntity>(newRegister);
            await this.modifyPost(newRegisterEntity, queryParams);
            context.Add(newRegisterEntity);
            await context.SaveChangesAsync();
            return this.mapper.Map<TDto>(newRegisterEntity);
        }
        protected virtual Task<errorMessageDto> validPost(TDtoCreation dtoNew, TQueryCreation queryParams)
        {
            return null;
        }
        protected async virtual Task modifyPost(TEntity entity, TQueryCreation queryParams)
        {
            return;
        }

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