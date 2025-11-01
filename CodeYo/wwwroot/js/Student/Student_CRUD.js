var AddEdit = function (id) {

    var url = "/Students/AddEdit?Id=" + id;
    if (id != "0") {
        $('#titleExtraBigModal').html("Edit Teacher");
    }
    else {
        $('#titleExtraBigModal').html("Add Teacher");
    }

    loadExtraBigModal(url);
};

var Details = function (id) {
    var url = "/Students/Details?Id=" + id;
    $('#titleBigModal').html("Student Details");
    loadBigModal(url);
};

var Save = function () {
    if (!$("#frmStudent").valid()) {
        return;
    }
    var _frmStudent = $("#frmStudent").serialize();

    $("#btnSave").html("Saving...");
    $('#btnSave').attr('disabled', 'disabled');
    $.ajax({
        type: "POST",
        url: "/Students/AddEdit",
        data: _frmStudent,
        success: function (result) {
            $("#btnSave").html("Save");
            $('#btnSave').removeAttr('disabled');
            if (result.IsSuccess) {
                Swal.fire({
                    title: result.AlertMessage,
                    icon: "success"
                }).then(function () {
                    document.getElementById("btnClose").click();
                    $('#tblStudents').DataTable().ajax.reload();
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
        title: 'Do you want to delete this student?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "POST",
                url: "/Students/Delete?Id=" + id,
                success: function (result) {
                    if (result.IsSuccess) {
                        Swal.fire({
                            title: result.AlertMessage,
                            icon: "info",
                            didClose: () => {
                                $('#tblStudents').DataTable().ajax.reload();
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
