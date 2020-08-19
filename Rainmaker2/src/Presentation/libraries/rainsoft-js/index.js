"use strict";
var __createBinding = (this && this.__createBinding) || (Object.create ? (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    Object.defineProperty(o, k2, { enumerable: true, get: function() { return m[k]; } });
}) : (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    o[k2] = m[k];
}));
exports.__esModule = true;
var helpers_1 = require("./helpers");
__createBinding(exports, helpers_1, "MaskPhone");
__createBinding(exports, helpers_1, "UnMaskPhone");
__createBinding(exports, helpers_1, "FormatAmountByCountry");
__createBinding(exports, helpers_1, "toTitleCase");
var httpService_1 = require("./httpService");
__createBinding(exports, httpService_1, "Http");
var fileUploadHelpers_1 = require("./fileUploadHelpers");
__createBinding(exports, fileUploadHelpers_1, "GetActualMimeType");
__createBinding(exports, fileUploadHelpers_1, "RemoveSpecialChars");
__createBinding(exports, fileUploadHelpers_1, "GetFileSize");
__createBinding(exports, fileUploadHelpers_1, "IsSizeAllowed");
__createBinding(exports, fileUploadHelpers_1, "RemoveDefaultExt");
__createBinding(exports, fileUploadHelpers_1, "SortByDate");
var signalR_1 = require("./signalR");
__createBinding(exports, signalR_1, "SignalRHub");
