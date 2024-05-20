using Volo.Abp.Application.Dtos;
using System;

namespace DemoTuan5.Countries
{
    public abstract class GetCountriesInputBase : PagedAndSortedResultRequestDto
    {

        public string? FilterText { get; set; }

        public string? Code { get; set; }
        public string? Description { get; set; }

        public GetCountriesInputBase()
        {

        }
    }
}