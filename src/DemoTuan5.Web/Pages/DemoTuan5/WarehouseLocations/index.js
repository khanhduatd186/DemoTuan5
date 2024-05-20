$(function () {
    var l = abp.localization.getResource("DemoTuan5");
	
	var warehouseLocationService = window.demoTuan5.warehouseLocations.warehouseLocation;
	
        var lastNpIdId = '';
        var lastNpDisplayNameId = '';

        var _lookupModal = new abp.ModalManager({
            viewUrl: abp.appPath + "Shared/LookupModal",
            scriptUrl: abp.appPath + "Pages/Shared/lookupModal.js",
            modalClass: "navigationPropertyLookup"
        });

        $('.lookupCleanButton').on('click', '', function () {
            $(this).parent().find('input').val('');
        });

        _lookupModal.onClose(function () {
            var modal = $(_lookupModal.getModal());
            $('#' + lastNpIdId).val(modal.find('#CurrentLookupId').val());
            $('#' + lastNpDisplayNameId).val(modal.find('#CurrentLookupDisplayName').val());
        });
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "DemoTuan5/WarehouseLocations/CreateModal",
        scriptUrl: abp.appPath + "Pages/DemoTuan5/WarehouseLocations/createModal.js",
        modalClass: "warehouseLocationCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "DemoTuan5/WarehouseLocations/EditModal",
        scriptUrl: abp.appPath + "Pages/DemoTuan5/WarehouseLocations/editModal.js",
        modalClass: "warehouseLocationEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            code: $("#CodeFilter").val(),
			description: $("#DescriptionFilter").val(),
            active: (function () {
                var value = $("#ActiveFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
			idxMin: $("#IdxFilterMin").val(),
			idxMax: $("#IdxFilterMax").val(),
			countryId: $("#CountryIdFilter").val(),			warehouseId: $("#WarehouseIdFilter").val()
        };
    };
    
    //<suite-custom-code-block-1>
    //</suite-custom-code-block-1>
    
    var dataTableColumns = [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('DemoTuan5.WarehouseLocations.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.warehouseLocation.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('DemoTuan5.WarehouseLocations.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    warehouseLocationService.delete(data.record.warehouseLocation.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reloadEx();;
                                        });
                                }
                            }
                        ]
                },
                width: "1rem"
            },
			{ data: "warehouseLocation.code" },
			{ data: "warehouseLocation.description" },
            {
                data: "warehouseLocation.active",
                render: function (active) {
                    return active ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            },
			{ data: "warehouseLocation.idx" },
            {
                data: "country.code",
                defaultContent : ""
            },
            {
                data: "warehouse.code",
                defaultContent : ""
            }        
    ];
    
    

    var dataTable = $("#WarehouseLocationsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(warehouseLocationService.getList, getFilter),
        columnDefs: dataTableColumns
    }));
    
    //<suite-custom-code-block-2>
    //</suite-custom-code-block-2>

    createModal.onResult(function () {
        dataTable.ajax.reloadEx();;
    });

    editModal.onResult(function () {
        dataTable.ajax.reloadEx();;
    });

    $("#NewWarehouseLocationButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reloadEx();;
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        warehouseLocationService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/demo-tuan5/warehouse-locations/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'code', value: input.code }, 
                            { name: 'description', value: input.description }, 
                            { name: 'active', value: input.active },
                            { name: 'idxMin', value: input.idxMin },
                            { name: 'idxMax', value: input.idxMax }, 
                            { name: 'countryId', value: input.countryId }
, 
                            { name: 'warehouseId', value: input.warehouseId }
                            ]);
                            
                    var downloadWindow = window.open(url, '_blank');
                    downloadWindow.focus();
            }
        )
    });

    $('#AdvancedFilterSectionToggler').on('click', function (e) {
        $('#AdvancedFilterSection').toggle();
    });

    $('#AdvancedFilterSection').on('keypress', function (e) {
        if (e.which === 13) {
            dataTable.ajax.reloadEx();;
        }
    });

    $('#AdvancedFilterSection select').change(function() {
        dataTable.ajax.reloadEx();;
    });
    
    //<suite-custom-code-block-3>
    //</suite-custom-code-block-3>
    
    
    
    
    
    
});
