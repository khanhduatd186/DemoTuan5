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
                id: Guid.Parse("b00d70c1-c607-4c84-9df6-7964558a9362"),
                code: "95510aefc6154c32a3c0a86da621e292",
                description: "8c17e3c147eb430abc7fecf1b69d059543d82a0c107841e081261d1e7e052dde8922d87c57064178b0f",
                active: true,
                idx: 260286894,
                countryId: null,
                warehouseId: null
            ));

            await _warehouseLocationRepository.InsertAsync(new WarehouseLocation
            (
                id: Guid.Parse("0ee727a9-cfe2-4b98-9f79-bf4db7d4d692"),
                code: "ac2bec2c30154c9b886847d9f1b00f",
                description: "d58a50491ecf4f5",
                active: true,
                idx: 2012601790,
                countryId: null,
                warehouseId: null
            ));

            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}