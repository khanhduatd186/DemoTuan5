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
    public class IndexModel : IndexModelBase
    {
        public IndexModel(IWarehouseLocationsAppService warehouseLocationsAppService)
            : base(warehouseLocationsAppService)
        {
        }
    }
}