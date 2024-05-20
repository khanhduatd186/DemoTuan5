using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DemoTuan5.WarehouseLocations;
using DemoTuan5.EntityFrameworkCore;
using Xunit;

namespace DemoTuan5.EntityFrameworkCore.Domains.WarehouseLocations
{
    public class WarehouseLocationRepositoryTests : DemoTuan5EntityFrameworkCoreTestBase
    {
        private readonly IWarehouseLocationRepository _warehouseLocationRepository;

        public WarehouseLocationRepositoryTests()
        {
            _warehouseLocationRepository = GetRequiredService<IWarehouseLocationRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _warehouseLocationRepository.GetListAsync(
                    code: "95510aefc6154c32a3c0a86da621e292",
                    description: "8c17e3c147eb430abc7fecf1b69d059543d82a0c107841e081261d1e7e052dde8922d87c57064178b0f",
                    active: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("b00d70c1-c607-4c84-9df6-7964558a9362"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _warehouseLocationRepository.GetCountAsync(
                    code: "ac2bec2c30154c9b886847d9f1b00f",
                    description: "d58a50491ecf4f5",
                    active: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}