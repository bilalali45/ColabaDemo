import { Actions, ActionMap } from "./reducers";
import { DocumentRequest } from "../../entities/Models/DocumentRequest";
import { UploadedDocuments } from "../../entities/Models/UploadedDocuments";
import { FileUpload } from "../../utils/helpers/FileUpload";

export enum DocumentsActionType {
    FetchPendingDocs = 'FETCH_PENDING_DOCS',
    FetchSubmittedDocs = 'FETCH_SUBMITTED_DOCS',
    SetCurrentDoc = 'SET_CURRENT_DOC',
    AddFileToDoc = 'ADD_FILE_TO_DOC'

}

export type DocumentsType = {
    pendingDocs: DocumentRequest[] | null,
    currentDoc: DocumentRequest | null,
    submittedDocs: UploadedDocuments[] | null
}


type DocumentsActionPayload = {
    [DocumentsActionType.FetchPendingDocs]: DocumentRequest[],
    [DocumentsActionType.SetCurrentDoc]: DocumentRequest,
    [DocumentsActionType.AddFileToDoc]: Document[],
    [DocumentsActionType.FetchSubmittedDocs]: UploadedDocuments[]
}

export type DocumentsActions = ActionMap<DocumentsActionPayload>[keyof ActionMap<DocumentsActionPayload>];

export const documentsReducer = (state: DocumentsType | {}, { type, payload }: Actions) => {
    switch (type) {
        case DocumentsActionType.FetchPendingDocs:
            return {
                ...state,
                pendingDocs: payload
            };

        case DocumentsActionType.SetCurrentDoc:
            return {
                ...state,
                currentDoc: payload
            };

        case DocumentsActionType.AddFileToDoc:
            const pdocs = state['pendingDocs']?.map((pd: any) => {
                if (pd?.docId === state['currentDoc']?.docId) {
                    if (Array.isArray(payload)) {
                        pd.files = payload;
                    }
                    return pd;
                }
                return pd;
            });
            return {
                ...state,
                pendingDocs: pdocs
            }

        case DocumentsActionType.FetchSubmittedDocs:
            return {
                ...state,
                submittedDocs: payload
            };

        default:
            return state;
    }
}