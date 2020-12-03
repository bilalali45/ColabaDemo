export default class LoanOfficer {
    userId: number;
    userName: string;
    byteUserName: string;
    fullName: string;

    constructor(id: number, userName: string, byteUserName: string, fullName: string) {
        this.userId = id;
        this.userName = userName;
        this.byteUserName = byteUserName === null ? "" : byteUserName;
        this.fullName = fullName
    }
}
