export class TemplateDocument {
    public docId: string;
    public docName: string;
    public docMessage: string;
    public message: string;
    public localId: string;
    
    constructor(localId: string, docId: string, docName: string, docMessage: string, message: string) {
        this.docId = docId;
        this.docName = docName;
        this.docMessage = docMessage;
        this.message = message;
        this.localId = localId;
    }
}
