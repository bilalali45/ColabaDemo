import { Actions, ActionMap } from "./reducers";
import { DocumentRequest } from "../../entities/Models/DocumentRequest";

export enum DocumentsActionType {
    FetchPendingDocs = 'FETCH_PENDING_DOCS',
    FetchSubmittedDocs = 'FETCH_SUBMITTED_DOCS',
    SetCurrentDoc = 'SET_CURRENT_DOC',
    AddFileToDoc = 'ADD_FILE_TO_DOC'

}

export type DocumentsType = {
    pendingDocs: DocumentRequest[] | null,
    currentDoc: DocumentRequest | null,
}


type DocumentsActionPayload = {
    [DocumentsActionType.FetchPendingDocs]: DocumentRequest[],
    [DocumentsActionType.SetCurrentDoc]: DocumentRequest,
    [DocumentsActionType.AddFileToDoc]: Document[]
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
            // debugger;
            return {
                ...state,
                currentDoc: payload
            };

        case DocumentsActionType.AddFileToDoc:
            const pdocs = state['pendingDocs']?.map((pd: any) => {
                if (pd?.docId === state['currentDoc']?.docId) {
                    pd.files = payload;
                    return pd;
                }
                return pd;
            });
            // debugger
            return {
                ...state,
                pendingDocs: pdocs
            }

        // case DocumentsActionType.FetchSubmittedDocs:
        //     return {
        //         ...state,
        //         submittedDocs: {...payload}
        //     };

        default:
            return state;
    }
}


// pendingDocs: state['pendingDocs'].map((p: DocumentRequest) => {
//     if (p.docName === state['currentDoc'].docName) {
//         if (Array.isArray(payload)) {
//             p['files'] = payload;
//         }
//         return {

//         }
//         currentDoc: {
//             ...state['currentDoc'],
//     files: payload
//         }
// }
// })