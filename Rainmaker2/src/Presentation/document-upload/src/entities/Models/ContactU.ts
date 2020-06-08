export class ContactUs {
    public id: string;
    public userName: string;
    public phone: string;
    public email: string;
    public website: string;
    public profileImageUrl: string;

    constructor(id: string, userName: string, phone: string, email: string, website: string, profileImageUrl: string) {
        this.id = id;
        this.userName = userName;
        this.phone = phone;
        this.email = email;
        this.website = website;
        this.profileImageUrl = profileImageUrl;
    }

}