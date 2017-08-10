using AutoMapper;

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
        }

        private void CreateMapUser()
        {
            CreateMap<ClawLibrary.Core.Models.User, ClawLibrary.Data.Models.User>().ReverseMap()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Key, opt => opt.MapFrom(x => x.Key))
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Email))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.LastName))
                .ForMember(x => x.Password, opt => opt.MapFrom(x => x.PasswordHash))
                .ForMember(x => x.Salt, opt => opt.MapFrom(x => x.PasswordSalt))
                .ForMember(x => x.PasswordResetKey, opt => opt.MapFrom(x => x.PasswordResetKey))
                .ForMember(x => x.PasswordResetKeyCreatedDate, opt => opt.MapFrom(x => x.PasswordResetKeyCreatedDate))
                .ForMember(x => x.Status, opt => opt.MapFrom(x => x.Status))
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(x => x.CreatedDate))
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(x => x.ModifiedDate));

            CreateMap<ClawLibrary.Core.Models.Auth.RegisterUserRequest, ClawLibrary.Data.Models.User>()
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Email))
                .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(x => x.PhoneNumber))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.LastName));
        }

    }
}
