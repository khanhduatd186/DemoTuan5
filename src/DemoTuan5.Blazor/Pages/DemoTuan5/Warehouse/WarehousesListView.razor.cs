using Blazorise;
using Blazorise.DataGrid;
using Blazorise.Snackbar;
using DemoTuan5.Helper;
using DemoTuan5.Permissions;
using DemoTuan5.WarehouseLocations;
using DemoTuan5.Warehouses;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Messages;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Volo.Abp.BlazoriseUI.Components;


namespace DemoTuan5.Blazor.Pages.DemoTuan5.Warehouse
{
    public partial class WarehousesListView
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar { get; } = new PageToolbar();
        protected bool ShowAdvancedFilters { get; set; }
        private IReadOnlyList<WarehouseDto> WarehouseList { get; set; }
        private List<WarehouseLocationDto> warehouseLocations { get; set; } = new List<WarehouseLocationDto>();
        private readonly IUiMessageService _uiMessageService;
        private int PageSize { get; set; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private int MaxCount { get; } = 1000;
        private bool CanCreateWarehouse { get; set; }
        private bool CanEditWarehouse { get; set; }
        private bool CanDeleteWarehouse { get; set; }
        private WarehouseCreateDto NewWarehouse { get; set; }
        private Validations NewWarehouseValidations { get; set; } = new();
        private WarehouseUpdateDto EditingWarehouse { get; set; }
        private Validations EditingWarehouseValidations { get; set; } = new();
        private Guid EditingWarehouseId { get; set; }
        private Modal CreateWarehouseModal { get; set; } = new();
        private Modal EditWarehouseModal { get; set; } = new();
        private GetWarehousesInput Filter { get; set; }
        bool isDeleteButtonVisible = false;


        private List<WarehouseDto> selectedWarehouseDto = new List<WarehouseDto>();
        private DataGridEntityActionsColumn<WarehouseDto> EntityActionsColumn { get; set; } = new();
        protected string SelectedCreateTab = "warehouse-create-tab";
        protected string SelectedEditTab = "warehouse-edit-tab";
        // private WarehouseDto? SelectedWarehouse;
        SnackbarStack _uiNotification;


        //==================================Initialize Section===================================
        #region
        public WarehousesListView()
        {
            NewWarehouse = new WarehouseCreateDto();
            EditingWarehouse = new WarehouseUpdateDto();
            Filter = new GetWarehousesInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            WarehouseList = new List<WarehouseDto>();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();

        }
     
        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:Warehouses"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            var parmAction2 = new Dictionary<string, object>()
                {
                    {"DownloadAsExcelAsync", EventCallback.Factory.Create(this, DownloadAsExcelAsync) },
                    {"Delete", EventCallback.Factory.Create(this, Delete)},
                    {"New", EventCallback.Factory.Create(this, New) }
                };
            Toolbar.AddComponent<ListViewAction>(parmAction2);
            return ValueTask.CompletedTask;
        }
      
