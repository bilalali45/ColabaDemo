export class NeedListDocuments {
    public id: string;
    public clientName: string;
    public fileUploadedOn: string;
    public mcuName: string;
    public byteProStatus: string;
    public status: string;

    constructor(id: string, clientName: string, fileUploadedOn:  string, mcuName: string, byteProStatus: string, status: string) {
        this.id = id;
        this.clientName = clientName;
        this.fileUploadedOn = fileUploadedOn;
        this.mcuName =mcuName;
        this.byteProStatus = byteProStatus;
        this.status = status
    }
}