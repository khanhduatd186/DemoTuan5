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
            result.Items.Any(x => x.Id == Guid.Parse("6f9c1019-11a3-4f27-bf81-47255ee25734")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("155dc45e-607b-4427-a467-a41bf5b9abd1")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _warehousesAppService.GetAsync(Guid.Parse("6f9c1019-11a3-4f27-bf81-47255ee25734"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("6f9c1019-11a3-4f27-bf81-47255ee25734"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new WarehouseCreateDto
            {
                Code = "c142019f33434f13a3ac39a27a9adff0efec9e424bb247aeb94c54afd3987811c1a7970f7bad457",
                Description = "8b8a183d8b8340f7a0e9506956d4dc15b1953ee430bf44bd80ec6",
                Active = true
            };

            // Act
            var serviceResult = await _warehousesAppService.CreateAsync(input);

            // Assert
            var result = await _warehouseRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("c142019f33434f13a3ac39a27a9adff0efec9e424bb247aeb94c54afd3987811c1a7970f7bad457");
            result.Description.ShouldBe("8b8a183d8b8340f7a0e9506956d4dc15b1953ee430bf44bd80ec6");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new WarehouseUpdateDto()
            {
                Code = "8df5724538724adfacf9fdaac994b13909cb6c317e964cb28",
                Description = "1b5f29de5a884b45adccef5d5a0cf1716f381b07e869437e9270f826b",
                Active = true
            };

            // Act
            var serviceResult = await _warehousesAppService.UpdateAsync(Guid.Parse("6f9c1019-11a3-4f27-bf81-47255ee25734"), input);

            // Assert
            var result = await _warehouseRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("8df5724538724adfacf9fdaac994b13909cb6c317e964cb28");
            result.Description.ShouldBe("1b5f29de5a884b45adccef5d5a0cf1716f381b07e869437e9270f826b");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _warehousesAppService.DeleteAsync(Guid.Parse("6f9c1019-11a3-4f27-bf81-47255ee25734"));

            // Assert
            var result = await _warehouseRepository.FindAsync(c => c.Id == Guid.Parse("6f9c1019-11a3-4f27-bf81-47255ee25734"));

            result.ShouldBeNull();
        }
    }
}