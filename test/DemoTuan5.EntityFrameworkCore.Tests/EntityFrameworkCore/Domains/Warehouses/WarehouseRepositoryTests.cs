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
                    code: "1266c0687f344ed0b2bd4319fd93c677ffc00",
                    description: "24d6e52130254258bca5f655321e97d97109e444057d4f78b8b97587b720879e80eeb0",
                    active: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("6f9c1019-11a3-4f27-bf81-47255ee25734"));
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
                    code: "811317b8275142c4ba1f4a38b32503898001fe3a10974b8f88e90b80ae4ce913e63b6c45ea2c4dea8ea9",
                    description: "e8adbf07ac8142c7aa5acb7c61d0944e6db6345c409846cf9f0ab0307fefb8cb3a37829d2a6d4bd5b4d13",
                    active: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}