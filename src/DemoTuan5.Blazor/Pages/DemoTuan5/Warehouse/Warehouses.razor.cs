using Blazorise;
using Blazorise.Snackbar;
using DevExpress.Blazor;

using DemoTuan5.Countries;
using DemoTuan5.Permissions;
using DemoTuan5.WarehouseLocations;
using DemoTuan5.Warehouses;


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Messages;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;

namespace DemoTuan5.Blazor.Pages.DemoTuan5.Warehouse
{
    public partial class Warehouses
    {
		protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
		protected PageToolbar Toolbar { get; } = new PageToolbar();
	
		private int MaxCount { get; } = 1000;

		private bool CanCreateLotSerialClass { get; set; }
		private bool CanEditLotSerialClass { get; set; }
		private bool CanDeleteLotSerialClass { get; set; }

		private bool CanCreateLotSerSegment { get; set; }
		private bool CanEditLotSerSegment { get; set; }
		private bool CanDeleteLotSerSegment { get; set; }

		private WarehouseUpdateDto EditingWarehouse { get; set; }
		private Guid EditingWarehouseId { get; set; }

		private readonly IUiMessageService _uiMessageService;
		SnackbarStack _uiNotification;


		private string FocusedColumn { get; set; }


		private bool IsDataEntryChanged { get; set; } //keep value to indicate data has been changed or not
		private EditForm EditFormMain { get; set; } //Id of Main form 

	
		private bool ShowInteractionForm { get; set; } = true;
		bool PanelVisible { get; set; }

		[Parameter]
		public string Id { get; set; }

		private IGrid WarehouseLocationGrid { get; set; } //Company grid control name
		private List<WarehouseLocationUpdateDto> WarehouseLocationList { get; set; } = new List<WarehouseLocationUpdateDto>(); //Data source used to bind to grid
        private IReadOnlyList<object> selectedWarehouseLocation { get; set; } = new List<WarehouseLocationUpdateDto>(); //Selected rows on grid
		private IReadOnlyList<CountryDto> CountriesCollection { get; set; } = new List<CountryDto>();

		 
        private EditContext _gridWarehouseLocationtEditContext;

        //==================================Initialize Section===================================
        #region
        public Warehouses(IUiMessageService uiMessageService)
		{
			_uiMessageService = uiMessageService;
			EditingWarehouse = new WarehouseUpdateDto();
			EditingWarehouse.ConcurrencyStamp = string.Empty;
       
        }
        protected override async Task OnInitializedAsync()
        {
            EditingWarehouseId = Guid.Parse(Id);
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				await PageProgressService.Go(null, options => { options.Color = Color.Info; }); 
				await LoadGridData();
				await PageProgressService.Go(-1);
			}
			await base.OnAfterRenderAsync(firstRender);
		}
	
		protected virtual ValueTask SetBreadcrumbItemsAsync()
		{
			BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:LotSerialClasses"]));
			return ValueTask.CompletedTask;
		}

		protected virtual ValueTask SetToolbarItemsAsync()
		{
			Toolbar.AddButton(L["Back"], async () =>
			{
				NavigationManager.NavigateTo($"/DemoTuan5/Warehouses");
			},
			IconName.Undo,
			Color.Secondary);
			Toolbar.AddButton(L["Save"], async () =>
			{
				await PageProgressService.Go(null, options => { options.Color = Color.Info; });
				await SaveClassesAsync(false);
				await PageProgressService.Go(-1);
			},
			  IconName.Save,
			  Color.Primary,
			   requiredPolicyName: DemoTuan5Permissions.Warehouses.Edit);
         
            return ValueTask.CompletedTask;
		}
		private async Task SetPermissionsAsync()
		{
			CanCreateLotSerialClass = await AuthorizationService
				.IsGrantedAsync(DemoTuan5Permissions.Warehouses.Create);
			CanEditLotSerialClass = await AuthorizationService
							.IsGrantedAsync(DemoTuan5Permissions.Warehouses.Edit);
			CanDeleteLotSerialClass = await AuthorizationService
							.IsGrantedAsync(DemoTuan5Permissions.Warehouses.Delete);

			CanCreateLotSerSegment = await AuthorizationService
			.IsGrantedAsync(DemoTuan5Permissions.WarehouseLocations.Create);
			CanEditLotSerSegment = await AuthorizationService
							.IsGrantedAsync(DemoTuan5Permissions.WarehouseLocations.Edit);
			CanDeleteLotSerSegment = await AuthorizationService
							.IsGrantedAsync(DemoTuan5Permissions.WarehouseLocations.Delete);
		}
        #endregion
        //======================Load Data Source for ListView & Others===========================
        #region
        private async Task GetWarehouseLocationAsync()
		{
			if (EditingWarehouseId != Guid.Empty)
			{
				var result = await WarehouseLocationsAppService.GetListNoPagedAsync(new GetWarehouseLocationsInput
				{
					WarehouseId = EditingWarehouseId,
					MaxResultCount = MaxCount,
				});
				WarehouseLocationList = ObjectMapper.Map<List<WarehouseLocationDto>, List<WarehouseLocationUpdateDto>>((List<WarehouseLocationDto>)result);
	
            }
			else if (EditingWarehouseId == Guid.Empty)
			{
				WarehouseLocationList = null;
			}
		}
        private async Task GetCountryCollectionLookupAsync()
        {
            var result = (await CountriesAppService.GetListAsync(new GetCountriesInput { }));
            CountriesCollection = result.Items.ToList();
        }
        #endregion

