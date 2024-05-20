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
        bool active, string? code = null, string? description = null)
        {

            var warehouse = new Warehouse(
             GuidGenerator.Create(),
             active, code, description
             );

            return await _warehouseRepository.InsertAsync(warehouse);
        }

        public virtual async Task<Warehouse> UpdateAsync(
            Guid id,
            bool active, string? code = null, string? description = null, [CanBeNull] string? concurrencyStamp = null
        )
        {

            var warehouse = await _warehouseRepository.GetAsync(id);

            warehouse.Active = active;
            warehouse.Code = code;
            warehouse.Description = description;

            warehouse.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _warehouseRepository.UpdateAsync(warehouse);
        }

    }
}