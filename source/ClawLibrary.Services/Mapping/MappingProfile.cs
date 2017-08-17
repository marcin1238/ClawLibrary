using AutoMapper;
using ClawLibrary.Core.Models;
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
        }

        private void CreateMapUser()
        {
            CreateMap<User, UserResponse>().ReverseMap();
            CreateMap<User, UserRequest>().ReverseMap();
        }

    }
}
