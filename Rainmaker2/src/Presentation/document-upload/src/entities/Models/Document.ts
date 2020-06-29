export class Document {
    public id: string;
    public clientName: string;
    public editName: boolean = false;
    public fileUploadedOn: string;
    public size: number;
    public order: number;
    public file?: File;
    public uploadProgress: number = 0;
    public uploadStatus?: string = 'pending'
    public documentOrder: any[] = []

    constructor(
        id: string = '',
        clientName: string = '',
        fileUploadedOn: string = '',
        size: number = 0,
        order: number = 0,
        uploadStatus?: string,
        file?: File
        ) {
        this.id = id;
        this.clientName = clientName;
        this.fileUploadedOn = fileUploadedOn;
        this.size = size;
        this.order = order;
        this.file = file;
        this.uploadStatus = uploadStatus;
        this.documentOrder = [{ fileName: this.clientName, order: 0 }]
    }

}