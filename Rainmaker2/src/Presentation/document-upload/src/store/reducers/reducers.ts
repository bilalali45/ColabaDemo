import { LoanActions, loanReducer } from "./loanReducer";
import { AuthActions, authReducer } from "./aauthReducer";
import { UserActions, userReducer } from "./userReducer";
import { InitialStateType } from "../store";
import { DocumentsActions, documentsReducer } from "./documentReducer";

export type ActionMap<M extends { [index: string]: any }> = {
    [Key in keyof M]: M[Key] extends undefined
    ? {
        type: Key;
    }
    : {
        type: Key;
        payload: M[Key];
    }
};

export const mainReducer = ({loan, documents} : InitialStateType  , action: Actions) => ({
    // auth: authReducer(auth, action),
    // user: userReducer(user, action),
    loan: loanReducer(loan, action),
    documents: documentsReducer(documents, action)
});

export type Actions = AuthActions | UserActions | LoanActions | DocumentsActions;
