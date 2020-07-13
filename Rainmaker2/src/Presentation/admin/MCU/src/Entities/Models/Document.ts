export class Document {
    public docTypeId: string;
    public docType: string;
    public docMessage: string;

    constructor(docTypeId: string, docType: string, docMessage: string) {
        this.docTypeId = docTypeId;
        this.docType = docType;
        this.docMessage = docMessage;

    }
}