export class TemplateDocument {
    public docId: string;
    public docName: string;
    public docMessage: string;

    constructor(docId: string, docName: string, docMessage: string) {
        this.docId = docId;
        this.docName = docName;
        this.docMessage = docMessage;
    }
}
