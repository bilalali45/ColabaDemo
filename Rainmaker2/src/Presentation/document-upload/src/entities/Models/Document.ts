export class Document {
    public id: string;
    public document: string;
    public fileName: string;
    public addedDate: string;

    constructor(id: string, document: string, fileName: string, addedDate: string) {
        this.document = document;
        this.fileName = fileName;
        this.addedDate = addedDate;
        this.id = id;
    }

}