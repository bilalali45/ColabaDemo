import { LoanApplication } from "../../Entities/Models/LoanApplication"
import { NeedList } from "../../Entities/Models/NeedList"
import { ActionMap, Actions } from "./reducers";

export type NeedListLock = {
    id: string,
    loanApplicationId: number,
    lockDateTime: string,
    lockUserId: number,
    lockUserName: string
}

export enum NeedListActionsType {
    SetLoanInfo = "SET_LOAN_INFO",
    SetNeedListTableDATA = "SET_NEEDLIST_TABLE_DATA",
    SetTemplateIds = "SET_TEMPLATE_IDS",
    SetIsDraft = "SET_IS_DRAFT",
    SetIsByteProAuto = "SET_IS_BYTE_PRO_AUTO",
    SetNeedListFilter = "SET_NEED_LIST_FILTER",
    SetIsNeedListLocked = "SET_IS_NEED_LIST_LOCKED"
}

export type NeedListType = {
    loanInfo: LoanApplication[],
    needList: NeedList[],
    templateIds: string[],
    isDraft: boolean,
    needListFilter: boolean,
    isNeedListLocked: NeedListLock
}

export type NeedListActionPayload = {
    [NeedListActionsType.SetLoanInfo]: LoanApplication[],
    [NeedListActionsType.SetNeedListTableDATA]: NeedList[],
    [NeedListActionsType.SetTemplateIds]: string[],
    [NeedListActionsType.SetIsDraft]: string,
    [NeedListActionsType.SetIsByteProAuto]: string,
    [NeedListActionsType.SetNeedListFilter]: string,
    [NeedListActionsType.SetIsNeedListLocked]: NeedListLock,
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
            return {
                ...state,
                templateIds: payload
            }
        case NeedListActionsType.SetIsDraft:
            return {
                ...state,
                isDraft: payload
            }
        case NeedListActionsType.SetIsByteProAuto:
            return {
                ...state,
                isByteProAuto: payload
            }
        case NeedListActionsType.SetNeedListFilter:
            return {
                ...state,
                needListFilter: payload
            }
        case NeedListActionsType.SetIsNeedListLocked:
            return {
                ...state,
                isNeedListLocked: payload
            }
        default:
            return state;
    }

}