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
                id: Guid.Parse("66826980-9bee-4dbe-8e27-24ab43b95ab2"),
                code: "2dc7ff4383754d28a6ee02eb926f79764df2eca3db88",
                description: "9c8abf58c17540f0af9ae9870c524b2baeaa7578f684482ab26014925fb78f59c78c0e63360d4d27a5b2eef960c6",
                active: true
            ));

            await _warehouseRepository.InsertAsync(new Warehouse
            (
                id: Guid.Parse("9a0be75e-7be5-445b-90ae-78c260e4e76a"),
                code: "7d1aac4f6feb4f79bc90737f6f71ccdd2be30b80d50c4a45ae1ff151b3a",
                description: "34aa7ecc602740d5bef936a38297",
                active: true
            ));

            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}