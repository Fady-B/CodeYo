
var ResetDesign = function () {
    SwalSimpleAlert("Warning!... The changes you made (if any) will be lost", "warning");
}

var SaveDesign = function () {
    debugger
    if (!FieldValidation("#TeachersDdl")) {
        toastr.warning("Please select a teacher before proceeding!", "warning");
        return;
    }

    $("#btnSaveDesign").html("Please Wait...");
    $('#btnSaveDesign').attr('disabled', 'disabled');


    var _FormData = new FormData()
    _FormData.append('TeacherId', $("#TeachersDdl").val())
    var FrontFormFile = $('#FrontCardFileInput')[0].files[0];
    if (FrontFormFile == undefined) {
        SwalSimpleAlert("Please select the front image card", "warning");
        return;
    }

    _FormData.append('FrontCardFile', FrontFormFile)


    var BackFormFile = $('#BackCardFileInput')[0].files[0];
    if (BackFormFile != undefined) {
        _FormData.append('BackCardFile', BackFormFile)
    }

    _FormData.append('CardWidth', $("#FrontCardWidthInput").val())
    _FormData.append('CardHeight', $("#FrontCardHeightInput").val())

    _FormData.append('IsQRInFrontCard', $("#ShowFrontQRCode").is(":checked"))

    if ($("#ShowFrontQRCode").is(":checked")) {
        _FormData.append('QRFrontSizePercent', $("#QRFrontSizeInput").val())
        _FormData.append('QRFrontTopPixels', $("#QRFrontTopPixelsInput").val())
        _FormData.append('QRFrontLeftPixels', $("#QRFrontLeftPixelsInput").val())
    }

    _FormData.append('IsQRInBackCard', $("#ShowBackQRCode").is(":checked"))

    if ($("#ShowBackQRCode").is(":checked")) {
        _FormData.append('QRBackSizePixels', $("#QRBackSizeInput").val())
        _FormData.append('QRBackTopPixels', $("#QRBackTopPixelsInput").val())
        _FormData.append('QRBackLeftPixels', $("#QRBackLeftPixelsInput").val())
    }

    const FrontContainer = document.getElementById('FrontCardImagePreview').innerHTML;
    _FormData.append('FrontHtmlDataContent', FrontContainer)

    const BackContainer = document.getElementById('FrontCardImagePreview').innerHTML;
    _FormData.append('BackHtmlDataContent', BackContainer)
    debugger
    $.ajax({
        type: "POST",
        url: "/CardsDesign/SaveCardDesign",
        data: _FormData,
        processData: false,
        contentType: false,
        success: function (result) {
            debugger
            if (result.IsSuccess) {
                Swal.fire({
                    title: result.AlertMessage,
                    icon: "success"
                }).then(function () {
                    //document.getElementById("btnClose").click();

                });
            }
            $("#btnSaveDesign").html("Save Design");
            $('#btnSaveDesign').removeAttr('disabled');
        },
        error: function (errormessage) {
            SwalSimpleAlert(errormessage.responseText, "warning");
            $("#btnSaveDesign").html("Save Design");
            $('#btnSaveDesign').removeAttr('disabled');
        }
    });


    SwalSimpleAlert("Warning!... The changes you made (if any) will be lost", "success");
}

