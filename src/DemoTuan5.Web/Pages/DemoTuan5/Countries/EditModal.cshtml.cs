using DemoTuan5.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using DemoTuan5.Countries;

namespace DemoTuan5.Web.Pages.DemoTuan5.Countries
{
    public abstract class EditModalModelBase : DemoTuan5PageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CountryUpdateViewModel Country { get; set; }

        protected ICountriesAppService _countriesAppService;

        public EditModalModelBase(ICountriesAppService countriesAppService)
        {
            _countriesAppService = countriesAppService;

            Country = new();
        }

        public virtual async Task OnGetAsync()
        {
            var country = await _countriesAppService.GetAsync(Id);
            Country = ObjectMapper.Map<CountryDto, CountryUpdateViewModel>(country);

        }

        public virtual async Task<NoContentResult> OnPostAsync()
        {

            await _countriesAppService.UpdateAsync(Id, ObjectMapper.Map<CountryUpdateViewModel, CountryUpdateDto>(Country));
            return NoContent();
        }
    }

    public class CountryUpdateViewModel : CountryUpdateDto
    {
    }
}