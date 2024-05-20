using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DemoTuan5.Countries;

namespace DemoTuan5.Countries
{
    [RemoteService(Name = "DemoTuan5")]
    [Area("demoTuan5")]
    [ControllerName("Country")]
    [Route("api/demo-tuan5/countries")]
    public class CountryController : CountryControllerBase, ICountriesAppService
    {
        public CountryController(ICountriesAppService countriesAppService) : base(countriesAppService)
        {
        }
    }
}