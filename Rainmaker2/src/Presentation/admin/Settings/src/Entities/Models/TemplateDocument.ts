export class TemplateDocument {
    public docId: string;
    public typeId: string;
    public docName: string;
    public docMessage?: string;
    public message?: string;
    public localId?: string;
    public templateId?: string;
    public requestId: string = '';
    public isCustom: boolean = false;
    
    constructor(docId: string, docName: string, typeId: string) {
        this.docId = docId;
        this.docName = docName;      
        this.typeId = typeId;    
    }
}
