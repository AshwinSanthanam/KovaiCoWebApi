using AutoMapper;
using KC.Base.Models;
using KC.Base.TransientModels;

namespace KC.DataAccess.MappingProfiles
{
    class TransientToDomainProfle : Profile
    {
        public TransientToDomainProfle()
        {
            CreateMap<TransientProduct, Product>();
            CreateMap<TransientCart, Cart>();
        }
    }
}
