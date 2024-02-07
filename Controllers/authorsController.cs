using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using prueba.DTOS;
using prueba.entities;
using prueba.services;

namespace prueba.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class AuthorsController : controllerCommons<Author, authorCreationDto, authorDto, authorQueryDto, object>
    {

        public AuthorsController(AplicationDBContex context, IMapper mapper)
        : base(context, mapper)
        { }

        protected override IQueryable<Author> modifyGet(IQueryable<Author> query, authorQueryDto queryParams)
        {
            return query.Include(authorDb => authorDb.country)
           .Select(authorDB => new Author { id = authorDB.id, name = authorDB.name, birthDate = authorDB.birthDate, country = authorDB.country });
        }
        //Con poner el signo de interrogacion al final del parametro lo hacemos opcional y puedes setear un valor por defecto
        [HttpGet("{id:int}", Name = "getAuthorId")]
        public async Task<ActionResult<authorDto>> getOne(int id, [FromQuery] Boolean? all = false)
        {
            IQueryable<Author> authorQuery = context.Authors.Include(authorDb => authorDb.country);
            Author author = await authorQuery.FirstOrDefaultAsync(authorDB => authorDB.id == id && authorDB.deleteAt == null);

            if (author == null)
                return NotFound();

            if (all.Value)
            {
                List<Author_Book> books = await context.Author_Book
                    .Where(
                    bookDb => bookDb.AuthorId == author.id && bookDb.Book.deleteAt == null
                    ).Include(bookDb => bookDb.Book)
                    .ToListAsync();
                author.Author_Book = books;
            }


            return mapper.Map<authorDto>(author);
        }

        [HttpGet("byName")]
        public async Task<ActionResult<List<authorDto>>> getByName([FromQuery] string name)
        {
            var authors = await context.Authors.Where(authorDB => authorDB.name.ToLower().Contains(name.ToLower()) && authorDB.deleteAt == null).ToListAsync();
            if (authors == null)
                return NotFound();
            return mapper.Map<List<authorDto>>(authors);
        }

        protected override async Task<errorMessageDto> validPost(authorCreationDto newAuthor, object obj)
        {
            Boolean exits = await context.Authors.AnyAsync(x => x.name == newAuthor.name);
            if (exits)
                return new errorMessageDto($"{newAuthor.name} ya existe");
            Boolean existCountry = await context.Country.AnyAsync(countryDB => countryDB.id == newAuthor.countryId);
            if (!existCountry)
                return new errorMessageDto($"El pais con id {newAuthor.countryId} no existe");
            return null;
        }

    }
}