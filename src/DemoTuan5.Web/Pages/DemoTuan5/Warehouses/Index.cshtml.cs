using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using DemoTuan5.Warehouses;
using DemoTuan5.Shared;

namespace DemoTuan5.Web.Pages.DemoTuan5.Warehouses
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

        protected IWarehousesAppService _warehousesAppService;

        public IndexModelBase(IWarehousesAppService warehousesAppService)
        {
            _warehousesAppService = warehousesAppService;
        }

        public virtual async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}