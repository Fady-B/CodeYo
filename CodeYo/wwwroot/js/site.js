//toastr.options = {
//    "closeButton": true,
//    "debug": false,
//    "newestOnTop": false,
//    "progressBar": false,
//    "positionClass": "toast-top-right",
//    "preventDuplicates": false,
//    "onclick": null,
//    "showDuration": "300",
//    "hideDuration": "1000",
//    "timeOut": "5000",
//    "extendedTimeOut": "1000",
//    "showEasing": "swing",
//    "hideEasing": "linear",
//    "showMethod": "fadeIn",
//    "hideMethod": "fadeOut"
//}

var FieldValidation = function (FieldName) {
    var _FieldName = $(FieldName).val();
    if (_FieldName == "" || _FieldName == null) {
        return false;
    }
    return true;
};

var FieldValidationAlert = function (FieldName, Message, icontype) {
    Swal.fire({
        title: Message,
        icon: icontype,
        didClose: () => {
            $(FieldName).focus();
        }
    });
}

var SwalSimpleAlert = function (Message, icontype) {
    Swal.fire({
        title: Message,
        icon: icontype
    });
}

var loadMediumModal = function (url) {
    $("#MediumModalDiv").load(url, function () {
        $("#MediumModal").modal({
            show: false,
            backdrop: 'static'
        });
        $("#MediumModal").modal("show");
        $("#Name").focus();

    });
};

var loadBigModal = function (url) {
    $("#BigModalDiv").load(url, function () {
        $("#BigModal").modal({
            show: false,
            backdrop: 'static'
        });
        $("#BigModal").modal("show");
        $('#Name').focus();

    });
};

var loadExtraBigModal = function (url) {
    $("#ExtraBigModalDiv").load(url, function () {
        $("#ExtraBigModal").modal({
            show: false,
            backdrop: 'static'
        });
        $("#ExtraBigModal").modal("show");

    });
};

function InitialSelect2(scope) {
    const $scope = scope || $(document);
    const $modal = $scope.closest('.modal');
    const parent = $modal.length ? $modal : $(document.body);

    $scope.find('select.select2').each(function () {

        if ($(this).hasClass("select2-hidden-accessible")) {
            $(this).select2('destroy');
        }
        $(this).select2({
            dropdownParent: parent,
            width: '100%',
            placeholder: '--- SELECT ---',
            allowClear: true
        });
    });
}
function InitialPartialSelect2(Container) {

    const $scope = Container || $(document);
    const parent = $scope.length ? $scope : $(document.body).closest('.modal');
    $scope.find('select.select2').each(function () {

        if ($(this).hasClass("select2-hidden-accessible")) {
            $(this).select2('destroy');
        }
        $(this).select2({
            dropdownParent: parent,
            width: '100%',
            placeholder: '--- SELECT ---',
            allowClear: true
        });
    });
}

var ShowBootstrapAlert = function (AlertType, AlertTitle, AlertMessage) {

    let AlertTypeLowerCase = AlertType.toLowerCase();

    // Build the alert dynamically
    let alertHtml = `
        <div class="alert alert-${AlertTypeLowerCase} alert-dismissible fade" role="alert">
            <strong id="alert-${AlertTypeLowerCase}-title">${AlertTitle}</strong> 
            <span id="alert-${AlertTypeLowerCase}-body">${AlertMessage}</span>
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
    `;

    // Append to container
    let $alert = $(alertHtml).appendTo("#alert-container");

    // Show it
    $alert.addClass("show").fadeIn();

    // Auto-hide after 8s
    setTimeout(function () {
        $alert.removeClass("show").fadeOut(function () {
            $(this).remove(); // remove from DOM
        });
    }, 8000);

    // Close button
    $alert.find(".close").on("click", function () {
        $alert.removeClass("show").fadeOut(function () {
            $(this).remove();
        });
    });

}