export class LoanStatus {
    public id?: number;
    public mcuName?: string;
    public statusId?: number;
    public fromStatusId?: number;
    public fromStatus?: string;
    public toStatusId?: number;
    public toStatus?: string;
    public noofDays?: number;
    public recurringTime?: Date;
    public isActive?: boolean;
    public emailId? : number;
    public fromAddress?: string;
    public ccAddress?: string;
    public subject?: string;
    public body?: string;
    public EditMode?: boolean;
   
}


export default class LoanStatusUpdateModel {
    public isActive?: boolean;
    public loanStatus?: LoanStatus[]
    constructor(isActive? : boolean, loanStatus? : LoanStatus[]){
      this.isActive = isActive;
      this.loanStatus =  loanStatus
    }
}