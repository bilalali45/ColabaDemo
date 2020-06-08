import { Document } from "./Document";

export class DocumentRequest {
    public submittedDocs: Document[] = [];
    public requiredDocs: Document[] = [];

    constructor() {}

    
    public get remainingCount() : number {
        return this.requiredDocs.length - this.submittedDocs.length;
    }

    public get submittedCount() : number {
        return this.submittedDocs.length;
    }

    public addDocs(doc: Document) {
        this.submittedDocs.push(doc);
    }

    public removeDocs(id: string) {
        this.submittedDocs = this.submittedDocs.filter(d => d.id !== id);
    }
    
}