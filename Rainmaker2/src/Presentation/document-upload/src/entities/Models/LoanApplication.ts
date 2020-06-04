
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
}