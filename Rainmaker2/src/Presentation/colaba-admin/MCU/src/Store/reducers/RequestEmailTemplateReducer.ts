import {RequestEmailTemplate} from '../../Entities/Models/RequestEmailTemplate';
import { Tokens } from '../../Entities/Models/Token';
import { ActionMap, Actions } from "./reducers";

export enum RequestEmailTemplateActionsType {
    SetRequestEmailTemplateData = "SET_REQUESTEMAILTEMPLATE_DATA",
    SetSelectedEmailTemplate = "SET_SELECTED_EMAIL_TEMPLATE",
    SetTokens = "SET_TOKENS",
    SetSelectedToken = "SET_SELECTED_TOKEN",
    SetDraftEmail = "SET_DRAFT_EMAIL",
    SetEmailContent = "SET_EMAIL_CONTENT",
    SetEdit = "SET_EDIT",
    SetListUpdated ="SET_LIST_UPDATED",
    SelectedEmailId= "SELECTED_EMAIL_ID"
}

export type RequestEmailTemplateType = {
   requestEmailTemplateData: RequestEmailTemplate[],
   selectedEmailTemplate: RequestEmailTemplate,
   tokens: Tokens[],
   selectedToken: Tokens,
   draftEmail: RequestEmailTemplate
   emailContent: RequestEmailTemplate,
   isEdit: string,
   listUpdated: boolean,
   selectedId: string
}

export type RequestEmailTemplateActionPayload = {
    [RequestEmailTemplateActionsType.SetRequestEmailTemplateData]: RequestEmailTemplate[],
    [RequestEmailTemplateActionsType.SetSelectedEmailTemplate]: RequestEmailTemplate,
    [RequestEmailTemplateActionsType.SetTokens]: Tokens[],
    [RequestEmailTemplateActionsType.SetSelectedToken]: Tokens,
    [RequestEmailTemplateActionsType.SetDraftEmail]: RequestEmailTemplate,
    [RequestEmailTemplateActionsType.SetEmailContent]: RequestEmailTemplate
    [RequestEmailTemplateActionsType.SetEdit]: string,
    [RequestEmailTemplateActionsType.SetListUpdated]: boolean
    [RequestEmailTemplateActionsType.SelectedEmailId]: string

}

export type RequestEmailTemplateActions = ActionMap<RequestEmailTemplateActionPayload>[keyof ActionMap<RequestEmailTemplateActionPayload>];

export const requestEmailTemplateReducer = (state : RequestEmailTemplateType | {}, {type, payload} : Actions) => {
    switch (type) {
        case RequestEmailTemplateActionsType.SetRequestEmailTemplateData:
            return {
                ...state,
                requestEmailTemplateData: payload
            }
            case RequestEmailTemplateActionsType.SetSelectedEmailTemplate:
                return {
                    ...state,
                    selectedEmailTemplate: payload
            }

            case RequestEmailTemplateActionsType.SetTokens:
                return {
                    ...state,
                    tokens: payload
            }

            case RequestEmailTemplateActionsType.SetSelectedToken:
                return {
                    ...state,
                    selectedToken: payload
            }

            case RequestEmailTemplateActionsType.SetDraftEmail:
                return {
                    ...state,
                    draftEmail: payload
            }
            case RequestEmailTemplateActionsType.SetEmailContent:
                return {
                    ...state,
                    emailContent: payload
                }
            case RequestEmailTemplateActionsType.SetEdit:
                return {
                    ...state,
                    isEdit: payload
                }
            case RequestEmailTemplateActionsType.SetListUpdated:
                return {
                    ...state,
                    listUpdated: payload
                }
            case RequestEmailTemplateActionsType.SelectedEmailId:
                return {
                    ...state,
                    selectedId: payload
                }
        default:
            return state
    }
}
