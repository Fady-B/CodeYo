var lat;
var long;
var LoginAction = function () {
    //var Recaptcha = grecaptcha.getResponse();

    //if (Recaptcha === '') {
    //    toastr.error('ReCaptcha Virification Is Failed');
    //    return;
    //}
    //else {

    //}
    debugger
    if (!$("#frmLogin").valid()) {
        return;
    }
    $('#Latitude').val(lat);
    $('#Longitude').val(long);

    var _frmLogin = $("#frmLogin").serialize();

    _frmLogin.Email =
        $("#signin").LoadingOverlay("show", {
            background: "transparent"
        });
    $("#signin").LoadingOverlay("show");
    debugger
    $.ajax({
        type: "POST",
        url: "/Account/Login",
        data: _frmLogin,
        success: function (Return) {
            //console.log(result); result: IsLockedOut: false, IsNotAllowed: false, RequiresTwoFactor: false, Succeeded: true
            debugger
            if (Return.IsSuccess) {
                toastr.success(Return.AlertMessage);
                location.href = "/Home/Index";
            }
            else {
                toastr.error(Return.AlertMessage);
                $("#signin").LoadingOverlay("hide", true);
            }

        },
        error: function (errormessage) {
            $("#btnUserLogin").LoadingOverlay("hide", true);
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });
}