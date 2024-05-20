using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DemoTuan5.Warehouses
{
    public abstract class WarehouseManagerBase : DomainService
    {
        protected IWarehouseRepository _warehouseRepository;

        public WarehouseManagerBase(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public virtual async Task<Warehouse> CreateAsync(
        string code, bool active, string? description = null)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));

            var warehouse = new Warehouse(
             GuidGenerator.Create(),
             code, active, description
             );

            return await _warehouseRepository.InsertAsync(warehouse);
        }

        public virtual async Task<Warehouse> UpdateAsync(
            Guid id,
            string code, bool active, string? description = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));

            var warehouse = await _warehouseRepository.GetAsync(id);

            warehouse.Code = code;
            warehouse.Active = active;
            warehouse.Description = description;

            warehouse.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _warehouseRepository.UpdateAsync(warehouse);
        }

    }
}