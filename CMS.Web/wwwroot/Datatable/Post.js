"use strict";

var PostDatatable = function () {
    var load = function () {
        $('#kt_datatable').KTDatatable({
            data: {
                type: 'remote',
                source: { read: { url: '/Post/GetPostData' } },
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
                { field: 'category.name', title: 'التصنيف', width: 100 },
                { field: 'author.fullName', title: 'الكاتب', width: 130 },
                { field: 'timeInMinute', title: 'مدة القراءة', width: 90 },
                { field: 'status', title: 'الحالة', width: 90 },
                { field: 'createdAt', title: 'تاريخ الاضافة', width: 100 },
                {
                    field: 'Actions', title: 'العمليات', sortable: false, width: 210,
                    overflow: 'visible', autoHide: false,
                    template: function (data) {
                        return '<a href="/Post/Update/' + data.id + '" class="PopUp btn btn-sm btn-clean btn-icon" title="تعديل الخبر"><i class="la la-edit text-warning"></i></a>' +
                            '<a href="/Post/UpdateStatus?id=' + data.id + '&status=2" tname="#kt_datatable" class="Confirm btn btn-sm btn-clean btn-icon" title="اعتماد"><i class="la la-check text-success"></i></a>' +
                            '<a href="/Post/UpdateStatus?id=' + data.id + '&status=3" tname="#kt_datatable" class="Confirm btn btn-sm btn-clean btn-icon" title="رفض"><i class="la la-times text-danger"></i></a>' +
                            '<a href="/Post/GetLog/' + data.id + '" class="PopUp btn btn-sm btn-clean btn-icon" title="سجل الحالة"><i class="la la-history text-info"></i></a>' +
                            '<a href="/Post/Delete/' + data.id + '" tname="#kt_datatable" class="Confirm btn btn-sm btn-clean btn-icon" title="حذف"><i class="la la-trash text-danger"></i></a>';
                    }
                }
            ]
        });
    };

    return { init: load };
}();

jQuery(document).ready(function () { PostDatatable.init(); });
