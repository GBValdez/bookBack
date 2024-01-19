using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prueba.entities;
using prueba.filters;
using prueba.services;

namespace prueba.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class authorsController : ControllerBase
    {


        private readonly AplicationDBContex context;
        private readonly getRandomNum num;
        private readonly ILogger<authorsController> logger;
        public authorsController(AplicationDBContex context, getRandomNum num, ILogger<authorsController> logger)
        {
            this.context = context;
            this.num = num;
            this.logger = logger;
        }
        // Con un eslas en el inicio de la ruta la ruta ya no es relativa sino absoluta
        [HttpGet("/getRandom")]
        [ServiceFilter(typeof(MyFilterAction))]

        [ResponseCache(Duration = 15)]
        public ActionResult<Dictionary<string, int>> getRandom()
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            dic.Add("num", num.num);
            return Ok(dic);
        }

        [HttpGet()]
        [ServiceFilter(typeof(MyFilterAction))]

        public async Task<ActionResult<List<Author>>> get()
        {
            throw new NotImplementedException();
            return await context.Authors.Include(x => x.books).ToListAsync();
        }

        //Con poner el signo de interrogacion al final del parametro lo hacemos opcional y puedes setear un valor por defecto
        [HttpGet("{id:int}/{param2=valor2}")]
        public async Task<ActionResult<Author>> get(int id)
        {
            var author = await context.Authors.Include(x => x.books).FirstOrDefaultAsync(x => x.id == id);
            if (author == null)
            {
                return NotFound();
            }
            return author;
        }

        [HttpPost()]
        public async Task<ActionResult> post(Author author)
        {
            var exits = await context.Authors.AnyAsync(x => x.name == author.name);
            if (exits)
            {
                logger.LogError($"El autor {author.name} ya existe");
                return BadRequest($"{author.name} ya existe");
            }
            context.Add(author);
            await context.SaveChangesAsync();
            logger.LogInformation($"Se creo el autor {author.name} con id {author.id}");
            return Ok();
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> put(Author author, int id)
        {
            if (author.id != id)
            {
                return BadRequest("Id no coincide");
            }
            var exits = await context.Authors.AnyAsync(x => x.id == id);
            if (!exits)
            {
                return NotFound();
            }
            context.Update(author);
            await context.SaveChangesAsync();
            return Ok();
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