using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DemoTuan5.Countries;

namespace DemoTuan5.Countries
{
    public class CountriesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICountryRepository _countryRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public CountriesDataSeedContributor(ICountryRepository countryRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _countryRepository = countryRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _countryRepository.InsertAsync(new Country
            (
                id: Guid.Parse("b20350fe-9665-438c-b7d0-2586a75501f9"),
                code: "236ada89304144d8aa0506df53d42ec9fab5f7",
                description: "cd59ab0a6cf54dc1b9fad59f7f3077004cfbbfa70a1f4cc8852c8cdf9083a8c944d885b14c004174af8fbfbe9c9660c8"
            ));

            await _countryRepository.InsertAsync(new Country
            (
                id: Guid.Parse("19f07083-ad9a-4586-934f-fbbd02c5b6fc"),
                code: "e759e1eb9d4b4ad2878e5b81f41e313c4d",
                description: "14c9d54156524e8491d09b82559e049d3a09b80ffce340cc9dec269970a67"
            ));

            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}