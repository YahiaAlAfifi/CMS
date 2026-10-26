"use strict";

var AdvertisementDatatable = function () {
    var load = function () {
        var datatable = $('#kt_datatable').KTDatatable({
            data: {
                type: 'remote',
                source: {
                    read: {
                        url: '/Advertisement/GetAdvertisementData'
                    }
                },
                pageSize: 10,
                saveState: false,
                serverPaging: true,
                serverFiltering: true,
                serverSorting: true
            },
            layout: { scroll: false },
            sortable: true,
            pagination: true,
            search: {
                input: $('#kt_datatable_search_query'),
                key: 'generalSearch'
            },
            columns: [
                { field: 'title', title: 'العنوان', width: 250 },
                { field: 'startDate', title: 'البداية', width: 100 },
                { field: 'endDate', title: 'النهاية', width: 100 },
                { field: 'price', title: 'السعر', width: 100 },
                { field: 'owner.fullName', title: 'صاحب الاعلان', width: 200 },
                {
                    field: 'Actions',
                    title: 'العمليات',
                    sortable: false,
                    width: 80,
                    overflow: 'visible',
                    autoHide: false,
                    template: function (data) {
                        return '<a href="/Advertisement/Delete/' + data.id + '" tname="#kt_datatable" class="Confirm btn btn-sm btn-clean btn-icon" title="حذف">' +
                            '<span class="svg-icon svg-icon-danger svg-icon-md">' +
                            '<svg xmlns="http://www.w3.org/2000/svg" width="24px" height="24px" viewBox="0 0 24 24">' +
                            '<g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">' +
                            '<rect x="0" y="0" width="24" height="24" />' +
                            '<path d="M6,8 L6,20.5 C6,21.3 6.7,22 7.5,22 L16.5,22 C17.3,22 18,21.3 18,20.5 L18,8 Z" fill="#000000" />' +
                            '<path d="M14,4.5 L14,4 C14,3.4 13.6,3 13,3 L11,3 C10.4,3 10,3.4 10,4 L10,4.5 L5.5,4.5 C5.2,4.5 5,4.7 5,5 L5,5.5 C5,5.8 5.2,6 5.5,6 L18.5,6 C18.8,6 19,5.8 19,5.5 L19,5 C19,4.7 18.8,4.5 18.5,4.5 Z" fill="#000000" opacity="0.3" />' +
                            '</g></svg></span></a>';
                    }
                }
            ]
        });
    };

    return { init: load };
}();

jQuery(document).ready(function () {
    AdvertisementDatatable.init();
});
