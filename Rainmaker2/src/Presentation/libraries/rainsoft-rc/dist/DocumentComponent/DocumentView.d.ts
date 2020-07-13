import { FunctionComponent } from "react";
interface DocumentViewProps {
    id: string;
    requestId: string;
    docId: string;
    tenantId?: string;
    blobData?: any | null;
    submittedDocumentCallBack?: Function;
    fileId?: string;
    clientName?: string;
    hideViewer: (currentDoc: any) => void;
    file?: any;
}
export declare const DocumentView: FunctionComponent<DocumentViewProps>;
export {};
