export class BorrowerBasicInfo {
    public loanApplicationId?: number;
    public id?: number;
    public firstName?: string;
    public lastName?: string;
    public middleName?: string;
    public suffix?: string;
    public email?: string;
    public homePhone?: string;
    public workPhone?: string;
    public workExt?: string;
    public cellPhone?: string;
    public state?: string;

    constructor(
        loanApplicationId?: number, 
        id?: number, 
        firstName?: string, 
        lastName?: string,
        middleName?: string, 
        suffix?: string, 
        email?: string,
        homePhone?: string,
        workPhone?: string,
        workExt?: string,
        cellPhone?: string,
        state?: string
        ){
         
            this.loanApplicationId = loanApplicationId;
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.middleName = middleName;
            this.suffix = suffix;
            this.email = email;
            this.homePhone = homePhone;
            this.workPhone = workPhone
            this.workExt = workExt;
            this.cellPhone = cellPhone;
            this.state = state;
    }
}