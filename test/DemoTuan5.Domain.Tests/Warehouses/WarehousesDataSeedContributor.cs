using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DemoTuan5.Warehouses;

namespace DemoTuan5.Warehouses
{
    public class WarehousesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public WarehousesDataSeedContributor(IWarehouseRepository warehouseRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _warehouseRepository = warehouseRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _warehouseRepository.InsertAsync(new Warehouse
            (
                id: Guid.Parse("6f9c1019-11a3-4f27-bf81-47255ee25734"),
                code: "1266c0687f344ed0b2bd4319fd93c677ffc00",
                description: "24d6e52130254258bca5f655321e97d97109e444057d4f78b8b97587b720879e80eeb0",
                active: true
            ));

            await _warehouseRepository.InsertAsync(new Warehouse
            (
                id: Guid.Parse("155dc45e-607b-4427-a467-a41bf5b9abd1"),
                code: "811317b8275142c4ba1f4a38b32503898001fe3a10974b8f88e90b80ae4ce913e63b6c45ea2c4dea8ea9",
                description: "e8adbf07ac8142c7aa5acb7c61d0944e6db6345c409846cf9f0ab0307fefb8cb3a37829d2a6d4bd5b4d13",
                active: true
            ));

            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}