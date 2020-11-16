import { LoanApplication } from "../../Entities/Models/LoanApplication"
import { NeedList } from "../../Entities/Models/NeedList"
import { ActionMap, Actions } from "./reducers"

export enum NeedListActionsType {
    SetLoanInfo = "SET_LOAN_INFO",
    SetNeedListTableDATA = "SET_NEEDLIST_TABLE_DATA",
    SetTemplateIds = "SET_TEMPLATE_IDS",
    SetIsDraft = "SET_IS_DRAFT",
    SetIsByteProAuto = "SET_IS_BYTE_PRO_AUTO",
    SetNeedListFilter = "SET_NEED_LIST_FILTER"
}

export type NeedListType = {
    loanInfo: LoanApplication[],
    needList: NeedList[],
    templateIds: string[],
    isDraft: boolean,
    needListFilter: boolean
}

export type NeedListActionPayload = {
    [NeedListActionsType.SetLoanInfo]: LoanApplication[],
    [NeedListActionsType.SetNeedListTableDATA]: NeedList[],
    [NeedListActionsType.SetTemplateIds]: string[],
    [NeedListActionsType.SetIsDraft]: string,
    [NeedListActionsType.SetIsByteProAuto]: string,
    [NeedListActionsType.SetNeedListFilter]: string
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
        case NeedListActionsType.SetIsByteProAuto:
            return{
                ...state,
                isByteProAuto: payload
            }  
            case NeedListActionsType.SetNeedListFilter:
            return{
                ...state,
                needListFilter: payload
            }   
        default:
            return state;
    }

}