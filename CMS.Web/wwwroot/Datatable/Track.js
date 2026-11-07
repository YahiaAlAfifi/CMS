"use strict";

var TrackDatatable = function () {
    var load = function () {
        $('#kt_datatable').KTDatatable({
            data: {
                type: 'remote',
                source: { read: { url: '/Track/GetTrackData' } },
                pageSize: 10,
                saveState: false,
                serverPaging: true,
                serverFiltering: true,
                serverSorting: true
            },
            layout: { scroll: false },
            sortable: true,
            pagination: true,
            search: { input: $('#kt_datatable_search_query'), key: 'generalSearch' },
            columns: [
                { field: 'title', title: 'العنوان', width: 180 },
                { field: 'timeInMinute', title: 'المدة', width: 80 },
                { field: 'ownerName', title: 'صاحب المقطع', width: 140 },
                { field: 'category.name', title: 'التصنيف', width: 120 },
                {
                    field: 'trackUrl', title: 'المقطع', width: 260,
                    template: function (data) {
                        return '<audio controls style="width:240px"><source src="/Tracks/' + data.trackUrl + '"></audio>';
                    }
                },
                {
                    field: 'Actions', title: 'العمليات', sortable: false, width: 125,
                    overflow: 'visible', autoHide: false,
                    template: function (data) {
                        return '<a href="/Track/Update/' + data.id + '" class="PopUp btn btn-sm btn-clean btn-icon mr-2" title="تعديل المقطع"><i class="la la-edit text-warning"></i></a>' +
                            '<a href="/Track/Delete/' + data.id + '" tname="#kt_datatable" class="Confirm btn btn-sm btn-clean btn-icon" title="حذف"><i class="la la-trash text-danger"></i></a>';
                    }
                }
            ]
        });
    };

    return { init: load };
}();

jQuery(document).ready(function () { TrackDatatable.init(); });
