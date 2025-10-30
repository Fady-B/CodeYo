var AddEdit = function (id) {

    var url = "/Teachers/AddEdit?TeacherId=" + id;
    if (id != "0") {
        $('#titleExtraBigModal').html("Edit Teacher");
    }
    else {
        $('#titleExtraBigModal').html("Add Teacher");
    }

    loadExtraBigModal(url);
};



var Details = function (id) {
    var url = "/Teachers/Details?TeacherId=" + id;
    $('#titleBigModal').html("Teacher Details");
    loadBigModal(url);
};



var Save = function () {

    if (!$("#frmTeacher").valid()) {
        return;
    }
    var _frmTeacher = $("#frmTeacher").serialize();

    $("#btnSave").html("Saving...");
    $('#btnSave').attr('disabled', 'disabled');
    $.ajax({
        type: "POST",
        url: "/Teachers/AddEdit",
        data: _frmTeacher,
        success: function (result) {
            debugger
            $("#btnSave").html("Save");
            $('#btnSave').removeAttr('disabled');
            if (result.IsSuccess) {
                Swal.fire({
                    title: result.AlertMessage,
                    icon: "success"
                }).then(function () {
                    document.getElementById("btnClose").click();
                    $('#tblTeacher').DataTable().ajax.reload();
                });
            }
            else {
                SwalSimpleAlert(result.AlertMessage, "warning");
            }
        },
        error: function (errormessage) {
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });
}

var Delete = function (id) {
    Swal.fire({
        title: 'Do you want to delete this teacher?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "POST",
                url: "/Teachers/Delete?TeacherId=" + id,
                success: function (result) {
                    if (result.IsSuccess) {
                        Swal.fire({
                            title: result.AlertMessage,
                            icon: "info",
                            didClose: () => {
                                $('#tblTeacher').DataTable().ajax.reload();
                            }
                        });
                    }
                    else {
                        SwalSimpleAlert(result.AlertMessage, "warning");
                    }
                }
            });
        }
    });
};
