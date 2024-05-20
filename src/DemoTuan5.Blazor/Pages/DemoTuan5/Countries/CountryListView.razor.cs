using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using Volo.Abp.BlazoriseUI.Components;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using DemoTuan5.Countries;
using DemoTuan5.Permissions;
using DemoTuan5.Shared;


namespace DemoTuan5.Blazor.Pages.DemoTuan5.Countries
{
    public partial class CountryListView
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar { get; } = new PageToolbar();
        protected bool ShowAdvancedFilters { get; set; }
        private IReadOnlyList<CountryDto> CountryList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private bool CanCreateCountry { get; set; }
        private bool CanEditCountry { get; set; }
        private bool CanDeleteCountry { get; set; }
        private CountryCreateDto NewCountry { get; set; }
        private Validations NewCountryValidations { get; set; } = new();
        private CountryUpdateDto EditingCountry { get; set; }
        private Validations EditingCountryValidations { get; set; } = new();
        private Guid EditingCountryId { get; set; }
        private Modal CreateCountryModal { get; set; } = new();
        private Modal EditCountryModal { get; set; } = new();
        private GetCountriesInput Filter { get; set; }
        private DataGridEntityActionsColumn<CountryDto> EntityActionsColumn { get; set; } = new();
        protected string SelectedCreateTab = "country-create-tab";
        protected string SelectedEditTab = "country-edit-tab";
        private CountryDto? SelectedCountry;




        public CountryListView()
        {
            NewCountry = new CountryCreateDto();
            EditingCountry = new CountryUpdateDto();
            Filter = new GetCountriesInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            CountryList = new List<CountryDto>();


        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();

        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:Countries"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["ExportToExcel"], async () => { await DownloadAsExcelAsync(); }, IconName.Download);

            Toolbar.AddButton(L["NewCountry"], async () =>
            {
                await OpenCreateCountryModalAsync();
            }, IconName.Add, requiredPolicyName: DemoTuan5Permissions.Countries.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateCountry = await AuthorizationService
                .IsGrantedAsync(DemoTuan5Permissions.Countries.Create);
            CanEditCountry = await AuthorizationService
                            .IsGrantedAsync(DemoTuan5Permissions.Countries.Edit);
            CanDeleteCountry = await AuthorizationService
                            .IsGrantedAsync(DemoTuan5Permissions.Countries.Delete);


        }

        private async Task GetCountriesAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await CountriesAppService.GetListAsync(Filter);
            CountryList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetCountriesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task DownloadAsExcelAsync()
        {
            var token = (await CountriesAppService.GetDownloadTokenAsync()).Token;
            var remoteService = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("DemoTuan5") ??
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
            NavigationManager.NavigateTo($"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/demo-tuan5/countries/as-excel-file?DownloadToken={token}&FilterText={Filter.FilterText}&Code={Filter.Code}&Description={Filter.Description}", forceLoad: true);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<CountryDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetCountriesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateCountryModalAsync()
        {
            NewCountry = new CountryCreateDto
            {


            };
            await NewCountryValidations.ClearAll();
            await CreateCountryModal.Show();
        }

        private async Task CloseCreateCountryModalAsync()
        {
            NewCountry = new CountryCreateDto
            {


            };
            await CreateCountryModal.Hide();
        }

        private async Task OpenEditCountryModalAsync(CountryDto input)
        {
            var country = await CountriesAppService.GetAsync(input.Id);

            EditingCountryId = country.Id;
            EditingCountry = ObjectMapper.Map<CountryDto, CountryUpdateDto>(country);
            await EditingCountryValidations.ClearAll();
            await EditCountryModal.Show();
        }

        private async Task DeleteCountryAsync(CountryDto input)
        {
            await CountriesAppService.DeleteAsync(input.Id);
            await GetCountriesAsync();
        }

        private async Task CreateCountryAsync()
        {
            try
            {
                if (await NewCountryValidations.ValidateAll() == false)
                {
                    return;
                }

                await CountriesAppService.CreateAsync(NewCountry);
                await GetCountriesAsync();
                await CloseCreateCountryModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditCountryModalAsync()
        {
            await EditCountryModal.Hide();
        }

        private async Task UpdateCountryAsync()
        {
            try
            {
                if (await EditingCountryValidations.ValidateAll() == false)
                {
                    return;
                }

                await CountriesAppService.UpdateAsync(EditingCountryId, EditingCountry);
                await GetCountriesAsync();
                await EditCountryModal.Hide();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private void OnSelectedCreateTabChanged(string name)
        {
            SelectedCreateTab = name;
        }

        private void OnSelectedEditTabChanged(string name)
        {
            SelectedEditTab = name;
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






    }
}
