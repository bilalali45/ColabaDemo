import { Actions, ActionMap } from "./reducers";

export enum UserActionTypes {
    SetInfo = 'SET_INFO',
    SetLoanApplication = 'SET_LOAN_APPLICATION',
    SetUploadedDocuments = 'SET_UPLOADED_DOCUMENTS',
    UploadDocuments = 'UPLOAD_DOCUMENTS'
    
}

type UserType = {} | null;

type UserPayload = {
    [UserActionTypes.SetInfo] : {}
}

export type UserActions = ActionMap<UserPayload>[keyof ActionMap<UserPayload>];

export const userReducer = (state: UserType, {type, payload} : Actions) => {
    switch (type) {
        case UserActionTypes.SetInfo:
            return payload;
    
        default:
            return state;
    }
}