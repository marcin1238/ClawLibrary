using AutoMapper;
using ClawLibrary.Core.Models.Authors;
using ClawLibrary.Core.Models.Books;
using ClawLibrary.Core.Models.Categories;
using ClawLibrary.Core.Models.Users;
using ClawLibrary.Services.Models.Users;

namespace ClawLibrary.Data.Mapping
{
    public class DataMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataMappingProfile"/> class.
        /// </summary>
        public DataMappingProfile()
        {
            CreateMapUser();
            CreateMapBook();
            CreateMapAuthor();
            CreateMapCategory();
        }

        private void CreateMapUser()
        {
            CreateMap<User, ClawLibrary.Data.Models.User>().ReverseMap()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Key, opt => opt.MapFrom(x => x.Key))
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Email))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.LastName))
                .ForMember(x => x.Password, opt => opt.MapFrom(x => x.PasswordHash))
                .ForMember(x => x.Salt, opt => opt.MapFrom(x => x.PasswordSalt))
                .ForMember(x => x.PasswordResetKey, opt => opt.MapFrom(x => x.PasswordResetKey))
                .ForMember(x => x.PasswordResetKeyCreatedDate, opt => opt.MapFrom(x => x.PasswordResetKeyCreatedDate))
                .ForMember(x => x.Status, opt => opt.MapFrom(x => x.Status))
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(x => x.CreatedDate))
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(x => x.ModifiedDate));

            CreateMap<RegisterUserRequest, ClawLibrary.Data.Models.User>()
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Email))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.LastName));
        }

        private void CreateMapBook()
        {
            CreateMap<Book, ClawLibrary.Data.Models.Book>().ReverseMap()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Key, opt => opt.MapFrom(x => x.Key))
                .ForMember(x => x.Title, opt => opt.MapFrom(x => x.Title))
                .ForMember(x => x.Isbn, opt => opt.MapFrom(x => x.Isbn))
                .ForMember(x => x.Publisher, opt => opt.MapFrom(x => x.Publisher))
                .ForMember(x => x.Language, opt => opt.MapFrom(x => x.Language))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(x => x.Paperback, opt => opt.MapFrom(x => x.Paperback))
                .ForMember(x => x.Language, opt => opt.MapFrom(x => x.Language))
                .ForMember(x => x.Author, opt => opt.MapFrom(x => x.Author))
                .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category))
                .ForMember(x => x.Status, opt => opt.MapFrom(x => x.Status))
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(x => x.CreatedDate))
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(x => x.ModifiedDate));
        }

        private void CreateMapAuthor()
        {
            CreateMap<Author, ClawLibrary.Data.Models.Author>().ReverseMap()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
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
            CreateMap<Category, ClawLibrary.Data.Models.Category>().ReverseMap()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Key, opt => opt.MapFrom(x => x.Key))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Status, opt => opt.MapFrom(x => x.Status))
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(x => x.CreatedDate))
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(x => x.ModifiedDate));
        }

    }
}
