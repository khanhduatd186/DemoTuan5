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
        Guid? countryId, Guid? warehouseId, string code, bool active, int idx, string? description = null)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));

            var warehouseLocation = new WarehouseLocation(
             GuidGenerator.Create(),
             countryId, warehouseId, code, active, idx, description
             );

            return await _warehouseLocationRepository.InsertAsync(warehouseLocation);
        }

        public virtual async Task<WarehouseLocation> UpdateAsync(
            Guid id,
            Guid? countryId, Guid? warehouseId, string code, bool active, int idx, string? description = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));

            var warehouseLocation = await _warehouseLocationRepository.GetAsync(id);

            warehouseLocation.CountryId = countryId;
            warehouseLocation.WarehouseId = warehouseId;
            warehouseLocation.Code = code;
            warehouseLocation.Active = active;
            warehouseLocation.Idx = idx;
            warehouseLocation.Description = description;

            warehouseLocation.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _warehouseLocationRepository.UpdateAsync(warehouseLocation);
        }

    }
}