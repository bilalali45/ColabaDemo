export class NeedListDocuments {
    public id: string;
    public clientName: string;
    public fileUploadedOn: string;
    public mcuName: string;
    public byteProStatus: string;
    public status: string;
    public isRead: boolean;
    public byteProStatusText: string = '';
    public byteProStatusClassName = '';
    
    constructor(id: string, clientName: string, fileUploadedOn:  string, mcuName: string, byteProStatus: string, status: string, isRead: boolean) {
        this.id = id;
        this.clientName = clientName;
        this.fileUploadedOn = fileUploadedOn;
        this.mcuName =mcuName;
        this.byteProStatus = byteProStatus;
        this.status = status
        this.isRead = isRead;
        
    }

    public fromJson(json: NeedListDocuments){
        this.id = json.id;
        this.clientName = json.clientName;
        this.fileUploadedOn = json.fileUploadedOn;
        this.mcuName = json.mcuName;
        this.byteProStatus = NeedListDocuments.updateByteProStatus(json.byteProStatus);
        this.status = json.status;
        this.isRead = json.isRead;
        //this.isRead = json.byteProStatus;
    }

    static updateByteProStatus(status: string){
      switch(status){
          case 'Synchronized':
            return 'Synced';
          case 'Not synchronized':
            return 'Not Synced';
          case 'Error':
              return 'Sync failed';
             default:
                 return '' 
      }
    }
}