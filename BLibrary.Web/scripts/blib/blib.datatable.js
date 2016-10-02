//二次封装了datatables
//功能: datatables的所有功能都有保留 參考http://legacy.datatables.net/ref
//      checkbox控制是否分頁
//      可以返回自定義錯誤信息
//Option：
//        searchFilter: 查詢條件容器
//        failedCallback: 返回json的result為false的時候會調用，用於提示自定義錯誤信息
//        datatables的原有option都支持
//html attribute
//        data-filter: 定義查詢條件選擇器,和searchFilter功能一樣
//        data-url: 表格查詢url，與sAjaxSource 功能一樣
//        data-paginate-option:用於控制是否使用分頁的checkbox

(function ($) {
    $.fn.deltatable = function (options) {
        var $dataTable = this;
        var defaults = {
            searchFilter: '',
            failedCallback: function () {
            },
            successCallback: function () {
            },
            fnInitComplete: function () {
            },
            language: "en",
            deferLoading: 0,
            dom: '<"top">rt<"bottom">p<"clear">',
            responsive: true,
            serverSide: true,
            autoWidth: false,
            bFilter: false,
            iDisplayLength: 10,
            processing: false,
            bLengthChange: false,
            bAutoWidth: false,
            bPaginate: true,
            bSort: false
        };


        var settings = $.extend({}, defaults, options);

        //對datatables參數做一些處理
        extendSettings(settings);
        setLanguage(settings);


        //查詢條件
        var filterContainer = $(this).attr('data-filter');

        if (filterContainer != null) {
            settings.searchFilter = filterContainer;
            var searchCriteria = [];
            //searchCriteria = manageFilters($dataTable, settings);

            var searchButton = $(settings.searchFilter).attr('data-button-search');
            var clearButton = $(settings.searchFilter).attr('data-button-clear');

            var searchFilters = $(settings.searchFilter).find('input,textarea,select');

            //註冊查詢按鈕
            if (searchButton != null) {
                $(searchButton).on('click', function () {
                    searchCriteria = [];
                    //changePaginate(datatable, settings);

                    searchFilters.each(function () {
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
            }

            settings.fnServerParams = function (aoData) {
                var i;
                for (i = 0; i < searchCriteria.length; i++) {
                    aoData.push(searchCriteria[i]);
                }
            };
        }

        var paginateOption = $(this).attr('data-paginate-option');
        if (paginateOption != null) {
            $(paginateOption).prop('checked', true);
            if ($(paginateOption).is(':checkbox')) {
                $(paginateOption).change(function () {
                    resetPaginateTable($dataTable, settings, this.checked);
                });
            }
        }

        var url = $(this).attr('data-url');
        if (url != null) {
            settings.sAjaxSource = url;
        }

        //調用 datatables plugin
        return $dataTable.dataTable(settings);
    }

    //自定义数据访问方式
    function extendSettings(settings) {
        settings.fnServerData = function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR =
            dataObj.requestURI(
                       sSource,
                        function (e) {
                            if (e.Result == null || e.Result) {
                                if (settings.successCallback) {
                                    settings.successCallback(e);
                                }
                                fnCallback(e);
                            }
                            else {
                                if (settings.failedCallback) {
                                    settings.failedCallback(e.ErrorMessage);
                                }
                                vw.showMsg(e.ErrorMessage);
                            }
                        },
                        function (data) {
                            vw.showMsg(data);
                        },
                        aoData,
                        'GET',
                        'application/json',
                        false,
                        null
                     );
        }
    }

    //根据一个checkbox来設置是否分页
    function resetPaginateTable(datatable, settings, isPaginate) {
        settings.bPaginate = isPaginate;
        datatable.fnDestroy();
        datatable.dataTable(settings);
        datatable.fnDraw();
    }
    //註冊查詢條件
    function manageFilters(datatable, settings) {
        var searchButton = $(settings.searchFilter).attr('data-button-search');
        var clearButton = $(settings.searchFilter).attr('data-button-clear');

        var searchFilters = $(settings.searchFilter).find('input,textarea,select');

        //註冊查詢按鈕
        if (searchButton != null) {
            $(searchButton).on('click', function () {
                searchCriteria = [];
                //changePaginate(datatable, settings);

                searchFilters.each(function () {
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
                datatable.fnDraw();
            });
        }
    }
    //設置語言
    function setLanguage(settings) {
        if (settings.language == 'en') {
            settings.oLanguage = {
                oPaginate: {
                    "sPrevious": "Previous",
                    "sNext": "Next",
                },
                sEmptyTable: "No records are found in the query coverage! "
            }
        }
        else if (settings.language.toUpperCase() == 'ZH-TW') {
            settings.oLanguage = {
                oPaginate: {
                    "sPrevious": "上頁",
                    "sNext": "下頁",
                },
                sEmptyTable: "在查詢範圍內無記錄! "
            }
        }
    }
}(jQuery));
