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
    public EditMode?: boolean = false;
   

    // constructor(
    //     id?: number,mcuName?: string,statusId?: number, 
    //     fromStatusId?: number,fromStatus?: string,toStatusId?: number, 
    //     toStatus?: string,noofDays?: number,recurringTime?: Date,isActive?: boolean,
    //     emailId? : number,fromAddress?: string,ccAddress?: string,subject?: string,body?: string
    //     ){
    //    this.id = id,
    //    this.mcuName = mcuName;
    //    this.statusId = statusId;
    //    this.fromStatusId = fromStatusId;
    //    this.fromStatus = fromStatus;
    //    this.toStatusId = toStatusId;
    //    this.toStatus = toStatus;
    //    this.noofDays = noofDays;
    //    this.recurringTime = recurringTime;
    //    this.isActive = isActive;
    //    this.emailId = emailId;
    //    this.fromAddress = fromAddress;
    //    this.ccAddress = ccAddress;
    //    this.subject = subject;
    //    this.body = body;
    // }
}


export default class LoanStatusUpdateModel {
    public isActive?: boolean;
    public loanStatus?: LoanStatus[]
    constructor(isActive? : boolean, loanStatus? : LoanStatus[]){
      this.isActive = isActive;
      this.loanStatus =  loanStatus
    }
}