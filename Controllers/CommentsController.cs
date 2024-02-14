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
    public class CommentsController : controllerCommons<Comments, CommentsCreationDto, CommentsDto, commentsParams, commentsParams, int>
    {
        private readonly UserManager<userEntity> userManager;
        public CommentsController(AplicationDBContex context, IMapper mapper, UserManager<userEntity> userManager) : base(context, mapper)
        {
            this.userManager = userManager;
        }

        protected override async Task<IQueryable<Comments>> modifyGet(IQueryable<Comments> query, commentsParams queryParams)
        {
            return query.Include(commentDb => commentDb.user);
        }

        protected override void modifyGetResult(List<Comments> list)
        {
            string idUser = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            list.ForEach(commentDb =>
                {
                    Boolean validUser = false;
                    if (idUser != null)
                    {
                        validUser = commentDb.user.Id == idUser;
                    }
                    commentDb.Id = validUser ? commentDb.Id : -1;
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
            Boolean exits = await context.Books.AnyAsync(bookDB => bookDB.Id == commentsParams.BookId);
            if (!exits)
                return new errorMessageDto("No existe el libro");
            return null;
        }

        protected override async Task<errorMessageDto> validPut(CommentsCreationDto dtoNew, Comments entity, commentsParams queryParams)
        {
            string idUser = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (entity.userId != idUser)
                return new errorMessageDto("El comentario no coincide con su creador");
            return null;
        }

    }
}