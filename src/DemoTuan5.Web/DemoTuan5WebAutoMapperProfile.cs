using DemoTuan5.Web.Pages.DemoTuan5.WarehouseLocations;
using DemoTuan5.WarehouseLocations;
using DemoTuan5.Web.Pages.DemoTuan5.Warehouses;
using DemoTuan5.Warehouses;
using DemoTuan5.Web.Pages.DemoTuan5.Countries;
using Volo.Abp.AutoMapper;
using DemoTuan5.Countries;
using AutoMapper;

namespace DemoTuan5.Web;

public class DemoTuan5WebAutoMapperProfile : Profile
{
    public DemoTuan5WebAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<CountryDto, CountryUpdateViewModel>();
        CreateMap<CountryUpdateViewModel, CountryUpdateDto>();
        CreateMap<CountryCreateViewModel, CountryCreateDto>();

        CreateMap<WarehouseDto, WarehouseUpdateViewModel>();
        CreateMap<WarehouseUpdateViewModel, WarehouseUpdateDto>();
        CreateMap<WarehouseCreateViewModel, WarehouseCreateDto>();

        CreateMap<WarehouseLocationDto, WarehouseLocationUpdateViewModel>();
        CreateMap<WarehouseLocationUpdateViewModel, WarehouseLocationUpdateDto>();
        CreateMap<WarehouseLocationCreateViewModel, WarehouseLocationCreateDto>();
    }
}