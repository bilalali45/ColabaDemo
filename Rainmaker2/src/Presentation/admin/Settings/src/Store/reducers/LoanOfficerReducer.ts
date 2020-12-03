import LoanOfficer from "../../Entities/Models/LoanOfficer";
import { ActionMap, Actions } from "./reducers"

export enum LoanOfficerActionType {
    SetLoanOfficerData = "SET_LOANOFFICER_Data"
}

export type LoanOfficerType = {
    loanOfficerData: LoanOfficer[] 
}

export type LoanOfficersActionPayload = {
    [LoanOfficerActionType.SetLoanOfficerData]: LoanOfficer[] 
}

export type LoanOfficerActions = ActionMap<LoanOfficersActionPayload>[keyof ActionMap<LoanOfficersActionPayload>];

export const loanOfficerReducer = (state : LoanOfficerType | {}, {type, payload} : Actions) => {
    switch (type) {
        case LoanOfficerActionType.SetLoanOfficerData:
            return {
                ...state,
                loanOfficerData: payload
            }

        default:
            return state
    }
}