function FrontQrSizeInputFun() {

    var _QRFrontSizeInput = +parseFloat($("#QRFrontSizeInput").val());
    var ShowFrontQRCode = $("#ShowFrontQRCode").is(":checked");

    if (_QRFrontSizeInput == 0 && ShowFrontQRCode) {
        $("#FrontQRCodeImage").css("width", "30%");
    } else if (ShowFrontQRCode && _QRFrontSizeInput > 0) {
        $("#FrontQRCodeImage").css("width", _QRFrontSizeInput + "%");
    }
    else if (!ShowFrontQRCode) {
        $('#QRFrontSizeInput').off('input', FrontQrSizeInputFun);
        $('#QRFrontSizeInput').removeAttr("oninput")
    }
    return;
}
function FrontQrPositionTopInputFun() {
    var _QRFrontTopPixelsInput = +parseFloat($("#QRFrontTopPixelsInput").val());
    var ShowFrontQRCode = $("#ShowFrontQRCode").is(":checked");

    if (_QRFrontTopPixelsInput == 0 && ShowFrontQRCode) {
        $("#FrontQRCodeImage").css("top", "1px");
    } else if (ShowFrontQRCode && _QRFrontTopPixelsInput > 0) {
        $("#FrontQRCodeImage").css("top", _QRFrontTopPixelsInput + "px");
    }
    else if (!ShowFrontQRCode) {
        $('#QRFrontTopPixelsInput').off('input', FrontQrPositionTopInputFun);
        $('#QRFrontTopPixelsInput').removeAttr("oninput")
    }
    return;
}
function FrontQrPositionLeftInputFun() {
    var _QRFrontLeftPixelsInput = +parseFloat($("#QRFrontLeftPixelsInput").val());
    var ShowFrontQRCode = $("#ShowFrontQRCode").is(":checked");

    if (_QRFrontLeftPixelsInput == 0 && ShowFrontQRCode) {
        $("#FrontQRCodeImage").css("left", "1px");
    } else if (ShowFrontQRCode && _QRFrontLeftPixelsInput > 0) {
        $("#FrontQRCodeImage").css("left", _QRFrontLeftPixelsInput + "px");
    }
    else if (!ShowFrontQRCode) {
        $('#QRFrontLeftPixelsInput').off('input', OnLogoPositionLeftInput);
        $('#QRFrontLeftPixelsInput').removeAttr("oninput")
    }
    return;
}

function BackQrSizeInputFun() {

    var _QRBackSizeInput = +parseFloat($("#QRBackSizeInput").val());
    var ShowBackQRCode = $("#ShowBackQRCode").is(":checked");

    if (_QRBackSizeInput == 0 && ShowBackQRCode) {
        $("#BackQRCodeImage").css("width", "30%");
    } else if (ShowBackQRCode && _QRBackSizeInput > 0) {
        $("#BackQRCodeImage").css("width", _QRBackSizeInput + "%");
    }
    else if (!ShowBackQRCode) {
        $('#QRBackSizeInput').off('input', BackQrSizeInputFun);
        $('#QRBackSizeInput').removeAttr("oninput")
    }
    return;
}
function BackQrPositionTopInputFun() {
    var _QRBackTopPixelsInput = +parseFloat($("#QRBackTopPixelsInput").val());
    var ShowBackQRCode = $("#ShowBackQRCode").is(":checked");

    if (_QRBackTopPixelsInput == 0 && ShowBackQRCode) {
        $("#BackQRCodeImage").css("top", "1px");
    } else if (ShowBackQRCode && _QRBackTopPixelsInput > 0) {
        $("#BackQRCodeImage").css("top", _QRBackTopPixelsInput + "px");
    }
    else if (!ShowBackQRCode) {
        $('#QRBackTopPixelsInput').off('input', BackQrPositionTopInputFun);
        $('#QRBackTopPixelsInput').removeAttr("oninput")
    }
    return;
}
function BackQrPositionLeftInputFun() {
    var _QRBackLeftPixelsInput = +parseFloat($("#QRBackLeftPixelsInput").val());
    var ShowBackQRCode = $("#ShowBackQRCode").is(":checked");

    if (_QRBackLeftPixelsInput == 0 && ShowBackQRCode) {
        $("#BackQRCodeImage").css("left", "1px");
    } else if (ShowBackQRCode && _QRBackLeftPixelsInput > 0) {
        $("#BackQRCodeImage").css("left", _QRBackLeftPixelsInput + "px");
    }
    else if (!ShowBackQRCode) {
        $('#QRBackLeftPixelsInput').off('input', OnLogoPositionLeftInput);
        $('#QRBackLeftPixelsInput').removeAttr("oninput")
    }
    return;
}


