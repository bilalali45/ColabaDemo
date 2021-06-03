export class ReminderEmailTemplate{
    public id?:string;
    public fromAddress?: string;
    public ccAddress?: string;
    public subject?: string;
    public emailBody?: string;
    constructor(id?:string, fromAddress?: string, ccAddress?: string, subject?: string, emailBody?: string){
        this.id = id;
        this.fromAddress = fromAddress;
        this.ccAddress = ccAddress;
        this.subject = subject;
        this.emailBody = emailBody;
    }
}
export default class ReminderSettingTemplate{
    public id?:string;
    public noOfDays?: string;
    public recurringTime?: Date;
    public isActive?: boolean;
    public email?:ReminderEmailTemplate;
    public isEditMode? : boolean;
    public index? : string = '';
    constructor(id?:string,noOfDays?: string, recurringTime?: Date, isActive?: boolean,email?:ReminderEmailTemplate){
        this.id = id;
        this.noOfDays = noOfDays;
        this.recurringTime = recurringTime;
        this.isActive = isActive;
        this.email = email;
        this.isEditMode = false;
    }
}

