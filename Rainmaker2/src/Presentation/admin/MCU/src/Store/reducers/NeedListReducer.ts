import { LoanApplication } from "../../Entities/Models/LoanApplication"
import { NeedList } from "../../Entities/Models/NeedList"
import { ActionMap, Actions } from "./reducers"

export enum NeedListActionsType {
    SetLoanInfo = "SET_LOAN_INFO",
    SetNeedListTableDATA = "SET_NEEDLIST_TABLE_DATA"
}

export type NeedListType = {
    loanInfo: LoanApplication[],
    needList: NeedList[]
}

export type NeedListActionPayload = {
    [NeedListActionsType.SetLoanInfo]: LoanApplication[],
    [NeedListActionsType.SetNeedListTableDATA]: NeedList[]
}

export type NeedListActions = ActionMap<NeedListActionPayload>[keyof ActionMap<NeedListActionPayload>];

export const needListReducer = (state: NeedListType | {}, { type, payload }: Actions) => {
    switch (type) {
        case NeedListActionsType.SetLoanInfo:
            return {
                ...state,
                loanInfo: payload
            }
        case NeedListActionsType.SetNeedListTableDATA:
            return {
                ...state,
                needList: payload
            }
        default:
            return state;
    }

}