export class TemplateDocument {
    public docId: string;
    public typeId: string;
    public docName: string;
    public docMessage: string;
    public message: string;
    public localId: string;
    public templateId: string;
    public requestId: string = '';
    public isCustom: boolean = false;
    
    constructor(templateId: string, typeId: string, localId: string, docId: string, docName: string, docMessage: string, message: string) {
        this.docId = docId;
        this.docName = docName;
        this.docMessage = docMessage;
        this.message = message;
        this.localId = localId;
        this.typeId = typeId;
        this.templateId = templateId;
    }
}
