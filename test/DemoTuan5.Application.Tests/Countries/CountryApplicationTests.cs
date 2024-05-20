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
            result.Items.Any(x => x.Id == Guid.Parse("b20350fe-9665-438c-b7d0-2586a75501f9")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("19f07083-ad9a-4586-934f-fbbd02c5b6fc")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _countriesAppService.GetAsync(Guid.Parse("b20350fe-9665-438c-b7d0-2586a75501f9"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("b20350fe-9665-438c-b7d0-2586a75501f9"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CountryCreateDto
            {
                Code = "ef5f67",
                Description = "5aec3810042f44e1998d9a8080f93f4051e5700b01714e6c924397c866209d9a"
            };

            // Act
            var serviceResult = await _countriesAppService.CreateAsync(input);

            // Assert
            var result = await _countryRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("ef5f67");
            result.Description.ShouldBe("5aec3810042f44e1998d9a8080f93f4051e5700b01714e6c924397c866209d9a");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CountryUpdateDto()
            {
                Code = "295bba4d5a5f4c7a86f54ae315",
                Description = "b902d791f60348198887a0d5e90f3d4c1bc7c7a796b94e40a77ee6c5fed256203b0086385aec45"
            };

            // Act
            var serviceResult = await _countriesAppService.UpdateAsync(Guid.Parse("b20350fe-9665-438c-b7d0-2586a75501f9"), input);

            // Assert
            var result = await _countryRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("295bba4d5a5f4c7a86f54ae315");
            result.Description.ShouldBe("b902d791f60348198887a0d5e90f3d4c1bc7c7a796b94e40a77ee6c5fed256203b0086385aec45");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _countriesAppService.DeleteAsync(Guid.Parse("b20350fe-9665-438c-b7d0-2586a75501f9"));

            // Assert
            var result = await _countryRepository.FindAsync(c => c.Id == Guid.Parse("b20350fe-9665-438c-b7d0-2586a75501f9"));

            result.ShouldBeNull();
        }
    }
}