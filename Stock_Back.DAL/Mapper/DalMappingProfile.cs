using AutoMapper;
using Stock_Back.DAL.Models;

namespace Stock_Back.DAL.Mapper
{
    public class DalMappingProfile : Profile
    {
        public DalMappingProfile()
        {
            CreateMap<Client, Client>()
                .ForMember(d => d.Updated, opt => opt.MapFrom(_ => ResolveDate()))
                .MapDirty();

            CreateMap<MaterialType, MaterialType>().MapDirty();


            CreateMap<User, User>()
                .ForMember(d => d.Updated, opt => opt.MapFrom(_ => ResolveDate()))
                .MapDirty();
        }

        private static DateTime ResolveDate()
        {
            return DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        }
    }
}
