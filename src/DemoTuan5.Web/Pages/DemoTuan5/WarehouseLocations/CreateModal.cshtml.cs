using DemoTuan5.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DemoTuan5.WarehouseLocations;

namespace DemoTuan5.Web.Pages.DemoTuan5.WarehouseLocations
{
    public abstract class CreateModalModelBase : DemoTuan5PageModel
    {

        [BindProperty]
        public WarehouseLocationCreateViewModel WarehouseLocation { get; set; }

        public List<SelectListItem> CountryLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };
        public List<SelectListItem> WarehouseLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        protected IWarehouseLocationsAppService _warehouseLocationsAppService;

        public CreateModalModelBase(IWarehouseLocationsAppService warehouseLocationsAppService)
        {
            _warehouseLocationsAppService = warehouseLocationsAppService;

            WarehouseLocation = new();
        }

        public virtual async Task OnGetAsync()
        {
            WarehouseLocation = new WarehouseLocationCreateViewModel();
            CountryLookupList.AddRange((
                                    await _warehouseLocationsAppService.GetCountryLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            WarehouseLookupList.AddRange((
                                    await _warehouseLocationsAppService.GetWarehouseLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

            await Task.CompletedTask;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {

            await _warehouseLocationsAppService.CreateAsync(ObjectMapper.Map<WarehouseLocationCreateViewModel, WarehouseLocationCreateDto>(WarehouseLocation));
            return NoContent();
        }
    }

    public class WarehouseLocationCreateViewModel : WarehouseLocationCreateDto
    {
    }
}