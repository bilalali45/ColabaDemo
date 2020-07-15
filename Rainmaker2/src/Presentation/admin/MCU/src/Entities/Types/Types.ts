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
  filePath: string;
  fileType: string;
  blob: any;
}
