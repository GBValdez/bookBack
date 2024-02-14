using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prueba.DTOS;
using prueba.entities;
using prueba.interfaces;

namespace prueba.Controllers
{
    public class controllerCommons<TEntity, TDtoCreation, TDto, TQuery, TQueryCreation, idClass> : ControllerBase
    where TEntity : class
    where TDto : class
    where TDtoCreation : class
    where TQuery : class
    {
        protected readonly AplicationDBContex context;
        protected readonly IMapper mapper;
        // protected readonly ILogger<AuthorsController> logger;
        protected virtual Boolean showDeleted { get; set; } = false;
        public controllerCommons(AplicationDBContex context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            // this.logger=logger;
        }

        [HttpGet()]
        [AllowAnonymous]
        public async Task<ActionResult<resPag<TDto>>> get([FromQuery] int pageSize, [FromQuery] int pageNumber, [FromQuery] TQuery queryParams, [FromQuery] Boolean? all = false)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();
            if (!showDeleted)
                query = query.Where(db => ((ICommonModel<idClass>)db).deleteAt == null);

            Type queryableType = typeof(TEntity);
            IEnumerable<PropertyInfo> properties = typeof(TQuery).GetProperties()
                .Where(prop => prop.GetValue(queryParams) != null); // Considera solo propiedades no nulas

            foreach (PropertyInfo property in properties)
            {
                var value = property.GetValue(queryParams);
                string entityPropertyName = property.Name;
                string operation = "Equal"; // Operación predeterminada

                // Determinar el nombre real de la propiedad y la operación basado en el nombre de la propiedad en TQuery
                if (entityPropertyName.EndsWith("Cont"))
                {
                    entityPropertyName = entityPropertyName.Replace("Cont", "");
                    operation = "Contains";
                }
                else if (entityPropertyName.EndsWith("Great"))
                {
                    entityPropertyName = entityPropertyName.Replace("Great", "");
                    operation = "GreaterThan";
                }
                else if (entityPropertyName.EndsWith("Small"))
                {
                    entityPropertyName = entityPropertyName.Replace("Small", "");
                    operation = "LessThan";
                }

                PropertyInfo entityProperty = queryableType.GetProperty(entityPropertyName);

                if (entityProperty != null)
                {
                    // Construye una expresión lambda dinámicamente y aplica el filtro
                    ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "e");
                    MemberExpression propertyAccess = Expression.MakeMemberAccess(parameter, entityProperty);
                    ConstantExpression constantValue = Expression.Constant(value);

                    Expression condition;
                    if (operation == "Contains" && entityProperty.PropertyType == typeof(string))
                    {    // Convertir tanto la propiedad de la entidad como el valor de comparación a minúsculas
                        MethodInfo toLowerMethod = typeof(string).GetMethod("ToLower", System.Type.EmptyTypes);

                        // Asegurarse de que propertyAccess y constantValue se conviertan a minúsculas
                        Expression propertyAccessToLower = Expression.Call(propertyAccess, toLowerMethod);
                        Expression constantValueToLower = Expression.Call(constantValue, toLowerMethod);
                        // Usar el método Contains para cadenas en la versión minúscula
                        MethodInfo containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        condition = Expression.Call(propertyAccessToLower, containsMethod, constantValueToLower);
                    }
                    else if (operation == "GreaterThan" || operation == "LessThan")
                    {
                        // Usar operadores GreaterThan o LessThan
                        condition = operation == "GreaterThan" ?
                            Expression.GreaterThanOrEqual(propertyAccess, constantValue) :
                            Expression.LessThanOrEqual(propertyAccess, constantValue);
                    }
                    else
                    {
                        // Igualdad por defecto
                        condition = Expression.Equal(propertyAccess, constantValue);
                    }

                    var lambda = Expression.Lambda<Func<TEntity, bool>>(condition, parameter);
                    query = query.Where(lambda);
                }
            }

            query = await this.modifyGet(query, queryParams);

            int total = await query.CountAsync();

            if (total == 0)
            {

                return NotFound(new errorMessageDto("No se encontraron registros"));
            }

            int totalPages = (int)Math.Ceiling((double)total / pageSize);

            if (pageNumber > totalPages && !all.Value)
                return BadRequest(new errorMessageDto("El indice de la pagina es mayor que el numero de paginas total"));

            if (pageNumber < 0 && !all.Value)
                return BadRequest(new errorMessageDto("El indice de la pagina no puede ser menor que 0"));

            if (all.Value == false)
                query = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            List<TEntity> listDb = await
                query
                .ToListAsync();
            modifyGetResult(listDb);
            listDb = listDb.OrderBy(db => ((ICommonModel<idClass>)db).updateAt).ToList();
            List<TDto> listDto = mapper.Map<List<TDto>>(listDb);
            return new resPag<TDto>
            {
                items = listDto,
                total = total,
                index = pageNumber,
                totalPages = totalPages
            };
        }
        protected async virtual Task<IQueryable<TEntity>> modifyGet(IQueryable<TEntity> query, TQuery queryParams)
        {
            return query;
        }
        protected virtual void modifyGetResult(List<TEntity> list) { }

        [HttpPost]
        public virtual async Task<ActionResult<TDto>> post(TDtoCreation newRegister, [FromQuery] TQueryCreation queryParams)
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
        protected async virtual Task<errorMessageDto> validPost(TDtoCreation dtoNew, TQueryCreation queryParams)
        {
            return null;
        }
        protected async virtual Task modifyPost(TEntity entity, TQueryCreation queryParams)
        {
            return;
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> delete(idClass id)
        {
            TEntity exits = await context.Set<TEntity>()
                .FirstOrDefaultAsync(Db => ((ICommonModel<idClass>)Db).Id.Equals(id) && ((ICommonModel<idClass>)Db).deleteAt == null);
            if (exits == null)
            {
                return NotFound();
            }
            ((ICommonModel<idClass>)exits).deleteAt = DateTime.Now.ToUniversalTime();
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult> put(TDtoCreation entityCurrent, [FromRoute] idClass id, [FromQuery] TQueryCreation queryCreation)
        {

            TEntity exits = await context.Set<TEntity>().FirstOrDefaultAsync(db => ((ICommonModel<idClass>)db).Id.Equals(id) && ((ICommonModel<idClass>)db).deleteAt == null);
            if (exits == null)
                return NotFound();
            errorMessageDto error = await this.validPut(entityCurrent, exits, queryCreation);
            if (error != null)
                return BadRequest(error);
            exits = mapper.Map(entityCurrent, exits);
            await context.SaveChangesAsync();
            return NoContent();
        }

        protected virtual async Task<errorMessageDto> validPut(TDtoCreation dtoNew, TEntity entity, TQueryCreation queryParams)
        {
            return null;
        }
    }
}