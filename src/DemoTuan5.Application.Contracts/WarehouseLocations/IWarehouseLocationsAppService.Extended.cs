using DemoTuan5.Warehouses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoTuan5.WarehouseLocations
{
    public partial interface IWarehouseLocationsAppService
    {

        //Write your custom code here...
        Task<List<WarehouseLocationDto>> GetListNoPagedAsync(GetWarehouseLocationsInput input);
 
    }
}