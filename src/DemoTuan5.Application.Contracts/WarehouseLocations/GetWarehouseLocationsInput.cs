using Volo.Abp.Application.Dtos;
using System;

namespace DemoTuan5.WarehouseLocations
{
    public abstract class GetWarehouseLocationsInputBase : PagedAndSortedResultRequestDto
    {

        public string? FilterText { get; set; }

        public string? Code { get; set; }
        public string? Description { get; set; }
        public bool? Active { get; set; }
        public int? IdxMin { get; set; }
        public int? IdxMax { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? WarehouseId { get; set; }

        public GetWarehouseLocationsInputBase()
        {

        }
    }
}