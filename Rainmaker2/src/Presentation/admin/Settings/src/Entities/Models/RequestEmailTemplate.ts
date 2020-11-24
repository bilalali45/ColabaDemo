export class RequestEmailTemplate {

   public id?: number;
   public templateTypeId?: number;
   public tenantId?: number;
   public templateName?: string;
   public templateDescription?: string;
   public fromAddress?: string;
   public toAddress?: string;
   public ccAddress?: string;
   public subject?: string;
   public emailBody?: string;
   public sortOrder?: number;
   public showDelete: boolean = false;
   public deleteReqSent: boolean = false;

    constructor(id?: number, templateTypeId?: number, tenantId?: number, templateName?: string, templateDescription?: string, fromAddress?: string, toAddress?: string, ccAddress?: string, subject?: string, emailBody?: string, sortOrder?: number){
        this.id = id;
        this.templateTypeId = templateTypeId;
        this.tenantId = tenantId;
        this.templateName = templateName;
        this.templateDescription = templateDescription;
        this.fromAddress = fromAddress;
        this.toAddress = toAddress;
        this.ccAddress = ccAddress;
        this.subject = subject;
        this.emailBody = emailBody;
        this.sortOrder = sortOrder;
        this.showDelete = false;
        this.deleteReqSent = false;
    }
}