using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DemoTuan5.Countries;
using DemoTuan5.EntityFrameworkCore;
using Xunit;

namespace DemoTuan5.EntityFrameworkCore.Domains.Countries
{
    public class CountryRepositoryTests : DemoTuan5EntityFrameworkCoreTestBase
    {
        private readonly ICountryRepository _countryRepository;

        public CountryRepositoryTests()
        {
            _countryRepository = GetRequiredService<ICountryRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _countryRepository.GetListAsync(
                    code: "236ada89304144d8aa0506df53d42ec9fab5f7",
                    description: "cd59ab0a6cf54dc1b9fad59f7f3077004cfbbfa70a1f4cc8852c8cdf9083a8c944d885b14c004174af8fbfbe9c9660c8"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("b20350fe-9665-438c-b7d0-2586a75501f9"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _countryRepository.GetCountAsync(
                    code: "e759e1eb9d4b4ad2878e5b81f41e313c4d",
                    description: "14c9d54156524e8491d09b82559e049d3a09b80ffce340cc9dec269970a67"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}