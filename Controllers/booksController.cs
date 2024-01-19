using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prueba.entities;

namespace prueba.Controllers
{
    [ApiController]
    [Route("books")]
    public class booksController : ControllerBase
    {
        private readonly AplicationDBContex context;
        public booksController(AplicationDBContex context)
        {
            this.context = context;
        }
        [HttpGet("getList")]
        public async Task<ActionResult<List<book>>> get()
        {
            return await context.Books.Include(x => x.author).ToListAsync();
        }


        [HttpPost()]
        public async Task<ActionResult> post(book book)
        {
            var exits = await context.Authors.AnyAsync(x => x.id == book.AuthorId);
            if (!exits)
            {
                return BadRequest($"No existe el autor con id ${book.AuthorId}");
            }
            context.Add(book);
            await context.SaveChangesAsync();
            return Ok();

        }

    }
}