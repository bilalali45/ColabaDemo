import { Actions, ActionMap } from "./reducers";
import { DocumentRequest } from "../../entities/Models/DocumentRequest";

export enum DocumentsActionType {
    FetchPendingDocs = 'FETCH_PENDING_DOCS',
    FetchSubmittedDocs = 'FETCH_SUBMITTED_DOCS',
    SetCurrentDoc = 'SET_CURRENT_DOC'

}

export type DocumentsType = {
    pendingDocs: DocumentRequest[] | null,
    currentDoc: DocumentRequest | null
}


type DocumentsActionPayload = {
    [DocumentsActionType.FetchPendingDocs]: DocumentRequest[],
    [DocumentsActionType.SetCurrentDoc]: DocumentRequest
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

        // case DocumentsActionType.FetchSubmittedDocs:
        //     return {
        //         ...state,
        //         submittedDocs: {...payload}
        //     };

        default:
            return state;
    }
}