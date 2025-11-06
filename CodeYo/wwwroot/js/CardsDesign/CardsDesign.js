
var ResetDesign = function () {
    SwalSimpleAlert("Warning!... The changes you made (if any) will be lost", "warning");
}

var SaveDesign = function () {
    SwalSimpleAlert("Warning!... The changes you made (if any) will be lost", "success");
}

var DiplayQrTest = function (Card) {
    let previewDiv, checkboxId;
    if (Card === "Front") {
        previewDiv = document.getElementById('FrontCardImagePreview');
        checkboxId = 'ShowFrontQRCode';
    } else if (Card === "Back") {
        previewDiv = document.getElementById('BackCardImagePreview');
        checkboxId = 'ShowBackQRCode';
    } else {
        return;
    }
    const checkbox = document.getElementById(checkboxId);
    if (!previewDiv || !checkbox) return;

    let qrImg = previewDiv.querySelector('.qr-overlay');
    if (checkbox.checked) {
        if (!qrImg) {
            qrImg = new Image();
            qrImg.src = "/images/TestQr.jpg";
            qrImg.alt = "QR Code";
            qrImg.classList.add("qr-overlay");
            previewDiv.appendChild(qrImg);
        }
        qrImg.style.display = "block";
    } else {
        if (qrImg) {
            qrImg.style.display = "none";
        }
    }
};

document.getElementById('FrontCardFileInput').addEventListener('change', function (e) {
    const previewDiv = document.getElementById('FrontCardImagePreview');
    const file = e.target.files[0];
    if (!file) {
        previewDiv.innerHTML = "";
        document.getElementById("MainFrontCardContainer").style.display = "none";
        return;
    }

    if (!file.type.startsWith("image/")) {
        previewDiv.innerHTML = "";
        document.getElementById("MainFrontCardContainer").style.display = "none";
        alert("Please upload an image file only.");
        return;
    }

    document.getElementById("MainFrontCardContainer").style.display = "flex";

    const widthInput = document.getElementById('FrontCardWidthInput');
    const heightInput = document.getElementById('FrontCardHeightInput');

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
            QrImg.classList.add("qr-overlay");

            var _Checckbox = document.getElementById("ShowFrontQRCode");
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
        };
    };
    reader.readAsDataURL(file);
});


document.getElementById('BackCardFileInput').addEventListener('change', function (e) {
    const previewDiv = document.getElementById('BackCardImagePreview');
    const file = e.target.files[0];
    if (!file) {
        previewDiv.innerHTML = "";
        document.getElementById("MainBackCardContainer").style.display = "none";
        return;
    }

    if (!file.type.startsWith("image/")) {
        previewDiv.innerHTML = "";
        document.getElementById("MainBackCardContainer").style.display = "none";
        alert("Please upload an image file only.");
        return;
    }

    document.getElementById("MainBackCardContainer").style.display = "flex";

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