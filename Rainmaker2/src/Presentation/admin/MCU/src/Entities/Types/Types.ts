export interface NeedListDocumentFileType {
  byterProResponse: string;
  clientName: string;
  fileUploadedOn: string;
  id: string;
  mcuName: string;
  status: string | null;
}

export interface NeedListDocumentType {
  docId: string;
  docName: string;
  id: string;
  requestId: string;
  status: string;
  files: NeedListDocumentFileType[];
}

export interface DocumentParamsType {
  blob: any;
}

export interface FileType {
  fileId: string
  clientName: string
  mcuName: string
  fileUploadedOn: string
}

export interface DocumentFileType {
  id: string
  docId: string
  docName: string
  typeId: string
  files: FileType[]
  username: string
}
