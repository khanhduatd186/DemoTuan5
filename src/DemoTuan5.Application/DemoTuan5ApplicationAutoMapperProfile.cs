using DemoTuan5.WarehouseLocations;
using DemoTuan5.Warehouses;
using System;
using DemoTuan5.Shared;
using Volo.Abp.AutoMapper;
using DemoTuan5.Countries;
using AutoMapper;

namespace DemoTuan5;

public class DemoTuan5ApplicationAutoMapperProfile : Profile
{
    public DemoTuan5ApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Country, CountryDto>();
        CreateMap<Country, CountryExcelDto>();

        CreateMap<Warehouse, WarehouseDto>();
        CreateMap<Warehouse, WarehouseExcelDto>();

        CreateMap<WarehouseLocation, WarehouseLocationDto>();
        CreateMap<WarehouseLocation, WarehouseLocationExcelDto>();
        CreateMap<WarehouseLocationWithNavigationProperties, WarehouseLocationWithNavigationPropertiesDto>();
        CreateMap<Country, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));
        CreateMap<Warehouse, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        /* CreateMap<WarehouseLocationUpdateDto, WarehouseLocationWithNavigationPropertiesDto>()
           .ForMember(dest => dest.WarehouseLocation, opt => opt.MapFrom(src => src))
           .ForMember(dest => dest.Country, opt => opt.Ignore())
           .ForMember(dest => dest.Warehouse, opt => opt.Ignore());*/
    }
}