        //======================CRUD & Load Main Data Source Section=============================
        #region
        private async Task LoadGridData()
        {
            if (IsDataEntryChanged)
            {
                var confirmed = await _uiMessageService.Confirm(L["DeleteConfirmationMessage"]);
            }

            await SetPermissionsAsync();
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await GetCountryCollectionLookupAsync();
            await LoadDataAsync(EditingWarehouseId);
        }
        private async Task LoadDataAsync(Guid classId)
		{
			await PageProgressService.Go(null, options => { options.Color = Color.Info; });
			if (classId != Guid.Empty)
			{
				EditingWarehouse = ObjectMapper.Map<WarehouseDto, WarehouseUpdateDto>(await WarehousesAppService.GetAsync(classId));
				await GetWarehouseLocationAsync();
				IsDataEntryChanged = false;
			}
			else
			{
				EditingWarehouse = new WarehouseUpdateDto();
				WarehouseLocationList = new List<WarehouseLocationUpdateDto> { };
			}
			await InvokeAsync(StateHasChanged);
			await PageProgressService.Go(-1);
		}

		#region Thêm & Lưu & Xóa
		private async Task NewClass()
		{
			EditingWarehouse = new WarehouseUpdateDto
			{
				ConcurrencyStamp = string.Empty,
			};

			EditingWarehouseId = Guid.Empty;
			IsDataEntryChanged = false;
			NavigationManager.NavigateTo($"/DemoTuan5/Warehouses/{Guid.Empty}");
			await LoadDataAsync(EditingWarehouseId);
		}

		private async Task SaveClassesAsync(bool IsNewNext)
		{
			try
			{
				var allCodes = await WarehousesAppService.GetListNoPagedAsync(new GetWarehousesInput { });
				bool codeExists = allCodes.Any(code => code.Code == EditingWarehouse.Code);
				bool idExists = allCodes.Any(code => code.Id == EditingWarehouseId);

				if (WarehouseLocationGrid != null && (WarehouseLocationGrid.IsEditingNewRow() || WarehouseLocationGrid.IsEditing()))
				{
					await WarehouseLocationGrid.SaveChangesAsync();
				}

				if (!EditFormMain.EditContext.Validate())
				{
					return;
				}

				if (EditingWarehouseId == Guid.Empty)
				{
					if (!codeExists)
					{
						EditingWarehouse = ObjectMapper.Map<WarehouseDto, WarehouseUpdateDto>(await WarehousesAppService.CreateAsync(ObjectMapper.Map<WarehouseUpdateDto, WarehouseCreateDto>(EditingWarehouse)));
						EditingWarehouseId = EditingWarehouse.Id;
						await SaveWarehouseLocationAsync();
						IsDataEntryChanged = false;
						//await HandleHistoryAdded();
						/*await _uiNotification.PushAsync(
								L["Notification:Save"], SnackbarColor.Success
							);*/
					}
					else
					{
						await _uiMessageService.Error(L["Notification:CodeAlreadyExists"]);
					}
				}
				else if (EditingWarehouseId != Guid.Empty)
				{
					var duplicateCode = allCodes.FirstOrDefault(code => code.Code == EditingWarehouse.Code);

					if (duplicateCode != null && duplicateCode.Id == EditingWarehouseId || !codeExists)
					{
						
						await WarehousesAppService.UpdateAsync(EditingWarehouseId, EditingWarehouse);
						EditingWarehouse = ObjectMapper.Map<WarehouseDto, WarehouseUpdateDto>(await WarehousesAppService.GetAsync(EditingWarehouseId));
						await SaveWarehouseLocationAsync();
						IsDataEntryChanged = false;
						ResetToolbar();
						//await HandleHistoryAdded();
						/*await _uiNotification.PushAsync(
								L["Notification:Edit"], SnackbarColor.Success);*/
					}
					else
					{
						await _uiMessageService.Error(L["Notification:CodeAlreadyExists"]);
					}
				}

				if (IsNewNext)
				{
					await NewClass();
					ResetToolbar();
				}
				else
				{
					IsDataEntryChanged = false;
					NavigationManager.NavigateTo($"/DemoTuan5/Warehouses/{EditingWarehouseId}");
				}
			}
			catch (Exception ex)
			{
				await HandleErrorAsync(ex);
			}
		}

