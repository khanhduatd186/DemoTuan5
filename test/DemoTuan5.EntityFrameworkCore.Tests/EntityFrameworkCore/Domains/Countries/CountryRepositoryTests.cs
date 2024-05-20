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
                    code: "5fb483c908c84bc3b9659ff22ac8f82d4f6ed45b017c4f7bb242bc1dedf",
                    description: "581583da5f4c45cfa682b1dd26"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("c49697d9-d401-4a2e-8281-94a3d843bd29"));
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
                    code: "53069810276e4033a3a94fd1d873eabb98aa75",
                    description: "af7b7e7752324a5094179e3130fa5eb16"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}