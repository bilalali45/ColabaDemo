export class TrashItem {
    public id: string;
    public fileUploadedOn: string;
    public mcuName: string
    public userId: string;
    public userName: string;


    constructor(id: string, fileUploadedOn: string, mcuName: string, userId: string, userName: string) {
       
        this.id = id;
        this.fileUploadedOn = fileUploadedOn;
        this.mcuName = mcuName;
        this.userId = userId;
        this.userName = userName;
    }
}