using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;
using Xunit;

namespace DemoTuan5.Countries
{
    public abstract class CountriesAppServiceTests<TStartupModule> : DemoTuan5ApplicationTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly ICountriesAppService _countriesAppService;
        private readonly IRepository<Country, Guid> _countryRepository;

        public CountriesAppServiceTests()
        {
            _countriesAppService = GetRequiredService<ICountriesAppService>();
            _countryRepository = GetRequiredService<IRepository<Country, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _countriesAppService.GetListAsync(new GetCountriesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("c49697d9-d401-4a2e-8281-94a3d843bd29")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("dcad7d8b-254f-4324-bb11-f6783c4705e4")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _countriesAppService.GetAsync(Guid.Parse("c49697d9-d401-4a2e-8281-94a3d843bd29"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("c49697d9-d401-4a2e-8281-94a3d843bd29"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CountryCreateDto
            {
                Code = "cb2c783a682144c89c5ff80530220f619e8a34ba582e40d3a0da112ed97f0e31ffb2",
                Description = "f5d4976c9f884680a685d6c9a8b9256d2fd118da609645ebba290dbbaf65e108"
            };

            // Act
            var serviceResult = await _countriesAppService.CreateAsync(input);

            // Assert
            var result = await _countryRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("cb2c783a682144c89c5ff80530220f619e8a34ba582e40d3a0da112ed97f0e31ffb2");
            result.Description.ShouldBe("f5d4976c9f884680a685d6c9a8b9256d2fd118da609645ebba290dbbaf65e108");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CountryUpdateDto()
            {
                Code = "3c147c6cd30c4505",
                Description = "a7c74f019447410aa26f964a52ec5916ec67bd327bb04595987155841ba04b4d93dfc97a53814bbc98c"
            };

            // Act
            var serviceResult = await _countriesAppService.UpdateAsync(Guid.Parse("c49697d9-d401-4a2e-8281-94a3d843bd29"), input);

            // Assert
            var result = await _countryRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("3c147c6cd30c4505");
            result.Description.ShouldBe("a7c74f019447410aa26f964a52ec5916ec67bd327bb04595987155841ba04b4d93dfc97a53814bbc98c");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _countriesAppService.DeleteAsync(Guid.Parse("c49697d9-d401-4a2e-8281-94a3d843bd29"));

            // Assert
            var result = await _countryRepository.FindAsync(c => c.Id == Guid.Parse("c49697d9-d401-4a2e-8281-94a3d843bd29"));

            result.ShouldBeNull();
        }
    }
}