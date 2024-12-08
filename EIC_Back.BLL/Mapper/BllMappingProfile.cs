using AutoMapper;
using EIC_Back.BLL.Models.FinancialCategoryModelDTO;
using EIC_Back.BLL.Models.ClientModelDTO;
using EIC_Back.BLL.Models.PersonnelModelDTO;
using EIC_Back.BLL.Models.FinancialSubCategoryModelDTO;
using EIC_Back.BLL.Models.UserModelDTO;
using EIC_Back.DAL.Models;
using EIC_Back.BLL.Models.FinancialMovementsModelDTO;

namespace EIC_Back.BLL.Mapper
{
    public class BllMappingProfile : Profile
    {
        public BllMappingProfile()
        {

            CreateMap<Client, ClientDTO>()
                .ReverseMap();

            CreateMap<Client, FullClientDTO>()
                .ReverseMap();

            CreateMap<Client, ClientEditDTO>()
                .ReverseMap()
                .ForMember(d => d.Created, opt => opt.Ignore())
                .ForMember(d => d.Updated, opt => opt.MapFrom(_ => ResolveDate()));

            CreateMap<Client, ClientInsertDTO>()
                .ReverseMap()
                .ForMember(d => d.Created, opt => opt.MapFrom(_ => ResolveDate()))
                .ForMember(d => d.Updated, opt => opt.MapFrom(_ => ResolveDate()));



            CreateMap<Personnel, PersonnelDTO>()
                .ReverseMap();

            CreateMap<Personnel, PersonnelEditDTO>()
                .ReverseMap()
                .ForMember(d => d.Created, opt => opt.Ignore())
                .ForMember(d => d.Updated, opt => opt.MapFrom(_ => ResolveDate()));

            CreateMap<Personnel, PersonnelInsertDTO>()
                .ReverseMap()
                .ForMember(d => d.Created, opt => opt.MapFrom(_ => ResolveDate()))
                .ForMember(d => d.Updated, opt => opt.MapFrom(_ => ResolveDate()));

            CreateMap<FinancialCategory, FinancialCategoryFullDTO>();

            CreateMap<User, UserDTO>()
                .ReverseMap();

            CreateMap<User, UserEditDTO>()
                .ReverseMap()
                .ForMember(d => d.Created, opt => opt.Ignore())
                .ForMember(d => d.Updated, opt => opt.MapFrom(_ => ResolveDate()));

            CreateMap<User, UserInsertDTO>()
                .ReverseMap()
                .ForMember(d => d.Created, opt => opt.MapFrom(_ => ResolveDate()))
                .ForMember(d => d.Updated, opt => opt.MapFrom(_ => ResolveDate()));

            CreateMap<FinancialCategory, FinancialCategoryDTO>()
           .ReverseMap();

            CreateMap<FinancialCategory, FinancialCategoryInsertDTO>()
                .ReverseMap()
                .ForMember(d => d.Created, opt => opt.MapFrom(_ => ResolveDate()))
                .ForMember(d => d.Updated, opt => opt.MapFrom(_ => ResolveDate()));

            CreateMap<FinancialCategory, FinancialCategoryEditDTO>()
                .ReverseMap()
                .ForMember(d => d.Created, opt => opt.Ignore())
                .ForMember(d => d.Updated, opt => opt.MapFrom(_ => ResolveDate()));

            CreateMap<FinancialSubCategory, FinancialSubCategoryFullDTO>()
                   .ReverseMap();
            CreateMap<FinancialSubCategory, FinancialSubCategoryDTO>()
                .ForMember(d => d.FinancialCategoryId, opt => opt.MapFrom(src => src.FinancialCategoryId))
                .ReverseMap();

            CreateMap<FinancialSubCategory, FinancialSubCategoryInsertDTO>()
                .ReverseMap()
                .ForMember(d => d.Created, opt => opt.MapFrom(_ => ResolveDate()))
                .ForMember(d => d.Updated, opt => opt.MapFrom(_ => ResolveDate()));

            CreateMap<FinancialSubCategory, FinancialSubCategoryEditDTO>()
                .ReverseMap()
                .ForMember(d => d.Created, opt => opt.Ignore())
                .ForMember(d => d.Updated, opt => opt.MapFrom(_ => ResolveDate()));

            CreateMap<FinancialMovements, FinancialMovementsDTO>()
            .ReverseMap();

            CreateMap<FinancialMovements, FinancialMovementsInsertDTO>()
                .ReverseMap()
                .ForMember(d => d.Created, opt => opt.MapFrom(_ => ResolveDate()))
                .ForMember(d => d.Updated, opt => opt.MapFrom(_ => ResolveDate()));

            CreateMap<FinancialMovements, FinancialMovementsEditDTO>()
                .ReverseMap()
                .ForMember(d => d.Created, opt => opt.Ignore())
                .ForMember(d => d.Updated, opt => opt.MapFrom(_ => ResolveDate()));
        }
        private DateTime ResolveDate()
        {
            return DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        }
        private DateTime ResolveDate(DateTime date)
        {
            return DateTime.SpecifyKind(date, DateTimeKind.Utc);
        }
        private object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

    }
}
