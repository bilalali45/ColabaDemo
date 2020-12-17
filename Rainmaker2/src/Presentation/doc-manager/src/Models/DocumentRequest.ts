import { Document } from "./Document";
import { DocumentFile } from "./DocumentFile";

export class DocumentRequest {

    public id: string;
    public requestId: string;
    public docId: string;
    public docName: string;
    public status: string;
    public createdOn: string;
    public files: DocumentFile[];
    public typeId: string;
    public userName: string;

    constructor(
        id: string,
        requestId: string,
        docId: string,
        docName: string,
        status: string, 
        createdOn: string,
        files: DocumentFile[] ,
        typeId: string, 
        userName: string
        
    ) {
        this.id = id;
        this.docId = docId;
        this.requestId = requestId;
        this.docName = docName;
        this.status = status;
        this.createdOn = createdOn;
        this.files = files;
        this.typeId = typeId;
        this.userName = userName;
        
    }
    
    
    
}