export class Document {
    public id: string;
    public clientName: string;
    public fileUploadedOn: string;
    public size: number;
    public order: number;
    public file?: File

    constructor(id: string = '', clientName: string = '', fileUploadedOn: string = '', size: number = 0, order: number = 0, file?: File) {
        this.id = id;
        this.clientName = clientName;
        this.fileUploadedOn = fileUploadedOn;
        this.size = size;
        this.order = order;
        this.file = file
    }

}