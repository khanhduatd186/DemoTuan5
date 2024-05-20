using DemoTuan5.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using DemoTuan5.WarehouseLocations;

namespace DemoTuan5.Web.Pages.DemoTuan5.WarehouseLocations
{
    public abstract class EditModalModelBase : DemoTuan5PageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public WarehouseLocationUpdateViewModel WarehouseLocation { get; set; }

        public List<SelectListItem> CountryLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };
        public List<SelectListItem> WarehouseLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        protected IWarehouseLocationsAppService _warehouseLocationsAppService;

        public EditModalModelBase(IWarehouseLocationsAppService warehouseLocationsAppService)
        {
            _warehouseLocationsAppService = warehouseLocationsAppService;

            WarehouseLocation = new();
        }

        public virtual async Task OnGetAsync()
        {
            var warehouseLocationWithNavigationPropertiesDto = await _warehouseLocationsAppService.GetWithNavigationPropertiesAsync(Id);
            WarehouseLocation = ObjectMapper.Map<WarehouseLocationDto, WarehouseLocationUpdateViewModel>(warehouseLocationWithNavigationPropertiesDto.WarehouseLocation);

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

        }

        public virtual async Task<NoContentResult> OnPostAsync()
        {

            await _warehouseLocationsAppService.UpdateAsync(Id, ObjectMapper.Map<WarehouseLocationUpdateViewModel, WarehouseLocationUpdateDto>(WarehouseLocation));
            return NoContent();
        }
    }

    public class WarehouseLocationUpdateViewModel : WarehouseLocationUpdateDto
    {
    }
}