
export class DocumentCategory {
    public id: string;
    public requestId: string;
    public docId: string;
    public docName: string;
    public status: string;
    public createdOn: string;
    public files: string;
    public typeId: string;
    public userName: string;

    constructor(id: string, requestId: string, docId: string,docName: string, status: string, createdOn: string,files: string, typeId: string, userName: string) {
        this.id = id;
        this.requestId = requestId;
        this.docId = docId;
        this.docName = docName;
        this.status = status;
        this.createdOn = createdOn;
        this.files = files;
        this.typeId = typeId;
        this.userName = userName;
        
        
    }
}