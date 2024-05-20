using DemoTuan5.Warehouses;
using DemoTuan5.Countries;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DemoTuan5.WarehouseLocations;

namespace DemoTuan5.WarehouseLocations
{
    public class WarehouseLocationsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IWarehouseLocationRepository _warehouseLocationRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CountriesDataSeedContributor _countriesDataSeedContributor;

        private readonly WarehousesDataSeedContributor _warehousesDataSeedContributor;

        public WarehouseLocationsDataSeedContributor(IWarehouseLocationRepository warehouseLocationRepository, IUnitOfWorkManager unitOfWorkManager, CountriesDataSeedContributor countriesDataSeedContributor, WarehousesDataSeedContributor warehousesDataSeedContributor)
        {
            _warehouseLocationRepository = warehouseLocationRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _countriesDataSeedContributor = countriesDataSeedContributor; _warehousesDataSeedContributor = warehousesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _countriesDataSeedContributor.SeedAsync(context);
            await _warehousesDataSeedContributor.SeedAsync(context);

            await _warehouseLocationRepository.InsertAsync(new WarehouseLocation
            (
                id: Guid.Parse("f75eb05c-18df-4d4d-bee9-83901bb5734f"),
                code: "269069b2582d41bd9b1af27a865",
                description: "f4026ed3a7a74f3f8926720dc745eba32ca5481a6",
                active: true,
                idx: 536784564,
                countryId: null,
                warehouseId: null
            ));

            await _warehouseLocationRepository.InsertAsync(new WarehouseLocation
            (
                id: Guid.Parse("67821507-6484-4393-92b3-fa6089403b6e"),
                code: "4e8d1fd9d38a418ea9fa1fcd6b96bd07e3b3feff6f394f8d98efb2f2bbf8ddd952b19443c",
                description: "fc9b0cbb60ef44628948dd6de88f3fbb818238c1fb554a10888ccc1038c852fcce1cebe04229496b",
                active: true,
                idx: 2125467952,
                countryId: null,
                warehouseId: null
            ));

            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}