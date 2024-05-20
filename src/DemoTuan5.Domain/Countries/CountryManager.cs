using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DemoTuan5.Countries
{
    public abstract class CountryManagerBase : DomainService
    {
        protected ICountryRepository _countryRepository;

        public CountryManagerBase(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public virtual async Task<Country> CreateAsync(
        string? code = null, string? description = null)
        {

            var country = new Country(
             GuidGenerator.Create(),
             code, description
             );

            return await _countryRepository.InsertAsync(country);
        }

        public virtual async Task<Country> UpdateAsync(
            Guid id,
            string? code = null, string? description = null, [CanBeNull] string? concurrencyStamp = null
        )
        {

            var country = await _countryRepository.GetAsync(id);

            country.Code = code;
            country.Description = description;

            country.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _countryRepository.UpdateAsync(country);
        }

    }
}