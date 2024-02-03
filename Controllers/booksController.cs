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
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class BooksController : controllerCommons<Book, bookCreationDto, bookDto>
    {
        public BooksController(AplicationDBContex context, IMapper mapper)
        : base(context, mapper)
        { }

        [HttpPut("{id}")]
        public async Task<IActionResult> put(int id, bookCreationDto book)
        {
            errorMessageDto error = await this.validListBook(book);
            if (error != null)
                return BadRequest(error);
            Book bookUpd = await context.Books
                .Include(bookDB => bookDB.Author_Book)
                .Include(bookBb => bookBb.Book_Category)
                .FirstOrDefaultAsync(bookDb => bookDb.id == id);
            if (bookUpd == null)
                return NotFound();
            bookUpd = mapper.Map(book, bookUpd);

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id:int}", Name = "getBookById"),]
        public async Task<ActionResult<bookDto>> getById(int id, [FromQuery] Boolean? all = false)
        {
            var bookQuery = context.Books
                .Include(bookDB => bookDB.Author_Book)
                .ThenInclude(bookAuthorDB => bookAuthorDB.Author)
                .Include(bookDb => bookDb.language)
                .Include(bookDb => bookDb.Book_Category)
                .ThenInclude(bookCategoryDb => bookCategoryDb.category);

            if (all.Value)
                bookQuery.Include(bookDb => bookDb.comments);
            Book bookOnly = await bookQuery.FirstOrDefaultAsync(bookDb => bookDb.id == id && bookDb.deleteAt == null);
            if (bookOnly == null)
                return NotFound();
            // bookOnly.Author_Book = bookOnly.Author_Book.OrderBy(x => x.order).ToList();
            return mapper.Map<bookDto>(bookOnly);
        }
        private async Task<Boolean> validListId<TValid>(List<int> ids)
        where TValid : CommonsModel
        {
            List<int> dbIds = await context.Set<TValid>()
                .Where(db => ids
                    .Contains(db.id))
                .Select(db => db.id).ToListAsync();
            return dbIds.Count == ids.Count;
        }
        private async Task<errorMessageDto> validListBook(bookCreationDto book)
        {
            if (!await validListId<Author>(book.authorIds))
                return new errorMessageDto("No existe alguno de los autores");
            if (!await validListId<Category>(book.categoriesId))
                return new errorMessageDto("No existe alguno de los autores");
            return null;
        }
        protected override IQueryable<Book> modifyGet(IQueryable<Book> query)
        {
            return query
            .Include(dbBook => dbBook.language)
            .Include(dbBook => dbBook.Book_Category)
            .ThenInclude(dbBookCategory => dbBookCategory.category)
            .Select(dbBook => new Book
            {
                id = dbBook.id,
                title = dbBook.title,
                dateCreation = dbBook.dateCreation,
                numPages = dbBook.numPages,
                language = dbBook.language,
                Book_Category = dbBook.Book_Category
            });
        }

        protected override async Task<errorMessageDto> validPost(bookCreationDto bookNew)
        {
            return await validListBook(bookNew);
        }
    }
}