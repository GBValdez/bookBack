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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class authorsController : ControllerBase
    {


        private readonly AplicationDBContex context;
        private readonly ILogger<authorsController> logger;
        private readonly IMapper mapper;
        public authorsController(AplicationDBContex context, ILogger<authorsController> logger, IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;


        }


        [HttpGet()]
        public async Task<ActionResult<resPag<authorDto>>> get([FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            int total = await context.Authors
                .Where(authorDb => authorDb.deleteAt == null)
                .CountAsync();
            double totalDB = total;
            if (pageNumber > Math.Ceiling(totalDB / pageSize))
                return BadRequest(new errorMessageDto("El indice de la pagina es mayor que el numero de paginas total"));
            List<Author> authors = await context.Authors
            .Where(authorDb => authorDb.deleteAt == null)
            .Include(authorDb => authorDb.country)
            .Select(authorDB => new Author { id = authorDB.id, name = authorDB.name, birthDate = authorDB.birthDate, country = authorDB.country })
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
            List<authorDto> authorsDto = mapper.Map<List<authorDto>>(authors);
            return new resPag<authorDto>
            {
                items = authorsDto,
                total = total,
                index = pageNumber

            };
        }

        //Con poner el signo de interrogacion al final del parametro lo hacemos opcional y puedes setear un valor por defecto
        [HttpGet("{id:int}", Name = "getAuthorId")]
        public async Task<ActionResult<authorDto>> getOne(int id, [FromQuery] Boolean? all = false)
        {
            var authorQuery = context.Authors.Include(authorDb => authorDb.country);
            if (all.Value)
                authorQuery
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
            var authors = await context.Authors.Where(authorDB => authorDB.name.Contains(name)).ToListAsync();
            if (authors == null)
                return NotFound();
            return mapper.Map<List<authorDto>>(authors);
        }

        [HttpPost()]
        public async Task<ActionResult> post(authorCreationDto author)
        {
            Boolean exits = await context.Authors.AnyAsync(x => x.name == author.name);
            if (exits)
            {
                logger.LogError($"El autor {author.name} ya existe");
                return BadRequest(new errorMessageDto($"{author.name} ya existe"));
            }
            Boolean existCountry = await context.Country.AnyAsync(countryDB => countryDB.id == author.countryId);
            if (!existCountry)
            {
                logger.LogError($"El pais con id{author.countryId} no existe");
                return BadRequest(new errorMessageDto($"El pais con id {author.countryId} no existe"));
            }
            Author newAuthor = mapper.Map<Author>(author);
            context.Add(newAuthor);
            await context.SaveChangesAsync();
            authorDto newAuthorDto = mapper.Map<authorDto>(newAuthor);
            // logger.LogInformation($"Se creo el autor {author.name} con id {author.id}");
            return CreatedAtRoute("getAuthorId", new { id = newAuthor.id }, newAuthorDto);
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
            // updAuthor.updateAt = DateTime.Now.ToUniversalTime();
            // updAuthor.userUpdateId = userSvc.user.Id;
            context.Update(updAuthor);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> delete(int id)
        {
            Author exits = await context.Authors
                .FirstOrDefaultAsync(authorDb => authorDb.id == id && authorDb.deleteAt == null);
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