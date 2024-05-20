using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DemoTuan5.WarehouseLocations;
using System.Collections.Generic;

namespace DemoTuan5.WarehouseLocations
{
    [RemoteService(Name = "DemoTuan5")]
    [Area("demoTuan5")]
    [ControllerName("WarehouseLocation")]
    [Route("api/demo-tuan5/warehouse-locations")]
    public class WarehouseLocationController : WarehouseLocationControllerBase, IWarehouseLocationsAppService
    {
        public WarehouseLocationController(IWarehouseLocationsAppService warehouseLocationsAppService) : base(warehouseLocationsAppService)
        {
        }
        [HttpGet]
		[Route("NoPage")]
		public Task<List<WarehouseLocationDto>> GetListNoPagedAsync(GetWarehouseLocationsInput input)
        {
            return _warehouseLocationsAppService.GetListNoPagedAsync(input);

        }
    }
}