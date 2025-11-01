$(document).ready(function () {

    $('#tblStudents').DataTable({
        processing: true,
        serverSide: true,
        filter: true,
        orderMulti: false,
        stateSave: true,
        paging: true,
        select: true,
        order: [[0, "desc"]],
        dom: 'Bfrtip',
        buttons: [
            'pageLength', 'excelHtml5', 'pdfHtml5', 'print'
        ],
        ajax: {
            url: '/Students/GetDataTabelData',
            type: 'POST',
            datatype: 'json'
        },
        columns: [
            {
                data: "CreatedDate", name: "CreatedDate",
                render: function (data, type, row) {
                    return "<button class='bi-eye btn btn-primary' title='Show Details' onclick=Details('" + row.Id + "');></button>";
                }
            },
            { data: "ArName", name: "ArName" },
            { data: "EnName", name: "EnName" },
            { data: "SerialNumber", name: "SerialNumber" },
            { data: "UserName", name: "UserName" },
            { data: "Password", name: "Password" },
            { data: "TeachersCount", name: "TeachersCount" },
            {
                data: "CreatedDate",
                name: "CreatedDate",
                render: function (data) {
                    if (!data) return '';
                    var date = new Date(data);
                    return date.getDate() + "/" + (date.getMonth() + 1) + "/" + date.getFullYear();
                }
            },
            {
                data: null,
                name: null,
                render: function (data, type, row) {
                    return "<button class='btn btn-info btn-xs bi bi-pencil-square' title='Edit' onclick=AddEdit('" + row.Id + "');></button>";
                }
            },
            {
                data: null,
                name: null,
                render: function (data, type, row) {
                    return "<button class='btn btn-danger btn-xs bi-trash-fill' title='Delete' onclick=Delete('" + row.Id + "');></button>";
                }
            }
        ],
        columnDefs: [
            { targets: [8, 9], orderable: false }
        ],
        lengthMenu: [
            [10, 20, 50, 100, 200, 500, 700, 1000, 1500, 2000],
            [10, 20, 50, 100, 200, 500, 700, 1000, 1500, 2000]
        ]
    });
});