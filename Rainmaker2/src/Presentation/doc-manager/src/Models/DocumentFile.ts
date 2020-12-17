import axios, { CancelToken } from "axios";

export class DocumentFile {
    public id: string;
    public clientName: string;
    public mcuName: string;
    public editName: boolean = false;
    public fileUploadedOn: string;
    public size: number;
    public order: number;
    public file: File;
    public uploadProgress: number = 0;
    public uploadStatus?: string = 'pending'
    public documentOrder: any[] = []
    public docLogo: string = '';
    public notAllowed: boolean = false;
    public notAllowedReason: string = '';
    public deleteBoxVisible: boolean = false;
    public uploadReqCancelToken = axios.CancelToken.source();
    public focused: boolean = true;
    public failedReason: string = '';
    public docCategoryId: string = '';
    public userName:string= '';

    constructor(
        id: string = '',
        clientName: string = '',
        fileUploadedOn: string = '',
        size: number = 0,
        order: number = 0,
        docLogo: string,
        file: File,
        uploadStatus?: string,
        mcuName: string = '',
        docCategoryId:string= '',
        userName:string = ''
        
        
        ) {
        this.id = id;
        this.clientName = clientName;
        this.fileUploadedOn = fileUploadedOn;
        this.size = size;
        this.order = order;
        this.file = file;
        this.uploadStatus = uploadStatus;
        this.documentOrder = [{ fileName: this.clientName, order: 0 }];
        this.docLogo = docLogo;
        this.mcuName = mcuName;
        this.docCategoryId = docCategoryId;
        this.userName = userName;
    }

}