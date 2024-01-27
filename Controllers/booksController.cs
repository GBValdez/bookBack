using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prueba.DTOS;
using prueba.entities;

namespace prueba.Controllers
{
    [ApiController]
    [Route("books")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]

    public class booksController : ControllerBase
    {
        private readonly AplicationDBContex context;
        private readonly IMapper mapper;
        private readonly ILogger<booksController> logger;
        public booksController(AplicationDBContex context, IMapper mapper, ILogger<booksController> logger)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet()]
        public async Task<ActionResult<List<Book>>> get()
        {
            return await context.Books.Include(bookDB => bookDB.comments).ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> put(int id, bookCreationDto book)
        {
            if (!await validListAuthorId(book))
                return BadRequest(new errorMessageDto("No existe alguno de los autores"));
            if (!await validListCategoryId(book))
                return BadRequest(new errorMessageDto("No existe alguno de los autores"));


            Book bookUpd = await context.Books
                .Include(bookDB => bookDB.Author_Book)
                .FirstOrDefaultAsync(bookDb => bookDb.id == id);
            if (bookUpd == null)
                return NotFound();
            bookUpd = mapper.Map(book, bookUpd);
            order(bookUpd);

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id:int}", Name = "getBookById"),]
        [AllowAnonymous]
        public async Task<ActionResult<bookDto>> getById(int id)
        {
            Book bookOnly = await context.Books
                .Include(bookDB => bookDB.Author_Book)
                .ThenInclude(bookAuthorDB => bookAuthorDB.Author)
                .FirstOrDefaultAsync(book => book.id == id);
            if (bookOnly == null)
                return NotFound();
            bookOnly.Author_Book = bookOnly.Author_Book.OrderBy(x => x.order).ToList();
            return mapper.Map<bookDto>(bookOnly);
        }

        [HttpPost()]
        public async Task<ActionResult> post(bookCreationDto book)
        {
            if (!await validListAuthorId(book))
                return BadRequest(new errorMessageDto("No existe alguno de los autores"));
            if (!await validListCategoryId(book))
                return BadRequest(new errorMessageDto("No existe alguno de los autores"));

            // List<Book> books=mapper.Map<Book>(book);
            Book bookEnd = mapper.Map<Book>(book);
            order(bookEnd);
            context.Add(bookEnd);
            await context.SaveChangesAsync();
            bookDto result = mapper.Map<bookDto>(bookEnd);
            return CreatedAtRoute("getBookById", new { id = bookEnd.id }, result);
        }

        private async Task<Boolean> validListAuthorId(bookCreationDto book)
        {
            List<int> authorsIds = await context.Authors
                .Where(authorDb => book.authorIds
                    .Contains(authorDb.id))
                .Select(authorDB => authorDB.id).ToListAsync();
            return authorsIds.Count == book.authorIds.Count;
        }
        private async Task<Boolean> validListCategoryId(bookCreationDto book)
        {
            List<int> authorsIds = await context.Category
                .Where(categoryDb => book.categoriesId
                    .Contains(categoryDb.id))
                .Select(categoryDb => categoryDb.id).ToListAsync();
            return authorsIds.Count == book.categoriesId.Count;
        }
        private void order(Book book)
        {
            for (int i = 0; i < book.Author_Book.Count; i++)
            {
                book.Author_Book[i].order = i;
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> patch(int id, JsonPatchDocument<bookPatchDto> patchDocument)
        {
            if (patchDocument == null)
                return BadRequest();
            Book book = await context.Books.FirstOrDefaultAsync(bookDb => bookDb.id == id);
            if (book == null)
                return NotFound();
            bookPatchDto bookPatch = mapper.Map<bookPatchDto>(book);
            patchDocument.ApplyTo(bookPatch, ModelState);
            if (!TryValidateModel(bookPatch))
                return BadRequest(ModelState);
            mapper.Map(bookPatch, book);
            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> delete(int id)
        {
            Boolean exits = await context.Books.AnyAsync(x => x.id == id);
            if (!exits)
            {
                return NotFound();
            }
            context.Remove(new Book { id = id });
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}