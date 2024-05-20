using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DemoTuan5.WarehouseLocations
{
    public abstract class WarehouseLocationManagerBase : DomainService
    {
        protected IWarehouseLocationRepository _warehouseLocationRepository;

        public WarehouseLocationManagerBase(IWarehouseLocationRepository warehouseLocationRepository)
        {
            _warehouseLocationRepository = warehouseLocationRepository;
        }

        public virtual async Task<WarehouseLocation> CreateAsync(
        Guid? countryId, Guid? warehouseId, bool active, int idx, string? code = null, string? description = null)
        {

            var warehouseLocation = new WarehouseLocation(
             GuidGenerator.Create(),
             countryId, warehouseId, active, idx, code, description
             );

            return await _warehouseLocationRepository.InsertAsync(warehouseLocation);
        }

        public virtual async Task<WarehouseLocation> UpdateAsync(
            Guid id,
            Guid? countryId, Guid? warehouseId, bool active, int idx, string? code = null, string? description = null, [CanBeNull] string? concurrencyStamp = null
        )
        {

            var warehouseLocation = await _warehouseLocationRepository.GetAsync(id);

            warehouseLocation.CountryId = countryId;
            warehouseLocation.WarehouseId = warehouseId;
            warehouseLocation.Active = active;
            warehouseLocation.Idx = idx;
            warehouseLocation.Code = code;
            warehouseLocation.Description = description;

            warehouseLocation.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _warehouseLocationRepository.UpdateAsync(warehouseLocation);
        }

    }
}