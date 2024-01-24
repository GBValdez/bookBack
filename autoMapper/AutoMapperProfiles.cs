using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using prueba.DTOS;
using prueba.entities;

namespace prueba.autoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<authorCreationDto, Author>();
            CreateMap<Author, authorDto>()
                .ForMember(author => author.books, options => options.MapFrom(mapBooksByAuthor));

            CreateMap<bookCreationDto, Book>()
                .ForMember(book => book.Author_Book, options => options.MapFrom(MapAuthorsBook));
            CreateMap<Book, bookDto>()
                .ForMember(book => book.authors, options => options.MapFrom(mapAuthorsDtoBook));

            CreateMap<CommentsCreationDto, Comments>();
            CreateMap<Comments, CommentsDto>();

            CreateMap<bookPatchDto, Book>().ReverseMap();
            CreateMap<Comments, CommentsDto>();

        }

        public List<bookDto> mapBooksByAuthor(Author author, authorDto authorDto)
        {
            List<bookDto> results = new List<bookDto>();
            if (author.Author_Book == null)
                return results;
            foreach (Author_Book item in author.Author_Book)
            {
                results.Add(new bookDto()
                {
                    id = item.BookId,
                    title = item.Book.title
                });
            }
            return results;
        }

        public List<authorDto> mapAuthorsDtoBook(Book book, bookDto bookDtoNew)
        {
            List<authorDto> results = new List<authorDto>();
            if (book.Author_Book == null)
                return results;
            foreach (Author_Book item in book.Author_Book)
            {
                if (item.Author != null)
                    results.Add(new authorDto()
                    {
                        id = item.AuthorId,
                        name = item.Author.name
                    });
            }
            return results;
        }

        private List<Author_Book> MapAuthorsBook(bookCreationDto bookCreation, Book book)
        {
            var results = new List<Author_Book>();
            // if (bookCreation.authorIds == null)
            //     return results;
            foreach (int item in bookCreation.authorIds)
            {
                results.Add(new Author_Book { AuthorId = item });
            }
            return results;
        }

    }
}