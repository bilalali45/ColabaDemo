
export class LoanApplication {

    public loanPurpose?: string;
    public propertyType?: string;
    public propertyAddress?: string;
    public loanAmount?: number;
    public countryName?: string;
    public countyName?: string;
    public cityName? : string;
    public stateName?: string;
    public streetAddress?: string;
    public zipCode? : string;
    public unitNumber? : any

    constructor(loanPurpose?: string, propertyType?: string, propertyAddress?: string, loanAmount?: number, country?: string, county?: string, city?: string, state?: string, street? : string, zipCode? : string, unitNumber? : any) {
        this.loanPurpose = loanPurpose;
        this.propertyType = propertyType;
        this.propertyAddress = propertyAddress;
        this.loanAmount = loanAmount;
        this.countryName = country;
        this.countyName = county;
        this.cityName = city;
        this.stateName = state;
        this.streetAddress = street;
        this.zipCode = zipCode;
        this.unitNumber = unitNumber;
    }

    get amount() {
        if(!this.loanAmount)
         return undefined;
        return `${LoanApplication.formatAmountByCountry(this.loanAmount)?.US()}`
    }

    public fromJson(json: LoanApplication) {
        this.loanPurpose = json.loanPurpose;
        this.propertyType = json.propertyType;
        this.propertyAddress = json.propertyAddress;
        this.loanAmount = json.loanAmount;
        this.countryName = json.countryName;
        this.countyName = json.countyName;
        this.cityName = json.cityName;
        this.stateName = json.stateName ? json.stateName : '';
        this.streetAddress = json.streetAddress;
        this.zipCode = json.zipCode ? json.zipCode : '';
        this.unitNumber = json.unitNumber;
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
                    return `${seperatorAdded}.${amountSplitByPoint[1]}`
                }
                return `${seperatorAdded}`
            },
            BRL: () => `R$${this.addAmountSeperator(strAmount, 'BRL')}`

        }
    }
}