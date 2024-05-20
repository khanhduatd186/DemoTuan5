using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoTuan5.Warehouses
{
    public partial interface IWarehousesAppService
    {
		Task<List<WarehouseDto>> GetListNoPagedAsync(GetWarehousesInput input);
	}
}