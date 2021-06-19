using AutoMapper;

namespace KC.DataAccess.MappingProfiles
{
    public static class AutoMapperConfiguration
    {
        public static IMapper Mapper { get; }

        static AutoMapperConfiguration()
        {
            var mapperConfiguration = new MapperConfiguration(configuration => 
            {
                configuration.AddProfile<TransientToDomainProfle>();
            });

            Mapper = mapperConfiguration.CreateMapper();
        }
    }
}
