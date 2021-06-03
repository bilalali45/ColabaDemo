export interface SelfIncomeAddress {
    street: string,
    unit: string,
    city: string,
    stateId: number,
    zipCode: number,
    countryId: number,
    countryName: string,
    stateName: string
}


export class SelfInome {

    constructor(
        public loanApplicationId: number,
        public id: number | null,
        public borrowerId: number,
        public businessName: string,
        public businessPhone: number,
        public startDate: string,
        public jobTitle: string| null,
        public annualIncome: number,
        public state: string,
    ) {}
}