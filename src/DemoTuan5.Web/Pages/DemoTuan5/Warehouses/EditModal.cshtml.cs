using DemoTuan5.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using DemoTuan5.Warehouses;

namespace DemoTuan5.Web.Pages.DemoTuan5.Warehouses
{
    public abstract class EditModalModelBase : DemoTuan5PageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public WarehouseUpdateViewModel Warehouse { get; set; }

        protected IWarehousesAppService _warehousesAppService;

        public EditModalModelBase(IWarehousesAppService warehousesAppService)
        {
            _warehousesAppService = warehousesAppService;

            Warehouse = new();
        }

        public virtual async Task OnGetAsync()
        {
            var warehouse = await _warehousesAppService.GetAsync(Id);
            Warehouse = ObjectMapper.Map<WarehouseDto, WarehouseUpdateViewModel>(warehouse);

        }

        public virtual async Task<NoContentResult> OnPostAsync()
        {

            await _warehousesAppService.UpdateAsync(Id, ObjectMapper.Map<WarehouseUpdateViewModel, WarehouseUpdateDto>(Warehouse));
            return NoContent();
        }
    }

    public class WarehouseUpdateViewModel : WarehouseUpdateDto
    {
    }
}