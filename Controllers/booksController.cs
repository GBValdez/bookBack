using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prueba.DTOS;
using prueba.entities;

namespace prueba.Controllers
{
    [ApiController]
    [Route("books")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class BooksController : controllerCommons<Book, bookCreationDto, bookDto, bookQueryDto, object>
    {
        public BooksController(AplicationDBContex context, IMapper mapper)
        : base(context, mapper)
        { }


        [HttpPut("{id}")]
        public override async Task<ActionResult> put(bookCreationDto book, int id, object query)
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
        [AllowAnonymous]
        public async Task<ActionResult<bookDto>> getById(int id)
        {
            IQueryable<Book> bookQuery = context.Books
                .Include(bookDb => bookDb.language);

            Book bookOnly = await bookQuery.FirstOrDefaultAsync(bookDb => bookDb.id == id && bookDb.deleteAt == null);
            if (bookOnly == null)
                return NotFound();

            List<Book_Category> bookCategories = await context.Book_Category.Where(
                bookDb => bookDb.category.deleteAt == null && bookDb.bookId == bookOnly.id
            ).Include(bookDb => bookDb.category).ToListAsync();
            List<Author_Book> bookAuthors = await context.Author_Book.Where(
                bookDb => bookDb.Author.deleteAt == null && bookDb.BookId == bookOnly.id
            ).Include(bookDb => bookDb.Author).ToListAsync();

            bookOnly.Author_Book = bookAuthors;
            bookOnly.Book_Category = bookCategories;
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
        protected override IQueryable<Book> modifyGet(IQueryable<Book> query, bookQueryDto queryParam)
        {

            int[] categoriesQuery = { };
            if (queryParam.categoriesId != null)
            {
                categoriesQuery = Array.ConvertAll(queryParam.categoriesId.Split(","), int.Parse);
            }
            IQueryable<Book> queryFinal = query
            .Include(dbBook => dbBook.language)
            .Include(dbBook => dbBook.Book_Category)
            .ThenInclude(dbBookCategory => dbBookCategory.category);
            if (queryParam.categoriesId != null)
                queryFinal = queryFinal.Where(dbBook => dbBook.Book_Category.Any(category => categoriesQuery.Contains(category.categoryId)));

            return queryFinal.Select(dbBook => new Book
            {
                id = dbBook.id,
                title = dbBook.title,
                dateCreation = dbBook.dateCreation,
                numPages = dbBook.numPages,
                language = dbBook.language,
                Book_Category = dbBook.Book_Category
            });
        }

        protected override async Task<errorMessageDto> validPost(bookCreationDto bookNew, object obj)
        {
            return await validListBook(bookNew);
        }
    }
}