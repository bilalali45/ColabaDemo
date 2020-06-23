import { Actions, ActionMap } from "./reducers";
import { ContactUs } from "../../entities/Models/ContactU";
import { LoanApplication } from "../../entities/Models/LoanApplication";

export enum LoanActionsType {
    FetchLoanInfo = 'FETCH_LOAN_INFO',
    FetchLoanOfficer = 'FETCH_LOAN_OFFICER'
    
}

export type LoanType = {
    loanOfficer: ContactUs | null,
    loanInfo: LoanApplication | null
}


export type LoanActionPayload = {
    [LoanActionsType.FetchLoanInfo] : LoanApplication,
    [LoanActionsType.FetchLoanOfficer] : ContactUs,
}

export type LoanActions = ActionMap<LoanActionPayload>[keyof ActionMap<LoanActionPayload>];

export const loanReducer = (state: LoanType | {}, {type, payload} : Actions) => {
    switch (type) {
        case LoanActionsType.FetchLoanOfficer:
            let st = {
                ...state,
                loanOfficer:  {...payload}
            };
            return st;
    
        case LoanActionsType.FetchLoanInfo:
            let s = {
                ...state,
                loanInfo: {...payload}
            };
            return s;
    
        default:
            return state;
    }
}