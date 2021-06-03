export interface LoanApplicationType  {
    id: number
    loanPurpose: string
    loanAmount: number
    countryName: string
    unitNumber: string
    status?: string
    pendingTasks?: number
    stateName: string
    countyName: string
    cityName: string
    streetAddress: string
    zipCode: string,
    mileStone: string,
    mileStoneId: number
}