		/*private async Task DeleteClass()
		{
			if (EditingWarehouseId != Guid.Empty)
			{
				var confirmed = await _uiMessageService.Confirm(L["DeleteConfirmationMessage"]);
				if (confirmed)
				{
					IsDataEntryChanged = false;
					await WarehouseLocationsAppService.DeleteAsync(EditingWarehouseId);
					NavigationManager.NavigateTo($"/DemoTuan5/Warehouses");
				}
			}
			else
				await _uiMessageService.Warn(L["Message:CannotDelete"]);
		}*/

		#endregion

		
		private async Task Duplicate(Guid accountId)
		{
			EditingWarehouse = new WarehouseUpdateDto()
			{
				ConcurrencyStamp = string.Empty,
			};

			EditingWarehouseId = Guid.Empty;
			IsDataEntryChanged = false;
			NavigationManager.NavigateTo($"/DemoTuan5/Warehouses/{Guid.Empty}");
			EditingWarehouse = ObjectMapper.Map<WarehouseDto, WarehouseUpdateDto>(await WarehousesAppService.GetAsync(accountId));
			EditingWarehouse.Code = string.Empty;
		
		}
        #endregion
        //=====================================Validations=======================================
        #region
        #region THÔNG BÁO KHI CÓ SỰ THAY ĐỔI NHƯNG MUỐN BACK RA NGOÀI
        private async Task<bool> SavingConfirmAsync()
		{
			if (IsDataEntryChanged)
			{
				var confirmed = await _uiMessageService.Confirm(L["SavingConfirmationMessage"]);
				if (confirmed)
					return true;
				else
					return false;
			}
			else
				return true;
		}
		#endregion

		#endregion

		//============================Controls triggers/events===================================
		#region  
		private void ResetToolbar()
		{
			Toolbar.Contributors.Clear();
			SetToolbarItemsAsync();
		}

	/*	private async Task HandleCommentAdded()
		{
			await formActivity.GetCommentListAsync();
		}*/

		private async Task OnBeforeInternalNavigation(LocationChangingContext context)
		{
			bool checkSaving = await SavingConfirmAsync();
			if (!checkSaving)
				context.PreventNavigation();
		}

		private async Task ToggleInteractionFormAsync()
		{
			ShowInteractionForm = !ShowInteractionForm;
		}

		#region LOT SERIAL SEGMENT GRID
		EditContext GridWarehouseLocationEditContext
		{
			get { return WarehouseLocationGrid.IsEditing() ? _gridWarehouseLocationtEditContext : null; }
			set { _gridWarehouseLocationtEditContext = value; }
		}


		//Kiểm tra dòng dữ liệu nào bị thây đổi và lưu lại khi có thây đổi
		private async Task WarehouseLocationGrid_OnFocusedRowChanged(GridFocusedRowChangedEventArgs e)
		{
			if (WarehouseLocationGrid.IsEditing() && _gridWarehouseLocationtEditContext.IsModified())
			{
				await WarehouseLocationGrid.SaveChangesAsync();
				IsDataEntryChanged = true;
			}
			else
				await WarehouseLocationGrid.CancelEditAsync();
		}



		private void WarehouseLocationGrid_OnCustomizeEditModel(GridCustomizeEditModelEventArgs e)
		{
			if (e.IsNew)
			{
				var newRow = (WarehouseLocationUpdateDto)e.EditModel;
				newRow.Id = Guid.Empty;

				if (WarehouseLocationGrid.GetVisibleRowCount() > 0)
				{
					int maxIdx = WarehouseLocationList.Max(x => x.Idx); 
					//int missingNumber = MissingNumberHelper.FindMissingNumber(WarehouseLocationList, x => x.SegmentCode, maxIdx);
					newRow.Idx = /* missingNumber != -1 ? missingNumber : */ maxIdx + 1;
				}
				else
				{
					newRow.Idx = 1;
				}

				newRow.WarehouseId = EditingWarehouseId;
				newRow.ConcurrencyStamp = string.Empty;
			}
		}
		//hiện icon khi edit dữ liệu
		private void WarehouseLocationGrid_CustomizeDataRowEditor(GridCustomizeDataRowEditorEventArgs e)
		{
			((ITextEditSettings)e.EditSettings).ShowValidationIcon = true;
		}