var DiplayQr = function (Card) {
    let previewDiv, checkboxId, qrId;
    if (Card === "Front") {
        previewDiv = document.getElementById('FrontCardImagePreview');
        checkboxId = 'ShowFrontQRCode';
        qrId = 'FrontQRCodeImage';
        $("#QRFrontSizeInput").attr("oninput", "FrontQrSizeInputFun();");
        $("#QRFrontTopPixelsInput").attr("oninput", "FrontQrPositionTopInputFun();");
        $("#QRFrontLeftPixelsInput").attr("oninput", "FrontQrPositionLeftInputFun();");
    } else if (Card === "Back") {
        previewDiv = document.getElementById('BackCardImagePreview');
        checkboxId = 'ShowBackQRCode';
        qrId = 'BackQRCodeImage';
        $("#QRBackSizeInput").attr("oninput", "BackQrSizeInputFun();");
        $("#QRBackTopPixelsInput").attr("oninput", "BackQrPositionTopInputFun();");
        $("#QRBackLeftPixelsInput").attr("oninput", "BackQrPositionLeftInputFun();");
    } else {
        return;
    }
    const checkbox = document.getElementById(checkboxId);
    if (!previewDiv || !checkbox) return;

    let qrImg = previewDiv.querySelector('.qr-overlay');
    if (checkbox.checked) {
        if (!qrImg) {
            qrImg = new Image();
            qrImg.id = qrId;
            qrImg.src = "/images/TestQr.jpg";
            qrImg.alt = "QR Code";
            qrImg.classList.add("qr-overlay");
            previewDiv.appendChild(qrImg);
        }
        qrImg.style.display = "block";
    } else {
        if (qrImg) {
            qrImg.style.display = "none";
            qrImg.remove();
        }
    }
};

document.getElementById('FrontCardFileInput').addEventListener('change', function (e) {
    var fileval = e.target;
    if (!FieldValidation("#TeachersDdl")) {
        toastr.warning("Please select a teacher before proceeding!", "warning");
        fileval.value = "";
        return;
    }
    const previewDiv = document.getElementById('FrontCardImagePreview');
    const file = fileval.files[0];
    if (!file) {
        previewDiv.innerHTML = "";
        document.getElementById("MainFrontCardContainer").style.display = "none";
        fileval.value = "";
        return;
    }

    if (!file.type.startsWith("image/")) {
        previewDiv.innerHTML = "";
        document.getElementById("MainFrontCardContainer").style.display = "none";
        toastr.info("Please upload an image file only.", "info");
        fileval.value = "";
        return;
    }

    document.getElementById("MainFrontCardContainer").style.display = "flex";

    const loader = document.createElement('div');
    loader.className = 'spinner';
    previewDiv.innerHTML = "";
    previewDiv.appendChild(loader);

    const widthInput = document.getElementById('FrontCardWidthInput');
    const heightInput = document.getElementById('FrontCardHeightInput');

    const reader = new FileReader();
    reader.onload = function (event) {

        const img = new Image();
        img.src = event.target.result;
        img.style.maxWidth = "100%";
        img.style.border = "1px solid #ccc";
        img.id = "FrontCardImage";
        img.onload = async function () {

            previewDiv.innerHTML = "";
            previewDiv.appendChild(img);

            const QrImg = new Image();
            QrImg.src = "/images/TestQr.jpg";
            QrImg.id = "FrontQRCodeImage";
            QrImg.alt = "qr";
            QrImg.classList.add("qr-overlay");

            var _Checckbox = document.getElementById("ShowFrontQRCode");
            if (_Checckbox.checked) {
                QrImg.style.display = "block";
            }
            else {
                QrImg.style.display = "none";
            }

            QrImg.style.position = "absolute"
            QrImg.style.top = "1px"
            QrImg.style.left = "1px"
            QrImg.style.zIndex = "2"
            QrImg.style.borderRadius = "6px"

            previewDiv.appendChild(QrImg);

            const pxWidth = img.naturalWidth;
            const pxHeight = img.naturalHeight;

            const dpi = await getImageDPI(file);
            const dpiX = dpi.x || 96;
            const dpiY = dpi.y || 96;

            const widthCm = (pxWidth * 2.54 / dpiX).toFixed(2);
            const heightCm = (pxHeight * 2.54 / dpiY).toFixed(2);

            widthInput.value = widthCm;
            heightInput.value = heightCm;



        };
    };
    reader.readAsDataURL(file);
});


