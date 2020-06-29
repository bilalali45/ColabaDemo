import { Document } from "./Document";

export class DocumentRequest {

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
    
    // public get remainingCount() : number {
    //     return this.requiredDocs.length - this.submittedDocs.length;
    // }

    // public get submittedCount() : number {
    //     return this.submittedDocs.length;
    // }

    // public addDocs(doc: Document) {
    //     this.submittedDocs.push(doc);
    // }

    // public removeDocs(id: string) {
    //     this.submittedDocs = this.submittedDocs.filter(d => d.id !== id);
    // }
    
}