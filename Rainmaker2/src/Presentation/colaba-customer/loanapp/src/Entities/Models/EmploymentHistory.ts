import { EmployerOfficeAddress } from "./Employment";

export type EmploymentHistoryDetails = {
    BorrowerId: number,
    LoanApplicationId: number,
    EmploymentInfo: EmployerHistoryInfo,
    EmployerAddress: EmployerOfficeAddress,
    WayOfIncome: WayOfHistoryIncome,
    State: string
}

export type EmployerHistoryInfo = {
    EmployerName: string,
    JobTitle: string,
    StartDate: string,
    EndDate: string,
    YearsInProfession: number,
    EmployerPhoneNumber: string,
    HasOwnershipInterest?: boolean,
    EmployedByFamilyOrParty: boolean,
    OwnershipInterest: number | null,
    IncomeInfoId: number,
};

export type WayOfHistoryIncome = {
    EmployerAnnualSalary: number,
}

export type BorrowersEmploymentHistoryList = {
    loanApplicationId: number,
    requiresHistory: boolean,
    borrowerEmploymentHistory: BorrowerEmploymentHistory[]
}

export type BorrowerEmploymentHistory = {
    borrowerId: number,
    borrowerName: string,
    employmentHistory: EmploymentHistory[]
}

export type EmploymentHistory = {
    incomeInfoId: number,
    employerName: string,
    startDate: string,
    endDate: string,
    isCurrentEmployment: boolean
}

export type BorrowerIncome  = {
    loanApplicationId:number,
    borrowerId: number,
    borrowerName:string,
    ownType: BorrowerOwnType,
    borrowerIncomes: Income[]    
}

export type BorrowerOwnType = {
    ownTypeId: number,
    name: string,
    ownTypeDisplayName: string
}

export type Income = {
        incomeInfoId: number,
        employerName: string,
        startDate: string,
        endDate: string,
        isCurrentEmployment: boolean,
        incomeType: IncomeType,
        employmentCategory: EmploymentCategory
}

export type IncomeType = {
    incomeTypeId: number,
    name: string,
    displayName: string
}

export type EmploymentCategory = {
    categoryDisplayName:string,
    categoryId:number, 
    categoryName:string
}