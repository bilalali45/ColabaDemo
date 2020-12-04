export class Tokens {
    public id?: number;
    public name?: string;
    public symbol?: string;
    public description?: string;
    public key?: Date;
    public fromAddess? : boolean;
    public ccAddess? : boolean;
    public emailBody? : boolean;
    public emailSubject? : boolean;

    constructor(id?: number, name?: string, symbol?: string, description?: string, key?: Date, fromAddess? : boolean, ccAddess? : boolean, emailBody? : boolean, emailSubject? : boolean){
      this.id = id;
      this.name = name;
      this.symbol = symbol;
      this.description = description;
      this.key = key;
      this.fromAddess = fromAddess;
      this.ccAddess = ccAddess;
      this.emailBody = emailBody;
      this.emailSubject = emailSubject;
    }
}