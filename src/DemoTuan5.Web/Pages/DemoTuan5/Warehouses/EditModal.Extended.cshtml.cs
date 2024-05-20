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
    public class EditModalModel : EditModalModelBase
    {
        public EditModalModel(IWarehousesAppService warehousesAppService)
            : base(warehousesAppService)
        {
        }
    }
}