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
    public class CreateModalModel : CreateModalModelBase
    {
        public CreateModalModel(IWarehousesAppService warehousesAppService)
            : base(warehousesAppService)
        {
        }
    }
}