using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DemoTuan5.Warehouses;
using DemoTuan5.EntityFrameworkCore;
using Xunit;

namespace DemoTuan5.EntityFrameworkCore.Domains.Warehouses
{
    public class WarehouseRepositoryTests : DemoTuan5EntityFrameworkCoreTestBase
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public WarehouseRepositoryTests()
        {
            _warehouseRepository = GetRequiredService<IWarehouseRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _warehouseRepository.GetListAsync(
                    code: "2dc7ff4383754d28a6ee02eb926f79764df2eca3db88",
                    description: "9c8abf58c17540f0af9ae9870c524b2baeaa7578f684482ab26014925fb78f59c78c0e63360d4d27a5b2eef960c6",
                    active: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("66826980-9bee-4dbe-8e27-24ab43b95ab2"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _warehouseRepository.GetCountAsync(
                    code: "7d1aac4f6feb4f79bc90737f6f71ccdd2be30b80d50c4a45ae1ff151b3a",
                    description: "34aa7ecc602740d5bef936a38297",
                    active: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}