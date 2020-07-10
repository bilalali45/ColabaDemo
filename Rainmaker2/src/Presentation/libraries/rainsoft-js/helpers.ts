//import * as moment from 'moment';


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
                return `$${seperatorAdded}.${amountSplitByPoint[1]}`
            }
            return `$${seperatorAdded}`
        })();
}

export const toTitleCase = (str: string | undefined) => {
  
    if (str) {
      let sentence = str?.toLowerCase().split(" ");
      for (var i = 0; i < sentence.length; i++) {
        sentence[i] = sentence[i][0]?.toUpperCase() + sentence[i]?.slice(1);
      }
      return sentence.join(" ");
    }else {
      console.log(str);
    }
    return "";
  }
  
// export const DateFormatWithMoment = (date: string,shortFormat: boolean = false) => {
//     const formatString = shortFormat
//       ? "MMM DD, YYYY hh:mm A"
//       : "MMMM DD, YYYY hh:mm A";
  
//     return moment(date).format(formatString);
// };
  


