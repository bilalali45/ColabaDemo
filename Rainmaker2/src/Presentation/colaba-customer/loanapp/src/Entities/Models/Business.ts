export class CurrentBusinessDetails {
    id? :number
    loanApplicationId: number
    borrowerId: number
    incomeTypeId: number
    businessName: string
    businessPhone: string
    startDate: string
    jobTitle: string
    ownershipPercentage: number
    annualIncome: number
    State: string|null
    address: BusinessAddressProto

    constructor(loanApplicationId: number,borrowerId: number,incomeTypeId: number,businessName: string,businessPhone: string,startDate: string,jobTitle: string,ownershipPercentage: number,annualIncome: number,State: string|null,address: BusinessAddressProto,id?:  number) {
        this.id = id;
        this.loanApplicationId = loanApplicationId;
        this.borrowerId = borrowerId
        this.incomeTypeId = incomeTypeId
        this.businessName = businessName
        this.businessPhone = businessPhone
        this.startDate = startDate
        this.jobTitle = jobTitle
        this.ownershipPercentage = ownershipPercentage
        this.annualIncome = annualIncome
        this.State = State;
        this.address = address;
    }

}


export class BusinessAddressProto  {
    street? :string
    unit? :number
    city? :string
    stateId? :number
    zipCode? :number
    countryId? :number
    countryName? :string
    stateName? :string
    constructor(street? :string,unit? :number,city? :string,stateId? :number,zipCode? :number,countryId? :number,countryName? :string,stateName? :string){
        this.street = street;
        this.unit = unit;
        this.city = city;
        this.stateId = stateId;
        this.zipCode = zipCode ;
        this.countryId = countryId;
        this.countryName = countryName;
        this.stateName = stateName;
    }
}




