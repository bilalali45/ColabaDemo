"use strict";
var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
exports.__esModule = true;
exports.Http = void 0;
var axios_1 = require("axios");
var Http = /** @class */ (function () {
    function Http() {
        this.baseUrl = '';
        this.auth = '';
        this.methods = {
            GET: 'GET',
            POST: 'POST',
            PUT: 'PUT',
            DELETE: 'DELETE'
        };
        if (!Http.instance) {
            Http.instance = this;
        }
        else {
            return Http.instance;
        }
    }
    Http.prototype.get = function (url) {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                return [2 /*return*/, this.createRequest(this.methods.GET, url)];
            });
        });
    };
    Http.prototype.post = function (url, data) {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                return [2 /*return*/, this.createRequest(this.methods.POST, url, data)];
            });
        });
    };
    Http.prototype.put = function (url, data) {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                return [2 /*return*/, this.createRequest(this.methods.PUT, url, data)];
            });
        });
    };
    Http.prototype["delete"] = function (url) {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                return [2 /*return*/, this.createRequest(this.methods.DELETE, url)];
            });
        });
    };
    Http.prototype.fetch = function (config, headers) {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                return [2 /*return*/, axios_1["default"].request(__assign(__assign({}, config), { headers: headers }))];
            });
        });
    };
    Http.prototype.setBaseUrl = function (baseUrl) {
        this.baseUrl = baseUrl;
    };
    Http.prototype.setAuth = function (auth) {
        this.auth = auth;
    };
    Http.prototype.createUrl = function (baseUrl, url) {
        return "" + baseUrl + url;
    };
    Http.prototype.createRequest = function (reqType, url, data) {
        var _a, _b, _c, _d, _e, _f;
        return __awaiter(this, void 0, void 0, function () {
            var res, error_1;
            return __generator(this, function (_g) {
                switch (_g.label) {
                    case 0:
                        _g.trys.push([0, 2, , 3]);
                        return [4 /*yield*/, axios_1["default"].request(this.getFonfig(reqType, url, data))];
                    case 1:
                        res = _g.sent();
                        return [2 /*return*/, res];
                    case 2:
                        error_1 = _g.sent();
                        if (((_b = (_a = error_1 === null || error_1 === void 0 ? void 0 : error_1.response) === null || _a === void 0 ? void 0 : _a.data) === null || _b === void 0 ? void 0 : _b.name) === 'TokenExpiredError'
                            || ((_d = (_c = error_1 === null || error_1 === void 0 ? void 0 : error_1.response) === null || _c === void 0 ? void 0 : _c.data) === null || _d === void 0 ? void 0 : _d.name) === 'JsonWebTokenError'
                            || ((_e = error_1 === null || error_1 === void 0 ? void 0 : error_1.response) === null || _e === void 0 ? void 0 : _e.data) === 'Could not login'
                            || ((_f = error_1 === null || error_1 === void 0 ? void 0 : error_1.response) === null || _f === void 0 ? void 0 : _f.status) === 401) {
                        }
                        return [2 /*return*/, new Promise(function (_, reject) {
                                reject(error_1);
                            })];
                    case 3: return [2 /*return*/];
                }
            });
        });
    };
    Http.prototype.getFonfig = function (method, url, data) {
        var completeUrl = this.createUrl(this.baseUrl, url);
        var headers = {};
        //let auth = Auth.getAuth();
        if (this.auth && (!url.includes('login') || !url.includes('authorize'))) {
            headers['Authorization'] = "Bearer " + this.auth;
        }
        var config = {
            method: method,
            url: completeUrl,
            headers: headers
        };
        if (data) {
            config.data = data;
        }
        return config;
    };
    Http.instance = null;
    return Http;
}());
exports.Http = Http;
