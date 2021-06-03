import { Document } from "./Document";

export class UploadedDocuments {

    public id: string;
    public requestId: string;
    public docId: string;
    public docName: string;
    public docMessage: string;
    public files: Document[]

    constructor(
        id: string,
        requestId: string,
        docId: string,
        docName: string,
        docMessage: string,
        files: Document[]
    ) {
        this.id = id;
        this.docId = docId;
        this.requestId = requestId;
        this.docName = docName;
        this.docMessage = docMessage;
        this.files = files;
    }   
}