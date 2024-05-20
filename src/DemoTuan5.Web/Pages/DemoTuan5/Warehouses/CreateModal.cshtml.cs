using DemoTuan5.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DemoTuan5.Warehouses;

namespace DemoTuan5.Web.Pages.DemoTuan5.Warehouses
{
    public abstract class CreateModalModelBase : DemoTuan5PageModel
    {

        [BindProperty]
        public WarehouseCreateViewModel Warehouse { get; set; }

        protected IWarehousesAppService _warehousesAppService;

        public CreateModalModelBase(IWarehousesAppService warehousesAppService)
        {
            _warehousesAppService = warehousesAppService;

            Warehouse = new();
        }

        public virtual async Task OnGetAsync()
        {
            Warehouse = new WarehouseCreateViewModel();

            await Task.CompletedTask;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {

            await _warehousesAppService.CreateAsync(ObjectMapper.Map<WarehouseCreateViewModel, WarehouseCreateDto>(Warehouse));
            return NoContent();
        }
    }

    public class WarehouseCreateViewModel : WarehouseCreateDto
    {
    }
}