		private async Task<bool> IsExisting(string code,Guid? countryId)
		{
			var result = WarehouseLocationList.Find(item => item.Code == code && item.CountryId == countryId);

			if (result != null)
				return true;
			else
				return false;
		}
		

		private async Task WarehouseLocationGrid_EditModelSaving(GridEditModelSavingEventArgs e)
		{

			
			WarehouseLocationUpdateDto editModel = (WarehouseLocationUpdateDto)e.EditModel;//dữ liệu chỉnh sửa trên grid
			WarehouseLocationUpdateDto dataItem = e.IsNew ? new WarehouseLocationUpdateDto() : WarehouseLocationList.Find(item => item.Idx == editModel.Idx);// dữ liệu ban đầu

				if (editModel != null && !e.IsNew)
				{ 
					IsDataEntryChanged = true;
					WarehouseLocationList.Remove(dataItem);
					WarehouseLocationList.Add(editModel);
				}
				if (editModel != null && e.IsNew)
				{
					IsDataEntryChanged = true;
					WarehouseLocationList.Add(editModel);
				}
		}


		private async Task SaveWarehouseLocationAsync()
		{
            try
            {
                await Task.CompletedTask;
                foreach (var cpn in WarehouseLocationList)
                {
                    if (cpn.WarehouseId == Guid.Empty)
                        cpn.WarehouseId = EditingWarehouseId;

                    if (cpn.ConcurrencyStamp == string.Empty)
                        await WarehouseLocationsAppService.CreateAsync(ObjectMapper.Map<WarehouseLocationUpdateDto, WarehouseLocationCreateDto>(cpn));
					else
					    await WarehouseLocationsAppService.UpdateAsync(cpn.Id, ObjectMapper.Map<WarehouseLocationUpdateDto, WarehouseLocationUpdateDto>(cpn));
				}
				await GetWarehouseLocationAsync();
                //await InvokeAsync(StateHasChanged);

            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

		private async Task DeleteWarehouseLocation()
		{
			var confirmed = await _uiMessageService.Confirm(L["DeleteConfirmationMessage"]);
			if (confirmed)
			{
				if (selectedWarehouseLocation != null)
				{
					foreach (WarehouseLocationUpdateDto row in selectedWarehouseLocation)
					{
						await WarehouseLocationsAppService.DeleteAsync(row.Id);
						WarehouseLocationList.Remove(row);
					}
				}
				WarehouseLocationGrid.Reload();
				//await InvokeAsync(StateHasChanged);
			}
		}

		private async Task BtnDelete_WarehouseLocationGrid_OnClick()
		{
			await DeleteWarehouseLocation();
			await WarehouseLocationGrid.CancelEditAsync();
		}

		private async Task BtnAdd_WarehouseLocationGrid_OnClick()
		{
			await WarehouseLocationGrid.SaveChangesAsync();
			WarehouseLocationGrid.ClearSelection();
			await WarehouseLocationGrid.StartEditNewRowAsync();
		}

		private async Task WarehouseLocationGrid_OnKeyDown(KeyboardEventArgs e)
		{
			if (e.Key == "F2" && CanEditLotSerSegment || e.AltKey && e.Key == "S" || e.AltKey && e.Key == "s")
			{
				await WarehouseLocationGrid.SaveChangesAsync();
				await WarehouseLocationGrid.StartEditRowAsync(WarehouseLocationGrid.GetFocusedRowIndex());
				WarehouseLocationGrid.ClearSelection();
				await WarehouseLocationGrid.StartEditNewRowAsync();
			}
		}
	

		#endregion

		/*bool disabledButton { get; set; } = true;
		private TrackingMethodTypeList SelectedTrackingMethod { get; set; } = new TrackingMethodTypeList();
		async Task SelectedTrackingMethodChangedAsync(TrackingMethodTypeList e)
		{
			SelectedTrackingMethod = e;
			IsDataEntryChanged = true;

			if (SelectedTrackingMethod.Value == TrackingMethodType.NotTracked.ToString())
			{
				disabledButton = true;
			}
			else
			{
				disabledButton = false;
			}
		}*/
		#endregion

	}
}

