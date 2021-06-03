"use strict";
exports.__esModule = true;
exports.toTitleCase = exports.FormatAmountByCountry = exports.UnMaskPhone = exports.MaskPhone = void 0;
var addAmountSeperator = function (amount, currency) {
    var counter = 0;
    return amount.split('').reverse().map(function (n, i) {
        if (counter === 2 && i !== amount.length - 1) {
            counter = 0;
            switch (currency) {
                case 'US':
                    return "," + n;
                case 'BRL':
                    return "." + n;
                default:
                    break;
            }
        }
        counter++;
        return n;
    }).reverse().join('');
};
exports.MaskPhone = function (number) {
    return String(number).split('').map(function (n, i) {
        if (i === 0) {
            return "(" + n;
        }
        if (i === 2) {
            return n + ") ";
        }
        if (i === 5) {
            return n + "-";
        }
        return n;
    }).join('');
};
exports.UnMaskPhone = function (formattedNumber) {
    return formattedNumber.split('').filter(function (n) {
        if (!isNaN(parseInt(n)) && n !== ' ') {
            return n;
        }
        return null;
    }).join('');
};
exports.FormatAmountByCountry = function (amount) {
    var strAmount = String(amount);
    var amountSplitByPoint = strAmount.split('.');
    if (amountSplitByPoint.length > 2) {
        return;
    }
    var seperatorAdded = addAmountSeperator(String((amountSplitByPoint === null || amountSplitByPoint === void 0 ? void 0 : amountSplitByPoint.length) > 0 ? amountSplitByPoint[0].toString() : ""), 'US');
    return (function () {
        if (amountSplitByPoint && amountSplitByPoint[1]) {
            return seperatorAdded + "." + amountSplitByPoint[1];
        }
        return "" + seperatorAdded;
    })();
};
exports.toTitleCase = function (str) {
    if (!str) {
        return "";
    }
    return str === null || str === void 0 ? void 0 : str.toLowerCase().replace(/([^a-z])([a-z])(?=[a-z]{2})|^([a-z])/g, function (_, g1, g2, g3) {
        return (typeof g1 === 'undefined') ? g3.toUpperCase() : g1 + g2.toUpperCase();
    });
};
