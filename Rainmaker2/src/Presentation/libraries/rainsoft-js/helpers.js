"use strict";
//import * as moment from 'moment';
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
    }).join('');
};
exports.FormatAmountByCountry = function (amount) {
    var strAmount = String(amount);
    var amountSplitByPoint = strAmount.split('.');
    if (amountSplitByPoint.length > 2) {
        return;
    }
    var seperatorAdded = addAmountSeperator(amountSplitByPoint[0].toString(), 'US');
    return (function () {
        if (amountSplitByPoint[1]) {
            return "$" + seperatorAdded + "." + amountSplitByPoint[1];
        }
        return "$" + seperatorAdded;
    })();
};
exports.toTitleCase = function (str) {
    var _a, _b;
    if (str) {
        var sentence = str === null || str === void 0 ? void 0 : str.toLowerCase().split(" ");
        for (var i = 0; i < sentence.length; i++) {
            sentence[i] = ((_a = sentence[i][0]) === null || _a === void 0 ? void 0 : _a.toUpperCase()) + ((_b = sentence[i]) === null || _b === void 0 ? void 0 : _b.slice(1));
        }
        return sentence.join(" ");
    }
    else {
        console.log(str);
    }
    return "";
};
// export const DateFormatWithMoment = (date: string,shortFormat: boolean = false) => {
//     const formatString = shortFormat
//       ? "MMM DD, YYYY hh:mm A"
//       : "MMMM DD, YYYY hh:mm A";
//     return moment(date).format(formatString);
// };
