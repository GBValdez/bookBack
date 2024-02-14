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
    [Route("language")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMINISTRATOR")]

    public class languageController : controllerCommons<Language, catalogueCreationDto, catalogueDto, catalogueQueryDto, object, int>
    {
        public languageController(AplicationDBContex context, IMapper mapper)
       : base(context, mapper)
        { }

        [AllowAnonymous]
        public override Task<ActionResult<resPag<catalogueDto>>> get([FromQuery] int pageSize, [FromQuery] int pageNumber, [FromQuery] catalogueQueryDto queryParams, [FromQuery] bool? all = false)
        {
            return base.get(pageSize, pageNumber, queryParams, all);
        }
        protected override async Task<errorMessageDto> validDelete(Language entity)
        {
            Boolean exist = await context.Books.AnyAsync(book => book.languageId == entity.Id && book.deleteAt == null);
            if (exist)
                return new errorMessageDto("No se puede eliminar el idioma porque esta siendo usado por un libro");
            return null;
        }
    }
}