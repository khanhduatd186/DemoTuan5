using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;
using Xunit;

namespace DemoTuan5.Warehouses
{
    public abstract class WarehousesAppServiceTests<TStartupModule> : DemoTuan5ApplicationTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly IWarehousesAppService _warehousesAppService;
        private readonly IRepository<Warehouse, Guid> _warehouseRepository;

        public WarehousesAppServiceTests()
        {
            _warehousesAppService = GetRequiredService<IWarehousesAppService>();
            _warehouseRepository = GetRequiredService<IRepository<Warehouse, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _warehousesAppService.GetListAsync(new GetWarehousesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("66826980-9bee-4dbe-8e27-24ab43b95ab2")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("9a0be75e-7be5-445b-90ae-78c260e4e76a")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _warehousesAppService.GetAsync(Guid.Parse("66826980-9bee-4dbe-8e27-24ab43b95ab2"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("66826980-9bee-4dbe-8e27-24ab43b95ab2"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new WarehouseCreateDto
            {
                Code = "8c027c39e4b24dbfbfe438fed5ad4f3b515c91ef30f34f0abc453a942a35e7a3fca0d1faf645",
                Description = "e860148",
                Active = true
            };

            // Act
            var serviceResult = await _warehousesAppService.CreateAsync(input);

            // Assert
            var result = await _warehouseRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("8c027c39e4b24dbfbfe438fed5ad4f3b515c91ef30f34f0abc453a942a35e7a3fca0d1faf645");
            result.Description.ShouldBe("e860148");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new WarehouseUpdateDto()
            {
                Code = "b657617eb63344e592e18ff9a0f6021b9a5bccd79680461693bfabdcb2b5d84d47bbc694e35c4350a21de18ab1e40379",
                Description = "6ea20f4e7f",
                Active = true
            };

            // Act
            var serviceResult = await _warehousesAppService.UpdateAsync(Guid.Parse("66826980-9bee-4dbe-8e27-24ab43b95ab2"), input);

            // Assert
            var result = await _warehouseRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("b657617eb63344e592e18ff9a0f6021b9a5bccd79680461693bfabdcb2b5d84d47bbc694e35c4350a21de18ab1e40379");
            result.Description.ShouldBe("6ea20f4e7f");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _warehousesAppService.DeleteAsync(Guid.Parse("66826980-9bee-4dbe-8e27-24ab43b95ab2"));

            // Assert
            var result = await _warehouseRepository.FindAsync(c => c.Id == Guid.Parse("66826980-9bee-4dbe-8e27-24ab43b95ab2"));

            result.ShouldBeNull();
        }
    }
}