        private async Task SetPermissionsAsync()
        {
            CanCreateWarehouse = await AuthorizationService
                .IsGrantedAsync(DemoTuan5Permissions.Warehouses.Create);
            CanEditWarehouse = await AuthorizationService
                            .IsGrantedAsync(DemoTuan5Permissions.Warehouses.Edit);
            CanDeleteWarehouse = await AuthorizationService
                            .IsGrantedAsync(DemoTuan5Permissions.Warehouses.Delete);

        }
        #endregion
        //======================Load Data Source for ListView & Others=========================== 
        #region  
        private async Task GetWarehousesAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await WarehousesAppService.GetListAsync(Filter);
            WarehouseList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetWarehousesAsync();
            await InvokeAsync(StateHasChanged);
        }

      
        private async Task New()
        {
            NavigationManager.NavigateTo($"/DemoTuan5/Warehouses/{Guid.Empty}");
        }
        private async Task Delete()
        {
            if (selectedWarehouseDto.Count > 0)
            {
                var confirmed = await UiMessageService.Confirm(L["DeleteConfirmationMessage"]);
                if (confirmed)
                {
                    //Ki?m tra các d? li?u ?ã ch?n
                    foreach (var selectedserClass in selectedWarehouseDto)
                    {
                        //Ki?m tra các d? li?u con liên k?t có trong trang ?? xóa tr??c => xóa cha

                        ///Xóa d? li?u b?ng Attributes
                        var listSegment = await GetCollectionSegmentLookupById(selectedserClass.Id);
                        foreach (var attr2 in listSegment.Where(m => m.WarehouseId == selectedserClass.Id))
                        {
                            await WarehouseLocationsAppService.DeleteAsync(attr2.Id);
                        }

                        //Xóa d? li?u chính Lot Serial Classes ?ã ch?n
                        await WarehousesAppService.DeleteAsync(selectedserClass.Id);
                    }
                    await _uiNotification.PushAsync(
                            L["Notification:Delete"], SnackbarColor.Danger
                        );
                    await GetClassDataAsync();
                }
            }
        }
        private async Task<List<WarehouseLocationDto>> GetCollectionSegmentLookupById(Guid id)
        {
            var result = await WarehouseLocationsAppService.GetListNoPagedAsync(new GetWarehouseLocationsInput
            {
                WarehouseId = id,
                MaxResultCount = MaxCount
            });
            return warehouseLocations = ObjectMapper.Map<List<WarehouseLocationDto>, List<WarehouseLocationDto>>(result);
            /*return warehouseLocations =result;*/
        }
        
        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<WarehouseDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetWarehousesAsync();
            //await InvokeAsync(StateHasChanged);
        }

        protected virtual async Task OnCodeChangedAsync(string? code)
        {
            Filter.Code = code;
            await SearchAsync();
        }
        protected virtual async Task OnDescriptionChangedAsync(string? description)
        {
            Filter.Description = description;
            await SearchAsync();
        }
        protected virtual async Task OnActiveChangedAsync(bool? active)
        {
            Filter.Active = active;
            await SearchAsync();
        }
       
        private async Task GetClassDataAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await WarehousesAppService.GetListAsync(Filter);
            WarehouseList = result.Items;
            TotalCount = (int)result.TotalCount;
        }
        private async Task GetClassDataAsync(bool isRefresh)
        {
            if (isRefresh)
            {
                Filter = new GetWarehousesInput(); // Clear all filter values for refresh
            }
            else
            {
                Filter.MaxResultCount = PageSize;
                Filter.SkipCount = (CurrentPage - 1) * PageSize;
                Filter.Sorting = CurrentSorting;
            }

            var result = await WarehousesAppService.GetListAsync(Filter);
            WarehouseList = result.Items;
            TotalCount = (int)result.TotalCount;
        }
        #endregion
        //============================Controls triggers/events===================================
        #region
        async Task SelectedChangeRow(List<WarehouseDto> e)
        {
            await ShowButtonAction();
        }
        protected void GotoEditPage(WarehouseDto context)
        {
            NavigationManager.NavigateTo($"/DemoTuan5/Warehouses/{context.Id}");
        }
        private async Task DownloadAsExcelAsync()
        {
            var token = (await WarehousesAppService.GetDownloadTokenAsync()).Token;
            var remoteService = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("DemoTuan5") ??
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
            NavigationManager.NavigateTo($"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/demo-tuan5/warehouses/as-excel-file?DownloadToken={token}&FilterText={Filter.FilterText}&Code={Filter.Code}&Description={Filter.Description}&Active={Filter.Active}", forceLoad: true);
        }
        private async Task ChangePageSize(int value)
        {
            PageSize = value;
            await GetClassDataAsync();
        }
        public static string TruncateText(string text, int maxLength) // C?t chu?i
        {
            if (text.Length <= maxLength)
                return text;

            return text.Substring(0, maxLength) + "...";
        }
        private bool isSelected { get; set; } = false;
        private async Task ShowButtonAction()
        {
            if (selectedWarehouseDto.Count > 0)
            {
                ShowActionListView.UnreadCount = !isSelected;
                //await InvokeAsync(StateHasChanged);
            }
            else
            {
                ShowActionListView.UnreadCount = isSelected;
                //await InvokeAsync(StateHasChanged);
            }

        }
        private async Task OnFilterChanged<T>(string filterName, T filterValue)
        {
            typeof(GetWarehousesInput).GetProperty(filterName)?.SetValue(Filter, filterValue);
            await GetClassDataAsync(false);
        }
        #endregion

    }
}
