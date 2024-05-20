using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DemoTuan5.Warehouses;
using System.Collections.Generic;

namespace DemoTuan5.Warehouses
{
    [RemoteService(Name = "DemoTuan5")]
    [Area("demoTuan5")]
    [ControllerName("Warehouse")]
    [Route("api/demo-tuan5/warehouses")]
    public class WarehouseController : WarehouseControllerBase, IWarehousesAppService
    {
        public WarehouseController(IWarehousesAppService warehousesAppService) : base(warehousesAppService)
        {
        }
        [HttpGet]
		[Route("NoPage")]
		public Task<List<WarehouseDto>> GetListNoPagedAsync(GetWarehousesInput input)
		{
			return _warehousesAppService.GetListNoPagedAsync(input);
		}
	}
}