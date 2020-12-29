import { Document } from "./Document";

export class DocumentRequest {

    public id: string;
    public requestId: string;
    public docId: string;
    public docName: string;
    public docMessage: string;
    public files: Document[] | null;
    public isRejected: boolean = false;
    public resubmittedNewFiles?: boolean = false;

    constructor(
        id: string,
        requestId: string,
        docId: string,
        docName: string,
        docMessage: string,
        files: Document[] | null,
        isRejected: boolean
    ) {
        this.id = id;
        this.docId = docId;
        this.requestId = requestId;
        this.docName = docName;
        this.docMessage = docMessage;
        this.files = files;
        this.isRejected = isRejected
    }  
    
}