document.getElementById('BackCardFileInput').addEventListener('change', function (e) {
    var fileval = e.target;
    if (!FieldValidation("#TeachersDdl")) {
        toastr.warning("Please select a teacher before proceeding!", "warning");
        fileval.value = "";
        return;
    }
    const previewDiv = document.getElementById('BackCardImagePreview');
    const file = fileval.files[0];
    if (!file) {
        previewDiv.innerHTML = "";
        document.getElementById("MainBackCardContainer").style.display = "none";
        fileval.value = "";
        return;
    }

    if (!file.type.startsWith("image/")) {
        previewDiv.innerHTML = "";
        document.getElementById("MainBackCardContainer").style.display = "none";
        toastr.info("Please upload an image file only!", "info");
        fileval.value = "";
        return;
    }

    document.getElementById("MainBackCardContainer").style.display = "flex";


    const loader = document.createElement('div');
    loader.className = 'spinner';
    previewDiv.innerHTML = "";
    previewDiv.appendChild(loader);

    const widthInput = document.getElementById('BackCardWidthInput');
    const heightInput = document.getElementById('BackCardHeightInput');

    const reader = new FileReader();
    reader.onload = function (event) {

        const img = new Image();
        img.src = event.target.result;
        img.style.maxWidth = "100%";
        img.style.border = "1px solid #ccc";

        img.onload = async function () {

            previewDiv.innerHTML = "";
            previewDiv.appendChild(img);
            const QrImg = new Image();
            QrImg.src = "/images/TestQr.jpg";
            QrImg.alt = "qr";
            QrImg.id = "BackQRCodeImage";
            QrImg.classList.add("qr-overlay");

            var _Checckbox = document.getElementById("ShowBackQRCode");
            if (_Checckbox.checked) {
                QrImg.style.display = "block";
            }
            else {
                QrImg.style.display = "none";
            }
            previewDiv.appendChild(QrImg);
            const pxWidth = img.naturalWidth;
            const pxHeight = img.naturalHeight;

            const dpi = await getImageDPI(file);
            const dpiX = dpi.x || 96;
            const dpiY = dpi.y || 96;

            const widthCm = (pxWidth * 2.54 / dpiX).toFixed(2);
            const heightCm = (pxHeight * 2.54 / dpiY).toFixed(2);

            widthInput.value = widthCm;
            heightInput.value = heightCm;
            //$("#QRFrontTopPixelsInput").attr("oninput", "QrPositionTopInput");
        };
    };
    reader.readAsDataURL(file);
});

async function getImageDPI(file) {
    return new Promise(resolve => {
        const reader = new FileReader();
        reader.onload = function (e) {
            const buffer = e.target.result;
            const view = new DataView(buffer);
            let xDPI = 96, yDPI = 96;

            try {
                if (file.type === "image/jpeg") {

                    for (let i = 0; i < view.byteLength - 9; i++) {
                        if (
                            view.getUint8(i) === 0x4A && // J
                            view.getUint8(i + 1) === 0x46 && // F
                            view.getUint8(i + 2) === 0x49 && // I
                            view.getUint8(i + 3) === 0x46 && // F
                            view.getUint8(i + 4) === 0x00
                        ) {
                            const units = view.getUint8(i + 7);
                            if (units === 1) { // pixels per inch
                                xDPI = view.getUint16(i + 8);
                                yDPI = view.getUint16(i + 10);
                            } else if (units === 2) { // pixels per cm
                                xDPI = view.getUint16(i + 8) * 2.54;
                                yDPI = view.getUint16(i + 10) * 2.54;
                            }
                            break;
                        }
                    }
                } else if (file.type === "image/png") {

                    // PNGs store DPI as pixels per meter (pHYs chunk)
                    for (let i = 0; i < view.byteLength - 8; i++) {
                        if (
                            view.getUint32(i) === 0x70485973 // pHYs
                        ) {
                            const ppmX = view.getUint32(i + 4);
                            const ppmY = view.getUint32(i + 8);
                            xDPI = ppmX * 0.0254;
                            yDPI = ppmY * 0.0254;
                            break;
                        }
                    }
                }
            } catch (err) {

                console.warn("Failed to read DPI metadata, defaulting to 96:", err);
            }

            if (xDPI < 72 || xDPI > 600) xDPI = 300;
            if (yDPI < 72 || yDPI > 600) yDPI = 300;

            resolve({ x: xDPI, y: yDPI });
        };
        reader.readAsArrayBuffer(file);
    });
}



