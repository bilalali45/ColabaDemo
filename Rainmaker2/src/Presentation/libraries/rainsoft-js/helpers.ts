
const addAmountSeperator = (amount: string, currency: string) => {
    let counter = 0;

    return amount.split('').reverse().map((n, i) => {
        if (counter === 2 && i !== amount.length - 1) {
            counter = 0;
            switch (currency) {
                case 'US':
                    return `,${n}`
                case 'BRL':
                    return `.${n}`

                default:
                    break;
            }
        }
        counter++;
        return n;
    }).reverse().join('');
}
export const MaskPhone = (number: number) => {
    return String(number).split('').map((n, i) => {
        if (i === 0) {
            return `(${n}`
        }
        if (i === 2) {
            return `${n}) `
        }

        if (i === 5) {
            return `${n}-`
        }

        return n;
    }).join('');
};

export const UnMaskPhone = (formattedNumber: string) => {
	return formattedNumber.split('').filter((n: string) => {
        if (!isNaN(parseInt(n)) && n !== ' ') {
            return n
        }
    }).join('');
}

export const FormatAmountByCountry = (amount: number) =>                                                       {
    let strAmount = String(amount);
    let amountSplitByPoint = strAmount.split('.');
    if (amountSplitByPoint.length > 2) {
        return;
    }

    let seperatorAdded = addAmountSeperator(amountSplitByPoint[0].toString(), 'US');

    return (() => {
            if (amountSplitByPoint[1]) {
                return `${seperatorAdded}.${amountSplitByPoint[1]}`
            }
            return `${seperatorAdded}`
        })();
}

export const toTitleCase = (str: string | undefined) => {  
        return str.toLowerCase().replace(/([^a-z])([a-z])(?=[a-z]{2})|^([a-z])/g, function (_, g1, g2, g3) {
          return (typeof g1 === 'undefined') ? g3.toUpperCase() : g1 + g2.toUpperCase();
        });   
}
    
  



