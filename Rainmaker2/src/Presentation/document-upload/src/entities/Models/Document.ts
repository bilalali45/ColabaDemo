export class Document {
    public id: string;
    public clientName: string;
    public fileUploadedOn: string;
    public size: number;
    public order: number;

    constructor(id: string, clientName: string, fileUploadedOn: string, size: number, order: number) {
        this.id = id;
        this.clientName = clientName;
        this.fileUploadedOn = fileUploadedOn;
        this.size = size;
        this.order = order;
    }

}