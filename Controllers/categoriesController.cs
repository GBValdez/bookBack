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
    [Route("category")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMINISTRATOR")]
    public class categoriesController : controllerCommons<Category, catalogueCreationDto, catalogueDto, catalogueQueryDto, object, int>
    {
        public categoriesController(AplicationDBContex context, IMapper mapper)
       : base(context, mapper)
        { }

        [AllowAnonymous]
        public override Task<ActionResult<resPag<catalogueDto>>> get([FromQuery] int pageSize, [FromQuery] int pageNumber, [FromQuery] catalogueQueryDto queryParams, [FromQuery] bool? all = false)
        {
            return base.get(pageSize, pageNumber, queryParams, all);
        }

        protected override async Task<errorMessageDto> validDelete(Category entity)
        {
            Boolean exist = await context.Books.AnyAsync(book =>
                book.Book_Category.Any(bookCategoyDb => bookCategoyDb.categoryId == entity.Id)
                && book.deleteAt == null);
            if (exist)
                return new errorMessageDto("No se puede eliminar la categoria porque esta siendo usada por un libro");
            return null;
        }
    }
}