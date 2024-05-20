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
            result.Items.Any(x => x.WarehouseLocation.Id == Guid.Parse("f75eb05c-18df-4d4d-bee9-83901bb5734f")).ShouldBe(true);
            result.Items.Any(x => x.WarehouseLocation.Id == Guid.Parse("67821507-6484-4393-92b3-fa6089403b6e")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _warehouseLocationsAppService.GetAsync(Guid.Parse("f75eb05c-18df-4d4d-bee9-83901bb5734f"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("f75eb05c-18df-4d4d-bee9-83901bb5734f"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new WarehouseLocationCreateDto
            {
                Code = "2211487037f14315a43667907c90839102cf76c2f7b34c0dbef76ac3094e751403c87e14c68440",
                Description = "b39ce44dfd724e7f817381b2c211e4b80669b5e7dfa24c68a921",
                Active = true,
                Idx = 611296079
            };

            // Act
            var serviceResult = await _warehouseLocationsAppService.CreateAsync(input);

            // Assert
            var result = await _warehouseLocationRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("2211487037f14315a43667907c90839102cf76c2f7b34c0dbef76ac3094e751403c87e14c68440");
            result.Description.ShouldBe("b39ce44dfd724e7f817381b2c211e4b80669b5e7dfa24c68a921");
            result.Active.ShouldBe(true);
            result.Idx.ShouldBe(611296079);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new WarehouseLocationUpdateDto()
            {
                Code = "68be5a4ab3234347a7e",
                Description = "8ad0c6e0ccf34f41b65daad731d2d6cd3ea6feb2ec394fbcaa5df94aebfdf9c76375394c99bb41e5b919ab017",
                Active = true,
                Idx = 790215488
            };

            // Act
            var serviceResult = await _warehouseLocationsAppService.UpdateAsync(Guid.Parse("f75eb05c-18df-4d4d-bee9-83901bb5734f"), input);

            // Assert
            var result = await _warehouseLocationRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("68be5a4ab3234347a7e");
            result.Description.ShouldBe("8ad0c6e0ccf34f41b65daad731d2d6cd3ea6feb2ec394fbcaa5df94aebfdf9c76375394c99bb41e5b919ab017");
            result.Active.ShouldBe(true);
            result.Idx.ShouldBe(790215488);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _warehouseLocationsAppService.DeleteAsync(Guid.Parse("f75eb05c-18df-4d4d-bee9-83901bb5734f"));

            // Assert
            var result = await _warehouseLocationRepository.FindAsync(c => c.Id == Guid.Parse("f75eb05c-18df-4d4d-bee9-83901bb5734f"));

            result.ShouldBeNull();
        }
    }
}