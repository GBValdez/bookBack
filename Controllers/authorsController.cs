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
        public async Task<ActionResult<List<Author>>> get()
        {
            return await context.Authors.ToListAsync();
        }

        //Con poner el signo de interrogacion al final del parametro lo hacemos opcional y puedes setear un valor por defecto
        [HttpGet("{id:int}", Name = "getAuthorId")]
        public async Task<ActionResult<authorDto>> get(int id)
        {
            Author author = await context.Authors.Include(author => author.Author_Book).ThenInclude(authorBook => authorBook.Book).FirstOrDefaultAsync(authorDB => authorDB.id == id);
            if (author == null)
            {
                return NotFound();
            }
            return mapper.Map<authorDto>(author);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<List<authorDto>>> getByName([FromRoute] string name)
        {
            var authors = await context.Authors.Where(authorDB => authorDB.name.Contains(name)).ToListAsync();
            if (authors == null)
                return NotFound();
            return mapper.Map<List<authorDto>>(authors);
        }

        [HttpPost()]
        public async Task<ActionResult> post(authorCreationDto author)
        {
            var exits = await context.Authors.AnyAsync(x => x.name == author.name);
            if (exits)
            {
                logger.LogError($"El autor {author.name} ya existe");
                return BadRequest($"{author.name} ya existe");
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

            Boolean exits = await context.Authors.AnyAsync(x => x.id == id);
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

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> delete(int id)
        {
            var exits = await context.Authors.AnyAsync(x => x.id == id);
            if (!exits)
            {
                return NotFound();
            }
            context.Remove(new Author { id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}