using DemoTuan5.WarehouseLocations;
using DemoTuan5.Warehouses;
using Volo.Abp.AutoMapper;
using DemoTuan5.Countries;
using AutoMapper;

namespace DemoTuan5.Blazor;

public class DemoTuan5BlazorAutoMapperProfile : Profile
{
    public DemoTuan5BlazorAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<CountryDto, CountryUpdateDto>();

        CreateMap<WarehouseDto, WarehouseUpdateDto>();

        CreateMap<WarehouseUpdateDto, WarehouseCreateDto>();
        /* CreateMap<WarehouseLocationUpdateDto, WarehouseLocationWithNavigationPropertiesDto>()
           .ForMember(dest => dest.WarehouseLocation, opt => opt.MapFrom(src => src))
           .ForMember(dest => dest.Country, opt => opt.Ignore())
           .ForMember(dest => dest.Warehouse, opt => opt.Ignore());*/

        CreateMap<WarehouseLocationDto, WarehouseLocationUpdateDto>();
        CreateMap<WarehouseLocationDto, WarehouseLocationCreateDto>();
        CreateMap<WarehouseLocationUpdateDto, WarehouseLocationCreateDto>();
    }
}