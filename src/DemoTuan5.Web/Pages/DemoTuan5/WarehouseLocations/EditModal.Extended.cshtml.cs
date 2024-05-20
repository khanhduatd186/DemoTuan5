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
    public class EditModalModel : EditModalModelBase
    {
        public EditModalModel(IWarehouseLocationsAppService warehouseLocationsAppService)
            : base(warehouseLocationsAppService)
        {
        }
    }
}