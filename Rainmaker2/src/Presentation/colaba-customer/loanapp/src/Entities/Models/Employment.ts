export type CurrentEmploymentDetails = {
    BorrowerId: number,
    LoanApplicationId: number,
    EmploymentInfo: EmployerInfo,
    EmployerAddress: EmployerOfficeAddress,
    WayOfIncome: WayOfIncome,
    EmploymentOtherIncomes: EmploymentOtherIncomes[],
    State: string
}

export type EmployerInfo = {
    EmployerName: string,
    JobTitle: string,
    StartDate: string,
    YearsInProfession: number,
    EmployerPhoneNumber: string,
    EmployedByFamilyOrParty: boolean,
    OwnershipInterest: number,
    HasOwnershipInterest?:boolean,
    IncomeInfoId: number,
};



export type EmployerOfficeAddress = {
    StreetAddress: string,
    UnitNo: string,
    CityId: number | null,
    CityName: string,
    CountryId: number,
    StateId: number,
    StateName: string,
    ZipCode: string
}

export type WayOfIncome = {
    IsPaidByMonthlySalary: boolean,
    EmployerAnnualSalary: number,
    HourlyRate: number,
    HoursPerWeek: number
}

export type EmploymentOtherIncomes = {
    IncomeTypeId: number,
    AnnualIncome: number,
    incomeTypeId?: number,
    annualIncome?: number
}

export type OtherDefaultIncomeTypes = {
    id: number,
    name: string,
    displayName: string
}