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
                    code: "269069b2582d41bd9b1af27a865",
                    description: "f4026ed3a7a74f3f8926720dc745eba32ca5481a6",
                    active: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("f75eb05c-18df-4d4d-bee9-83901bb5734f"));
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
                    code: "4e8d1fd9d38a418ea9fa1fcd6b96bd07e3b3feff6f394f8d98efb2f2bbf8ddd952b19443c",
                    description: "fc9b0cbb60ef44628948dd6de88f3fbb818238c1fb554a10888ccc1038c852fcce1cebe04229496b",
                    active: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}