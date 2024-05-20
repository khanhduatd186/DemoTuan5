using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using DemoTuan5.Countries;
using DemoTuan5.Shared;

namespace DemoTuan5.Web.Pages.DemoTuan5.Countries
{
    public abstract class IndexModelBase : AbpPageModel
    {
        public string? CodeFilter { get; set; }
        public string? DescriptionFilter { get; set; }

        protected ICountriesAppService _countriesAppService;

        public IndexModelBase(ICountriesAppService countriesAppService)
        {
            _countriesAppService = countriesAppService;
        }

        public virtual async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}