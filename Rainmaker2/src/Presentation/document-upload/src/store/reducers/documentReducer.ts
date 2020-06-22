import { Actions, ActionMap } from "./reducers";
import { DocumentRequest } from "../../entities/Models/DocumentRequest";

export enum DocumentsActionType {
    FetchPendingDocs = 'FETCH_PENDING_DOCS',
    FetchSubmittedDocs = 'FETCH_SUBMITTED_DOCS'
    
}

export type DocumentsType = {
    pendingDocs: DocumentRequest[] | null,
}


type DocumentsActionPayload = {
    [DocumentsActionType.FetchPendingDocs] : DocumentRequest[],
}

export type DocumentsActions = ActionMap<DocumentsActionPayload>[keyof ActionMap<DocumentsActionPayload>];

export const documentsReducer = (state: DocumentsType | {}, {type, payload} : Actions) => {
    switch (type) {
        case DocumentsActionType.FetchPendingDocs:
            return {
                ...state,
                pendingDocs: payload
            };
    
        // case DocumentsActionType.FetchSubmittedDocs:
        //     return {
        //         ...state,
        //         submittedDocs: {...payload}
        //     };
    
        default:
            return state;
    }
}