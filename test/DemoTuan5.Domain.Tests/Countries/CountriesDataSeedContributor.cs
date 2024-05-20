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
                id: Guid.Parse("c49697d9-d401-4a2e-8281-94a3d843bd29"),
                code: "5fb483c908c84bc3b9659ff22ac8f82d4f6ed45b017c4f7bb242bc1dedf",
                description: "581583da5f4c45cfa682b1dd26"
            ));

            await _countryRepository.InsertAsync(new Country
            (
                id: Guid.Parse("dcad7d8b-254f-4324-bb11-f6783c4705e4"),
                code: "53069810276e4033a3a94fd1d873eabb98aa75",
                description: "af7b7e7752324a5094179e3130fa5eb16"
            ));

            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}