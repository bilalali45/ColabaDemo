
export class LoanApplication {

    public loanPurpose?: string;
    public propertyType?: string;
    public propertyAddress?: string;
    public loanAmount?: number;
    public addressName?: string;
    public countyName?: string;
    public stateName?: string;

    constructor(loanPurpose?: string, propertyType?: string, propertyAddress?: string, loanAmount?: number, address?: string, county?: string, state?: string) {
        this.loanPurpose = loanPurpose;
        this.propertyType = propertyType;
        this.propertyAddress = propertyAddress;
        this.loanAmount = loanAmount;
        this.addressName = address;
        this.countyName = county;
        this.stateName = state;
    }

    get amount() {
        return `${LoanApplication.formatAmountByCountry(this.loanAmount)?.US()}`
    }

    public fromJson(json: LoanApplication) {
        this.loanPurpose = json.loanPurpose;
        this.propertyType = json.propertyType;
        this.propertyAddress = json.propertyAddress;
        this.loanAmount = json.loanAmount;
        this.addressName = json.addressName;
        this.countyName = json.countyName;
        this.stateName = json.stateName;
        return this;
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

    static formatAmountByCountry(amount?: number) {
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