import { LoanApplication } from "../../Entities/Models/LoanApplication"
import { NeedList } from "../../Entities/Models/NeedList"
import { ActionMap, Actions } from "./reducers"

export enum NeedListActionsType {
    SetLoanInfo = "SET_LOAN_INFO",
    SetNeedListTableDATA = "SET_NEEDLIST_TABLE_DATA",
    SetTemplateIds = "SET_TEMPLATE_IDS",
    SetIsDraft = "SET_IS_DRAFT"
}

export type NeedListType = {
    loanInfo: LoanApplication[],
    needList: NeedList[],
    templateIds: string[],
    isDraft: boolean
}

export type NeedListActionPayload = {
    [NeedListActionsType.SetLoanInfo]: LoanApplication[],
    [NeedListActionsType.SetNeedListTableDATA]: NeedList[],
    [NeedListActionsType.SetTemplateIds]: string[],
    [NeedListActionsType.SetIsDraft]: string
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
        case NeedListActionsType.SetTemplateIds:
            return{
                ...state,
                templateIds: payload
            }
        case NeedListActionsType.SetIsDraft:
            return{
                ...state,
                isDraft: payload
            }
        default:
            return state;
    }

}