/// <reference path="../lib/jquery-grid/dist/js/jquery.jqGrid.min.js" />
$(function () {

    $("#jqGrid").jqGrid({
        url: "/Home/GetAudits",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['AuditId', 'Particulars', 'Accountability', 'Capex', 'Priority', 'Comments','Status'],
        colModel: [
            { key: true, hidden: true, name: 'AuditId', index: 'AuditId', editable: true },
            { key: false, name: 'Particulars', index: 'Particulars', editable: true },
            { key: false, name: 'Accountability', index: 'Accountability', editable: true },
            { key: false, name: 'Capex', index: 'Capex', editable: true, edittype: 'select', editoptions: { value: { 'Not Applicable': 'Not Applicable', 'Capex': 'Capex', 'Opex': 'Opex' } } },
            { key: false, name: 'Priority', index: 'Priority', editable: true, edittype: 'select', editoptions: { value: { 'Low': 'Low', 'Medium': 'Medium', 'High': 'High' } } },
            { key: false, name: 'Comments', index: 'Comments', editable: true },
            { key: false, name: 'Status', index: 'Status', editable: true, edittype: 'select', editoptions: { value: { 'Not Applicable': 'Not Applicable', 'Not Started': 'Not Started', 'In Progress': 'In Progress', 'Completed': 'Completed' } }}],
            
        pager: jQuery('#jqControls'),
        rowNum: 10,
        rowList: [10, 20, 30, 40, 50],
        height: '100%',
        viewrecords: true,
        caption: 'Audit Records',
        emptyrecords: 'No Audit Records are Available to Display',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            Id: "0"
        },
        autowidth: true,
        multiselect: false,
    }).navGrid('#jqControls', {
        edit: true, add: true, del: true, search: true,
                                searchtext: "Search Audit", refresh: true
    },
    {
        zIndex: 100,
        url: '/Home/Edit',
        closeOnEscape: true,
        closeAfterEdit: true,
        recreateForm: true,
        afterComplete: function (response) {
            if (response.responseText) {
                alert(response.responseText);
            }
        }
    },
    {
        zIndex: 100,
        url: "/Home/Create",
        closeOnEscape: true,
        closeAfterAdd: true,
        afterComplete: function (response) {
            if (response.responseText) {
                alert(response.responseText);
            }
        }
    },
    {
        zIndex: 100,
        url: "/Home/Delete",
        closeOnEscape: true,
        closeAfterDelete: true,
        recreateForm: true,
        msg: "Are you sure you want to delete Audit... ? ",
        afterComplete: function (response) {
            if (response.responseText) {
                alert(response.responseText);
            }
        }
    },
        {
            zIndex: 100,
            caption: "Search Audits",
            sopt: ['cn']
        });
});


