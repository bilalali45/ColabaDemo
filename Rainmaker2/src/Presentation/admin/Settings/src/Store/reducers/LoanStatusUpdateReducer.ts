import LoanStatusUpdateModel, {LoanStatus} from "../../Entities/Models/LoanStatusUpdate";
import { ActionMap, Actions } from "./reducers";


export enum LoanStatusUpdateActionsType {
    SetLoanStatusData = "SET_LOAN_STATUS_DATA",   
    SetSelectedLoanStatus = "SET_SELECTED_LOAN_STATUS"
}

export type LoanStatusUpdateType = {
    loanStatusData : LoanStatusUpdateModel,
    selectedLoanStatus: LoanStatus
}

export type LoanStatusActionPayload = {
    [LoanStatusUpdateActionsType.SetLoanStatusData]: LoanStatusUpdateModel
    [LoanStatusUpdateActionsType.SetSelectedLoanStatus]: LoanStatus
}

export type LoanStatusActions =  ActionMap<LoanStatusActionPayload> [keyof ActionMap<LoanStatusActionPayload>];

export const loanStatusUpdateReducer = (state: LoanStatusUpdateType | {}, {type, payload}: Actions) => {
    switch(type) {
        case LoanStatusUpdateActionsType.SetLoanStatusData:
            return {
                ...state,
                loanStatusData: payload
            }
            case LoanStatusUpdateActionsType.SetSelectedLoanStatus:
            return {
                ...state,
                selectedLoanStatus: payload
            }

        default:
            return state
    }
}
