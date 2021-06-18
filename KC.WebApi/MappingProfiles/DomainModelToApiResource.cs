using AutoMapper;
using KC.Base.Models;
using KC.WebApi.Models.User;

namespace KC.WebApi.MappingProfiles
{
    public class DomainModelToApiResource : Profile
    {
        public DomainModelToApiResource()
        {
            CreateMap<User, CreateUserResponse>();
        }
    }
}
