export default class LoanOfficer {
    userId: number;
    userName: string;
    byteUserName: string;
    fullName: string;
    photo: string;
    constructor(id: number, userName: string, byteUserName: string, fullName: string,photo:string) {
        this.userId = id;
        this.userName = userName;
        this.byteUserName = byteUserName === null ? "" : byteUserName;
        this.fullName = fullName;
        this.photo = photo
    }
}
