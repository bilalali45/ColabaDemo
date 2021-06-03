export type PrimaryPropertyType = {
    id?: number,
    BorrowerId: number,
    LoanApplicationId: number,
    PropertyTypeId?: number,
    RentalIncome?: number,
    State: string
}

export type CurrentPropertyVal = {
    LoanApplicationId: number,
    Id: number,
    PropertyValue: number,
    OwnersDue: number | null,
    IsSelling: boolean | null,
    State: string,
    BorrowerId: number
}

export type HasFirstMortgage = {
    HasFirstMortgage: boolean,
    PropertyTax: number,
    HomeOwnerInsurance: number,
    FloodInsurance:number, 
    LoanApplicationId: number,
    Id: number,
    State: string
}

export type HasSecondMortgage = {
    HasSecondMortgage: boolean,
    LoanApplicationId: number
    Id: number
    State: string
}

export type FirstMortgageValue = {
    Id: number,
    PropertyTax: number,
    PropertyTaxesIncludeinPayment: boolean,
    HomeOwnerInsurance: number | null,
    HomeOwnerInsuranceIncludeinPayment: boolean,
    LoanApplicationId: number,
    FirstMortgagePayment: number | null,
    UnpaidFirstMortgagePayment: number | null,
    HelocCreditLimit: number,
    IsHeloc: boolean,
    FloodInsurance: number,
    FloodInsuranceIncludeinPayment:boolean,
    PaidAtClosing:boolean, 
    State: string
}

export type SecondMortgageValue = {
    LoanApplicationId: number,
    Id: number,
    SecondMortgagePayment: number | null,
    UnpaidSecondMortgagePayment: number | null,
    HelocCreditLimit: number,
    IsHeloc: boolean,
    PaidAtClosing:boolean,
    State: string
}