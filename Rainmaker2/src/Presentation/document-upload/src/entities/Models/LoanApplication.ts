
export class LoanApplication {

    public loanPurpose: string;
    public propertyType: string;
    public propertyAddress: string;
    public loanAmount: number

    constructor(loanPurpose: string, propertyType: string, propertyAddress: string, loanAmount: number) {
        this.loanPurpose = loanPurpose;
        this.propertyType = propertyType;
        this.propertyAddress = propertyAddress;
        this.loanAmount = loanAmount;
    }

    get amount() {
        return `${this.loanAmount}`
    }

    static addAmountSeperator(amount: string, currency: string) {
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

    static formatAmountByCountry(amount: number) {
        let strAmount = String(amount);
        let amountSplitByPoint = strAmount.split('.');
        if (amountSplitByPoint.length > 2) {
            return;
        }

        let seperatorAdded = this.addAmountSeperator(amountSplitByPoint[0].toString(), 'US');

        return {
            US: () => {
                if (amountSplitByPoint[1]) {
                    return `$${seperatorAdded}.${amountSplitByPoint[1]}`
                }
                return `$${seperatorAdded}`
            },
            BRL: () => `R$${this.addAmountSeperator(strAmount, 'BRL')}`

        }
    }
}