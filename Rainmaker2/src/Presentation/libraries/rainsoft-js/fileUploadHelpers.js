"use strict";
exports.__esModule = true;
exports.SortByDate = exports.RemoveDefaultExt = exports.IsSizeAllowed = exports.GetFileSize = exports.RemoveSpecialChars = exports.GetActualMimeType = void 0;
var PNG = {
    hex: "89504E47",
    type: "image/png"
};
var GIF = {
    hex: "47494638",
    type: "image/gif"
};
var PDF = {
    hex: "25504446",
    type: "application/pdf"
};
var JPG = {
    hex: "FFD8FFE0",
    type: "image/jpg"
};
var JPEG = {
    hex: ["FFD8FFDB", "FFD8FFE1", "FFD8FFE2", "FFD8FFE3", "FFD8FFE8"],
    type: "image/jpeg"
};
var ZIP = {
    hex: "504B0304",
    type: "application/zip"
};
var UNKNOWN = {
    type: "Unknown filetype"
};
var getMimetype = function (signature) {
    switch (signature) {
        case PNG.hex:
            return PNG.type;
        case GIF.hex:
            return GIF.type;
        case PDF.hex:
            return PDF.type;
        case JPG.hex:
            return JPG.type;
        case JPEG.hex[0]:
        case JPEG.hex[1]:
        case JPEG.hex[2]:
        case JPEG.hex[3]:
        case JPEG.hex[4]:
        case JPEG.hex[5]:
            return JPEG.type;
        case ZIP.hex:
            return ZIP.type;
        default:
            return UNKNOWN.type;
    }
};
exports.GetActualMimeType = function (file) {
    return new Promise(function (resolve, reject) {
        var filereader = new FileReader();
        var mimeType = "";
        filereader.onloadend = function (e) {
            try {
                if (e.target.readyState === FileReader.DONE) {
                    var uint = new Uint8Array(e.target.result);
                    var bytes_1 = [];
                    uint.forEach(function (byte) {
                        if (byte) {
                            bytes_1.push(byte.toString(16));
                        }
                    });
                    var hex = bytes_1.join("").toUpperCase();
                    mimeType = getMimetype(hex);
                    console.log(mimeType);
                    resolve(mimeType);
                }
            }
            catch (error) {
                reject(error);
            }
        };
        var blob = file.slice(0, 4);
        filereader.readAsArrayBuffer(blob);
    });
};
exports.RemoveSpecialChars = function (text) {
    return text.replace(/[`~!@#$%^&*()_|+\=?;:'",.<>\{\}\[\]\\\/]/gi, "");
};
exports.GetFileSize = function (file) {
    var _a;
    var size = file.size || ((_a = file.file) === null || _a === void 0 ? void 0 : _a.size);
    if (size) {
        var inKbs = size / 1000;
        if (inKbs > 1000) {
            return (inKbs / 1000).toFixed(2) + "mb(s)";
        }
        return inKbs.toFixed(2) + "kb(s)";
    }
    return 0 + "kbs";
};
exports.IsSizeAllowed = function (file, allowedSize) {
    if (!file)
        return null;
    if (file.size / 1000 / 1000 < allowedSize) {
        return true;
    }
    return false;
};
exports.RemoveDefaultExt = function (fileName) {
    var splitData = fileName.split(".");
    var onlyName = "";
    for (var i = 0; i < splitData.length - 1; i++) {
        if (i != splitData.length - 2)
            onlyName += splitData[i] + ".";
        else
            onlyName += splitData[i];
    }
    return onlyName != "" ? onlyName : fileName;
};
exports.SortByDate = function (array, dateFieldName) {
    return array.sort(function (a, b) {
        var first = new Date(a[dateFieldName]);
        var second = new Date(b[dateFieldName]);
        return first > second ? -1 : first < second ? 1 : 0;
    });
};
