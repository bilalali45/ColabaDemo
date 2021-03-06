import { FunctionComponent } from 'react';
interface DocumentViewProps {
    id: string;
    requestId: string;
    docId: string;
    blobData?: any | null;
    submittedDocumentCallBack?: Function;
    fileId?: string;
    clientName?: string;
    hideViewer: (currentDoc: any) => void;
    file?: any;
    loading?: boolean;
    showCloseBtn?: boolean;
    isMobile?: any;
}
export declare const DocumentView: FunctionComponent<DocumentViewProps>;
export {};
