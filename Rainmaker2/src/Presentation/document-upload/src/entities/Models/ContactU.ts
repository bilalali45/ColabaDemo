export class ContactUs {
    public firstName?: string;
    public lastName?: string;
    public phone?: string;
    public email?: string;
    public webUrl?: string;
    public nmls?: string;
    public photo?: string;
    
    constructor(firstName?: string, lastName?: string, phone?: string, email?: string, webUrl?: string, nmls?: string, photo?: string) {
       this.firstName = firstName;
       this.lastName = lastName;
       this.phone = phone;
       this.email = email;
       this.webUrl = webUrl;
       this.nmls = nmls;
       this.photo = photo;
    }

    fromJson(json: ContactUs) {
        this.firstName = json.firstName;    
        this.lastName = json.lastName;    
        this.phone = json.phone;    
        this.email = json.email;    
        this.webUrl = json.webUrl;    
        this.nmls = json.nmls;    
        this.photo = json.photo;    
        return this;
        
    }

    public completeName() {
        console.log('in here!!!')
        return `${this.firstName} ${this.lastName}`;
    }


}