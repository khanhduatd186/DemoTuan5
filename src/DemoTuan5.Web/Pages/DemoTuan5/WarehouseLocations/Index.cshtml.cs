using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using DemoTuan5.WarehouseLocations;
using DemoTuan5.Shared;

namespace DemoTuan5.Web.Pages.DemoTuan5.WarehouseLocations
{
    public abstract class IndexModelBase : AbpPageModel
    {
        public string? CodeFilter { get; set; }
        public string? DescriptionFilter { get; set; }
        [SelectItems(nameof(ActiveBoolFilterItems))]
        public string ActiveFilter { get; set; }

        public List<SelectListItem> ActiveBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        public int? IdxFilterMin { get; set; }

        public int? IdxFilterMax { get; set; }
        [SelectItems(nameof(CountryLookupList))]
        public Guid? CountryIdFilter { get; set; }
        public List<SelectListItem> CountryLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(WarehouseLookupList))]
        public Guid? WarehouseIdFilter { get; set; }
        public List<SelectListItem> WarehouseLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        protected IWarehouseLocationsAppService _warehouseLocationsAppService;

        public IndexModelBase(IWarehouseLocationsAppService warehouseLocationsAppService)
        {
            _warehouseLocationsAppService = warehouseLocationsAppService;
        }

        public virtual async Task OnGetAsync()
        {
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
    }
}