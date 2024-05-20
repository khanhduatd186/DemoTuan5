using DemoTuan5.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DemoTuan5.Countries;

namespace DemoTuan5.Web.Pages.DemoTuan5.Countries
{
    public abstract class CreateModalModelBase : DemoTuan5PageModel
    {

        [BindProperty]
        public CountryCreateViewModel Country { get; set; }

        protected ICountriesAppService _countriesAppService;

        public CreateModalModelBase(ICountriesAppService countriesAppService)
        {
            _countriesAppService = countriesAppService;

            Country = new();
        }

        public virtual async Task OnGetAsync()
        {
            Country = new CountryCreateViewModel();

            await Task.CompletedTask;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {

            await _countriesAppService.CreateAsync(ObjectMapper.Map<CountryCreateViewModel, CountryCreateDto>(Country));
            return NoContent();
        }
    }

    public class CountryCreateViewModel : CountryCreateDto
    {
    }
}