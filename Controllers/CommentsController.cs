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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prueba.DTOS;
using prueba.entities;

namespace prueba.Controllers
{
    [ApiController]
    [Route("books/{BookId}/comments")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class CommentsController : controllerCommons<Comments, CommentsCreationDto, CommentsDto, commentsParams, commentsParams>
    {
        private readonly UserManager<IdentityUser> userManager;
        public CommentsController(AplicationDBContex context, IMapper mapper, UserManager<IdentityUser> userManager) : base(context, mapper)
        {
            this.userManager = userManager;
        }

        protected override IQueryable<Comments> modifyGet(IQueryable<Comments> query, commentsParams queryParams)
        {
            return query.Include(commentDb => commentDb.user);
        }

        protected override void modifyGetResult(List<Comments> list)
        {
            string idUser = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            list.ForEach(commentDb =>
                {
                    Boolean validUser = commentDb.user.Id == idUser;
                    commentDb.id = validUser ? commentDb.id : -1;
                    commentDb.user.Email = null;
                });

        }
        protected override async Task modifyPost(Comments entity, commentsParams queryParams)
        {
            string idUser = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            entity.userId = idUser;
            entity.BookId = queryParams.BookId;
            return;
        }

        protected override async Task<errorMessageDto> validPost(CommentsCreationDto dtoNew, commentsParams commentsParams)
        {
            Boolean exits = await context.Books.AnyAsync(bookDB => bookDB.id == commentsParams.BookId);
            if (!exits)
                return new errorMessageDto("No existe el libro");
            return null;
        }
        // [HttpPost]
        // public async Task<ActionResult<CommentsDto>> post(int idBook, CommentsCreationDto comment)
        // {
        //     string idUser = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        //     Book exits = await context.Books.FirstOrDefaultAsync(bookDB => bookDB.id == idBook);
        //     if (exits == null)
        //         return NotFound();

        //     Comments newComment = mapper.Map<Comments>(comment);
        //     newComment.BookId = idBook;
        //     newComment.userId = idUser;
        //     context.Add(newComment);
        //     await context.SaveChangesAsync();
        //     CommentsDto results = mapper.Map<CommentsDto>(newComment);
        //     return results;
        // }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> put(CommentsCreationDto comment, int id)
        {
            Boolean exits = await context.comments.AnyAsync(x => x.id == id);
            if (!exits)
            {
                return NotFound();
            }
            Comments updComment = mapper.Map<Comments>(comment);
            updComment.id = id;
            context.Update(updComment);
            await context.SaveChangesAsync();
            return NoContent();
        }

        // [HttpGet]
        // public async Task<ActionResult<List<CommentsDto>>> get(int idBook)
        // {
        //     Book exits = await context.Books.FirstOrDefaultAsync(bookDB => bookDB.id == idBook);
        //     if (exits == null)
        //         return NotFound();
        //     List<Comments> commentsList = await context.comments.Where(commentDB => commentDB.BookId == idBook).ToListAsync();
        //     return mapper.Map<List<CommentsDto>>(commentsList);
        // }

        // [HttpGet("/comment/{id}", Name = "getCommentById")]
        // public async Task<ActionResult<CommentsDto>> getById(int idBook, int id)
        // {
        //     Comments comment = await context.comments.FirstOrDefaultAsync(comment => comment.id == id);
        //     if (comment == null)
        //         return NotFound();
        //     return mapper.Map<CommentsDto>(comment);
        // }

    }
}