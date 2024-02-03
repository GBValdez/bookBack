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

    public class AuthorsController : controllerCommons<Author, authorCreationDto, authorDto>
    {

        public AuthorsController(AplicationDBContex context, IMapper mapper)
        : base(context, mapper)
        { }

        protected override IQueryable<Author> modifyGet(IQueryable<Author> query)
        {
            return query.Include(authorDb => authorDb.country)
           .Select(authorDB => new Author { id = authorDB.id, name = authorDB.name, birthDate = authorDB.birthDate, country = authorDB.country });
        }
        //Con poner el signo de interrogacion al final del parametro lo hacemos opcional y puedes setear un valor por defecto
        [HttpGet("{id:int}", Name = "getAuthorId")]
        public async Task<ActionResult<authorDto>> getOne(int id, [FromQuery] Boolean? all = false)
        {
            IQueryable<Author> authorQuery = context.Authors.Include(authorDb => authorDb.country);
            if (all.Value)
                authorQuery = authorQuery
                   .Include(author => author.Author_Book)
                   .ThenInclude(authorBook => authorBook.Book);
            Author author = await authorQuery.FirstOrDefaultAsync(authorDB => authorDB.id == id && authorDB.deleteAt == null);

            if (author == null)
            {
                return NotFound();
            }
            return mapper.Map<authorDto>(author);
        }

        [HttpGet("byName")]
        public async Task<ActionResult<List<authorDto>>> getByName([FromQuery] string name)
        {
            var authors = await context.Authors.Where(authorDB => authorDB.name.ToLower().Contains(name.ToLower())).ToListAsync();
            if (authors == null)
                return NotFound();
            return mapper.Map<List<authorDto>>(authors);
        }

        protected override async Task<errorMessageDto> validPost(authorCreationDto newAuthor)
        {
            Boolean exits = await context.Authors.AnyAsync(x => x.name == newAuthor.name);
            if (exits)
                return new errorMessageDto($"{newAuthor.name} ya existe");
            Boolean existCountry = await context.Country.AnyAsync(countryDB => countryDB.id == newAuthor.countryId);
            if (!existCountry)
                return new errorMessageDto($"El pais con id {newAuthor.countryId} no existe");
            return null;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> put(authorCreationDto author, int id)
        {

            Boolean exits = await context.Authors
                .AnyAsync(authorDb => authorDb.id == id && authorDb.deleteAt == null);
            if (!exits)
            {
                return NotFound();
            }
            Author updAuthor = mapper.Map<Author>(author);
            updAuthor.id = id;
            context.Update(updAuthor);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}