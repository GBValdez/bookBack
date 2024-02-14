using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prueba.DTOS;
using prueba.entities;

namespace prueba.Controllers
{
    [ApiController]
    [Route("country")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMINISTRATOR")]

    public class CountryController : controllerCommons<Country, catalogueCreationDto, catalogueDto, catalogueQueryDto, object, int>
    {
        public CountryController(AplicationDBContex context, IMapper mapper)
       : base(context, mapper)
        { }

        [AllowAnonymous]
        public override Task<ActionResult<resPag<catalogueDto>>> get([FromQuery] int pageSize, [FromQuery] int pageNumber, [FromQuery] catalogueQueryDto queryParams, [FromQuery] bool? all = false)
        {
            return base.get(pageSize, pageNumber, queryParams, all);
        }

        protected override async Task<errorMessageDto> validDelete(Country entity)
        {
            Boolean exist = await context.Authors.AnyAsync(authorDb => authorDb.countryId == entity.Id && authorDb.deleteAt == null);
            if (exist)
                return new errorMessageDto("No se puede eliminar el país porque esta siendo relacionado a un autor");
            return null;
        }
    }
}