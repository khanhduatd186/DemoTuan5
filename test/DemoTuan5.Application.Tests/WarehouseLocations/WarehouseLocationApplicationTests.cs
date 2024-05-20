using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;
using Xunit;

namespace DemoTuan5.WarehouseLocations
{
    public abstract class WarehouseLocationsAppServiceTests<TStartupModule> : DemoTuan5ApplicationTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly IWarehouseLocationsAppService _warehouseLocationsAppService;
        private readonly IRepository<WarehouseLocation, Guid> _warehouseLocationRepository;

        public WarehouseLocationsAppServiceTests()
        {
            _warehouseLocationsAppService = GetRequiredService<IWarehouseLocationsAppService>();
            _warehouseLocationRepository = GetRequiredService<IRepository<WarehouseLocation, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _warehouseLocationsAppService.GetListAsync(new GetWarehouseLocationsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.WarehouseLocation.Id == Guid.Parse("b00d70c1-c607-4c84-9df6-7964558a9362")).ShouldBe(true);
            result.Items.Any(x => x.WarehouseLocation.Id == Guid.Parse("0ee727a9-cfe2-4b98-9f79-bf4db7d4d692")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _warehouseLocationsAppService.GetAsync(Guid.Parse("b00d70c1-c607-4c84-9df6-7964558a9362"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("b00d70c1-c607-4c84-9df6-7964558a9362"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new WarehouseLocationCreateDto
            {
                Code = "3bc34f81338840a294e2313d9d6b3de00b1ba7ba2ff9429aa6bbc92736d",
                Description = "42de0419ed3c4363b1f2d178226b317bee264962956144df83707d91779c3eff3b97188ac4f64bbe81803",
                Active = true,
                Idx = 32219421
            };

            // Act
            var serviceResult = await _warehouseLocationsAppService.CreateAsync(input);

            // Assert
            var result = await _warehouseLocationRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("3bc34f81338840a294e2313d9d6b3de00b1ba7ba2ff9429aa6bbc92736d");
            result.Description.ShouldBe("42de0419ed3c4363b1f2d178226b317bee264962956144df83707d91779c3eff3b97188ac4f64bbe81803");
            result.Active.ShouldBe(true);
            result.Idx.ShouldBe(32219421);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new WarehouseLocationUpdateDto()
            {
                Code = "b7d000092cc94223a8a02957bfb2b0c5f079cf8635194fcda4922401638c60a87bf5",
                Description = "cd08ded0fb694b93a27a4d5800c89ba45a879fcb949c475c82aa19f9bcf92573131",
                Active = true,
                Idx = 388972370
            };

            // Act
            var serviceResult = await _warehouseLocationsAppService.UpdateAsync(Guid.Parse("b00d70c1-c607-4c84-9df6-7964558a9362"), input);

            // Assert
            var result = await _warehouseLocationRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("b7d000092cc94223a8a02957bfb2b0c5f079cf8635194fcda4922401638c60a87bf5");
            result.Description.ShouldBe("cd08ded0fb694b93a27a4d5800c89ba45a879fcb949c475c82aa19f9bcf92573131");
            result.Active.ShouldBe(true);
            result.Idx.ShouldBe(388972370);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _warehouseLocationsAppService.DeleteAsync(Guid.Parse("b00d70c1-c607-4c84-9df6-7964558a9362"));

            // Assert
            var result = await _warehouseLocationRepository.FindAsync(c => c.Id == Guid.Parse("b00d70c1-c607-4c84-9df6-7964558a9362"));

            result.ShouldBeNull();
        }
    }
}