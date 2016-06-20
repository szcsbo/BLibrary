(function ($) {
    //封裝包含查詢條件datatable
    $.fn.DatatablesWithFilters = function (settings) {
        var $dataTable = this,
        searchCriteria = [],
        filterOptions = settings.filterOptions,
        $searchContainerInputs = $('#' + filterOptions.searchContainer).find('input[type="text"],input[type="radio"],input[type="checkbox"],select,textarea');
        delete settings.filterOptions;
        if (filterOptions === undefined) {
            throw {
                name: 'filterOptionsUndefinedError',
                message: 'Please define a filterOptions property in the object literal'
            };
        }
        if (filterOptions.searchButton === undefined) {
            throw {
                name: 'searchButtonUndefinedError',
                message: 'Please define a searchButton in the filterOptions'
            };
        }
        if (filterOptions.clearSearchButton === undefined) {
            throw {
                name: 'clearSearchButtonUndefinedError',
                message: 'Please define a clearSearchButton in the filterOptions'
            };
        }
        if (filterOptions.searchContainer === undefined) {
            throw {
                name: 'searchContainerUndefinedError',
                message: 'Please define a searchContainer in the filterOptions'
            };
        }
        $searchContainerInputs.keypress(function (e) {
            if (e.keyCode === 13) {
                $("#" + filterOptions.searchButton).click();
            }
        });
        $("#" + filterOptions.searchButton).click(function () {
            searchCriteria = [];
            var searchContainer = $("#" + filterOptions.searchContainer);
            searchContainer.find('input[type="text"],input[type="radio"]:checked,input[type="checkbox"]:checked,textarea,select').each(function () {
                var element = $(this), value = element.val();
                if (typeof value === "string") {
                    searchCriteria.push({ "name": element.attr("name"), "value": value });
                }
                else if (Object.prototype.toString.apply(value) === '[object Array]') {
                    var i;
                    for (i = 0; i < value.length; i++) {
                        searchCriteria.push({ "name": element.attr("name"), "value": value[i] });
                    }
                }
            });
            $dataTable.fnDraw();
        });
        $("#" + filterOptions.clearSearchButton).click(function () {
            searchCriteria = [];
            $searchContainerInputs.each(function () {
                var $input = $(this),
                tagName = this.tagName.toLowerCase();
                if (tagName === "input") {
                    var type = $input.attr("type").toLowerCase();
                    if (type === "checkbox"
                    || type === "radio") {
                        $input.removeAttr("checked");
                    }
                    else if (type === "text") {
                        $input.val("");
                    }
                }
                else if (tagName === "select") {
                    if ($input.attr("multiple") !== undefined) {
                        $input.val([]);
                    }
                    else {
                        $input.val("");
                    }
                }
                else if (tagName === "textarea") {
                    $input.val("");
                }
            });
            $dataTable.fnDraw();
        });
        settings.fnServerParams = function (aoData) {
            var i;
            for (i = 0; i < searchCriteria.length; i++) {
                aoData.push(searchCriteria[i]);
            }
        };
        return $dataTable.dataTable(settings);
    };

}(jQuery))

var QueryTable = Class.create();
QueryTable.prototype = {
    initialize: function () {
        this.lib = new Global();
    },
    //提供簡單的表查詢功能，對於特殊化得查詢表格，請使用DatatablesWithFilters
    createTable: function (option) {
        var columns = [];
        for (var i = 0; i < option.columnNames.length; i++) {
            columns.push({ 'data': option.columnNames[i] });
        }
       
        $('#' + option.targetTable).DatatablesWithFilters({
            sAjaxSource: option.url,
            serverSide: true,
            responsive: true,
            bFilter: false,
            iDisplayLength: 10,
            processing: false,
            bLengthChange: false,
            bAutoWidth: false,
            columns: columns,
            dom: '<"top">rt<"bottom">p<"clear">',
            fnDrawCallback: function (oSettings) {
                if (option.drawCallback)
                    option.drawCallback;
            },
            fnInitComplete: function (oSettings, json) {
                if (option.completeCallback)
                    option.completeCallback;
            },
            filterOptions: { searchButton: option.searchButton, clearSearchButton: option.clearButton, searchContainer: option.searchContainer }
        });
    }
}
var qt = new QueryTable();

////sample using
//qt.createTable({
//    url: '@Url.Action("GetTRSForm","TRSEF02")',
//    targetTable: 'pageTable',
//    columnNames: ['InstanceCode', 'ApplyEmpCode', 'FactoryCode'],
//    searchButton: 'btnSearch',
//    clearButton: 'btnClear',
//    searchContainer: 'searchFilter',
//});