using AutoMapper;
using Stock_Back.BLL.Models.FinancialCategoryModelDTO;
using Stock_Back.BLL.Models.ClientModelDTO;
using Stock_Back.BLL.Models.MaterialModelDTO;
using Stock_Back.BLL.Models.FinancialSubCategoryModelDTO;
using Stock_Back.BLL.Models.UserModelDTO;
using Stock_Back.BLL.Models.FinancialMovementsModelDTO;
using Stock_Back.DAL.Models;
namespace Stock_Back.BLL.Mapper
{
    public class BllMappingProfile : Profile
    {
        public BllMappingProfile()
        {

            CreateMap<Client, ClientDTO>()
                .ReverseMap();

            CreateMap<Client, ClientEditDTO>()
                .ReverseMap()
                .ForMember(d => d.Created, opt => opt.Ignore())
                .ForMember(d => d.Updated, opt => opt.MapFrom(_ => ResolveDate()));

            CreateMap<Client, ClientInsertDTO>()
                .ReverseMap()
                .ForMember(d => d.Created, opt => opt.MapFrom(_ => ResolveDate()))
                .ForMember(d => d.Updated, opt => opt.MapFrom(_ => ResolveDate()));


            CreateMap<MaterialType, MaterialDTO>()
                .ReverseMap();

            CreateMap<MaterialType, MaterialEditDTO>()
                .ReverseMap()
                .ForMember(d => d.Created, opt => opt.Ignore())
                .ForMember(d => d.Updated, opt => opt.MapFrom(_ => ResolveDate())); ;

            CreateMap<MaterialType, MaterialInsertDTO>()
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
    }
}
