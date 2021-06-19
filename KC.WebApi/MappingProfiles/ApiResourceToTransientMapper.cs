using AutoMapper;
using KC.Base.TransientModels;
using KC.WebApi.Models.Product;
using KC.WebApi.Models.User;

namespace KC.WebApi.MappingProfiles
{
    public class ApiResourceToTransientMapper : Profile
    {
        public ApiResourceToTransientMapper()
        {
            CreateMap<CreateUserRequest, TransientUser>();
            CreateMap<CreateOrUpdateProductRequest, TransientProduct>();
        }
    }
}
