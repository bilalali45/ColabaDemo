import { Actions, ActionMap } from "./reducers";
import { ContactUs } from "../../entities/Models/ContactU";
import { LoanApplication } from "../../entities/Models/LoanApplication";

export enum DocumentsActionType {
    FetchPendingDocs = 'FETCH_PENDING_DOCS',
    FetchSubmittedDocs = 'FETCH_SUBMITTED_DOCS'
    
}

type DocumentsType = {
    loanOfficer?: ContactUs | null,
    loanInfo?: LoanApplication | null
}


type DocumentsActionPayload = {
    [DocumentsActionType.FetchPendingDocs] : LoanApplication,
    [DocumentsActionType.FetchSubmittedDocs] : ContactUs,
}

export type DocumentsActions = ActionMap<DocumentsActionPayload>[keyof ActionMap<DocumentsActionPayload>];

export const documentsReducer = (state: DocumentsType, {type, payload} : Actions) => {
    switch (type) {
        case DocumentsActionType.FetchPendingDocs:
            return {
                ...state,
                pendingDocs: {...payload}
            };
    
        case DocumentsActionType.FetchSubmittedDocs:
            return {
                ...state,
                submittedDocs: {...payload}
            };
    
        default:
            return state;
    }
}