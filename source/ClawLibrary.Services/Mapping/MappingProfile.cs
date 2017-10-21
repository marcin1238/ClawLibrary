using AutoMapper;
using ClawLibrary.Core.Models;
using ClawLibrary.Core.Models.Authors;
using ClawLibrary.Core.Models.Books;
using ClawLibrary.Core.Models.Categories;
using ClawLibrary.Core.Models.Users;
using ClawLibrary.Services.Models.Books;
using ClawLibrary.Services.Models.Users;

namespace ClawLibrary.Services.Mapping
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        public MappingProfile()
        {
            CreateMapUser();
            CreateMapBook();
            CreateMapAuthor();
            CreateMapCategory();
        }

        private void CreateMapUser()
        {
            CreateMap<User, UserResponse>().ReverseMap();
            CreateMap<ListResponse<User>, ListResponse<UserResponse>>().ReverseMap();
            CreateMap<User, UserRequest>().ReverseMap();
        }

        private void CreateMapBook()
        {
            CreateMap<ClawLibrary.Services.Models.Books.BookRequest, ClawLibrary.Core.Models.Books.Book>().ReverseMap()
                .ForMember(x => x.Title, opt => opt.MapFrom(x => x.Title))
                .ForMember(x => x.Isbn, opt => opt.MapFrom(x => x.Isbn))
                .ForMember(x => x.Publisher, opt => opt.MapFrom(x => x.Publisher))
                .ForMember(x => x.Language, opt => opt.MapFrom(x => x.Language))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(x => x.Quantity, opt => opt.MapFrom(x => x.Quantity))
                .ForMember(x => x.Paperback, opt => opt.MapFrom(x => x.Paperback))
                .ForMember(x => x.Language, opt => opt.MapFrom(x => x.Language));

            CreateMap<ClawLibrary.Services.Models.Books.BookUpdateRequest, ClawLibrary.Core.Models.Books.Book>().ReverseMap()
                .ForMember(x => x.Title, opt => opt.MapFrom(x => x.Title))
                .ForMember(x => x.Isbn, opt => opt.MapFrom(x => x.Isbn))
                .ForMember(x => x.Publisher, opt => opt.MapFrom(x => x.Publisher))
                .ForMember(x => x.Language, opt => opt.MapFrom(x => x.Language))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(x => x.Quantity, opt => opt.MapFrom(x => x.Quantity))
                .ForMember(x => x.Paperback, opt => opt.MapFrom(x => x.Paperback))
                .ForMember(x => x.Status, opt => opt.MapFrom(x => x.Status))
                .ForMember(x => x.Language, opt => opt.MapFrom(x => x.Language));

            CreateMap<ClawLibrary.Services.Models.Books.BookResponse, ClawLibrary.Core.Models.Books.Book>().ReverseMap()
                .ForMember(x => x.Key, opt => opt.MapFrom(x => x.Key))
                .ForMember(x => x.Title, opt => opt.MapFrom(x => x.Title))
                .ForMember(x => x.Isbn, opt => opt.MapFrom(x => x.Isbn))
                .ForMember(x => x.Publisher, opt => opt.MapFrom(x => x.Publisher))
                .ForMember(x => x.Language, opt => opt.MapFrom(x => x.Language))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(x => x.Quantity, opt => opt.MapFrom(x => x.Quantity))
                .ForMember(x => x.Paperback, opt => opt.MapFrom(x => x.Paperback))
                .ForMember(x => x.Language, opt => opt.MapFrom(x => x.Language))
                .ForMember(x => x.Status, opt => opt.MapFrom(x => x.Status))
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(x => x.CreatedDate))
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(x => x.ModifiedDate));

            CreateMap<ListResponse<ClawLibrary.Core.Models.Books.Book>, ListResponse<ClawLibrary.Services.Models.Books.BookResponse>>().ReverseMap();
        }

        private void CreateMapAuthor()
        {
            CreateMap<ClawLibrary.Services.Models.Authors.AuthorRequest, ClawLibrary.Core.Models.Authors.Author>().ReverseMap()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.LastName))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description));

            CreateMap<ClawLibrary.Services.Models.Authors.AuthorResponse, ClawLibrary.Core.Models.Authors.Author>().ReverseMap()
                .ForMember(x => x.Key, opt => opt.MapFrom(x => x.Key))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.LastName))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(x => x.Status, opt => opt.MapFrom(x => x.Status))
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(x => x.CreatedDate))
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(x => x.ModifiedDate));
        }

        private void CreateMapCategory()
        {
            CreateMap<ClawLibrary.Services.Models.Categories.CategoryRequest, ClawLibrary.Core.Models.Categories.Category>().ReverseMap()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name));

            CreateMap<ClawLibrary.Services.Models.Categories.CategoryResponse, ClawLibrary.Core.Models.Categories.Category>().ReverseMap()
                .ForMember(x => x.Key, opt => opt.MapFrom(x => x.Key))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Status, opt => opt.MapFrom(x => x.Status))
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(x => x.CreatedDate))
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(x => x.ModifiedDate));
        }

    }
}
