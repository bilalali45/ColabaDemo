import { NeedListDocuments } from './NeedListDocuments'

export class NeedList {
    
    public id: string;
    public requestId: string;
    public docName: string
    public docId: string;
    public status: string;
    public files: NeedListDocuments[] = [];
    public typeId: string;
    public userName: string;
    public index : number = 0;

    constructor(id: string, requestId: string, docName: string, docId: string, status: string, files: NeedListDocuments[], typeId: string, userName: string) {
       
        this.id = id;
        this.requestId = requestId;
        this.docName = docName;
        this.docId = docId;
        this.status = status;
        this.files = files;
        this.typeId = typeId;
        this.userName = userName
    }
}
