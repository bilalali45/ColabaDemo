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
  typeId: string;
  userName: string;
}

export interface DocumentParamsType {
  blob: any;
}

export interface FileType {
  fileId: string;
  clientName: string;
  mcuName: string;
  fileUploadedOn: string;
  isRead: boolean
}

export interface DocumentFileType {
  id: string;
  docId: string;
  docName: string;
  typeId: string;
  files: FileType[];
  userName: string;
}

export interface LogType {
  _id: string;
  dateTime: string;
  activity: string;
}

export interface ActivityLogType {
  id: string;
  userId: number;
  userName: string;
  typeId: string;
  docId: string;
  activity: string;
  dateTime: string;
  loanId: string;
  message: string;
  log: LogType[];
}

export enum DocumentStatus {
  PENDING_REVIEW = 'Pending review',
  COMPLETED = 'Completed',
  IN_DRAFT = 'In draft'
}

export interface EmailLogsType {
  dateTime: string;
  emailText: string;
  userName: string;
  message: string;
}
