export class ContactUs {
    public firstName?: string;
    public lastName?: string;
    public phone?: string;
    public email?: string;
    public webUrl?: string;
    public rmls?: string;
    public photo?: string;
    
    constructor(firstName?: string, lastName?: string, phone?: string, email?: string, webUrl?: string, rmls?: string, photo?: string) {
       this.firstName = firstName;
       this.lastName = lastName;
       this.phone = phone;
       this.email = email;
       this.webUrl = webUrl;
       this.rmls = rmls;
       this.photo = photo;
    }

    fromJson(json: ContactUs) {
        this.firstName = json.firstName;    
        this.lastName = json.lastName;    
        this.phone = json.phone;    
        this.email = json.email;    
        this.webUrl = json.webUrl;    
        this.rmls = json.rmls;    
        this.photo = json.photo;    
        return this;
        
    }

    public completeName() {
        console.log('in here!!!')
        return `${this.firstName} ${this.lastName}`;
    }


}