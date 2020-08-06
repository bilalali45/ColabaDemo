export class TemplateDocument {
    public docId: string;
    public docName: string;
    public docMessage: string;
    public localId: string;

    constructor(docId: string, docName: string, docMessage: string, localId: string) {
        this.docId = docId;
        this.docName = docName;
        this.docMessage = docMessage;
        this.localId = localId;
    }
}