var DataControlFun = function (dataName, cardFace, btnAction) {
    if (btnAction == "Remove") {
        var existingTextDiv = document.getElementById('overlayText-' + dataName + cardFace);
        if (existingTextDiv) existingTextDiv.remove();

        var RemoveBtn = document.getElementById("Remove" + dataName + cardFace + "Btn");
        var ShowBtn = document.getElementById("Show" + dataName + cardFace + "Btn");
        RemoveBtn.style.display = "none";
        ShowBtn.style.display = "block";
        toastr.info(dataName + " removed from the card!", "info");
    }
    else {
        $.ajax({
            url: '/CardsDesign/GetStudenData',
            type: 'GET',
            data: { Name: dataName },
            success: function (response) {
                if (response.IsSuccess) {
                    DisplayText(dataName, cardFace, response.ReturnData);
                    toastr.success(dataName + " added to the card!", "Success");
                }
                else {

                    Swal.fire({
                        title: 'Enter ' + dataName + ' text example to display on the ' + cardFace + ' card',
                        input: 'text',
                        inputPlaceholder: 'Type the' + dataName + ' ...',
                        showCancelButton: true,
                        confirmButtonText: 'Submit',
                        cancelButtonText: 'Cancel',
                        inputValidator: (value) => {
                            if (!value) {
                                return 'You need to write something!'
                            }
                        }
                    }).then((result) => {
                        if (result.isConfirmed) {
                            DisplayText(dataName, cardFace, result.value);
                            toastr.success(dataName + " added to the card!", "Success");
                        }
                        else {
                            return;
                        }
                    });
                }
            },
            error: function (xhr, status, error) {

                SwalSimpleAlert("Something went wrong.", "error");
            }
        });
    }
}

var DisplayText = function (dataName, cardFace, dataText) {
    var RemoveBtn = document.getElementById("Remove" + dataName + cardFace + "Btn");
    var ShowBtn = document.getElementById("Show" + dataName + cardFace + "Btn");

    var previewDiv = document.getElementById(cardFace + 'CardImagePreview');

    var textDiv = document.createElement('div');
    textDiv.id = 'overlayText-' + dataName + cardFace;
    textDiv.innerText = dataText;

    textDiv.style.position = 'absolute';
    textDiv.style.top = '10px';
    textDiv.style.left = '10px';
    textDiv.style.color = 'white';
    textDiv.style.fontSize = '40%';
    textDiv.style.fontWeight = 'bold';
    textDiv.style.cursor = 'move';
    //textDiv.style.textShadow = '1px 1px 2px black';

    previewDiv.appendChild(textDiv);

    $(textDiv).draggable({
        containment: previewDiv // يضمن أن النص يبقى داخل الصورة
    });

    // ربط التحكم في الحجم واللون
    var sizeSlider = document.getElementById('FontSize' + dataName + cardFace);
    if (sizeSlider) {
        sizeSlider.addEventListener('input', function () {
            textDiv.style.fontSize = this.value + '%';
        });
    }

    var colorPicker = document.getElementById('FontColor' + dataName + cardFace);
    if (colorPicker) {
        colorPicker.addEventListener('input', function () {
            textDiv.style.color = this.value;
        });
    }

    RemoveBtn.style.display = "block";
    ShowBtn.style.display = "none";
}