import {RequestEmailTemplate} from '../../Entities/Models/RequestEmailTemplate';
import { Tokens } from '../../Entities/Models/Token';
import { ActionMap, Actions } from "./reducers";

export enum RequestEmailTemplateActionsType {
    SetRequestEmailTemplateData = "SET_REQUESTEMAILTEMPLATE_DATA",
    SetSelectedEmailTemplate = "SET_SELECTED_EMAIL_TEMPLATE",
    SetTokens = "SET_TOKENS",
    SetSelectedToken = "SET_SELECTED_TOKEN"
}

export type RequestEmailTemplateType = {
   requestEmailTemplateData: RequestEmailTemplate[],
   selectedEmailTemplate: RequestEmailTemplate,
   tokens: Tokens[],
   selectedToken: Tokens
}

export type RequestEmailTemplateActionPayload = {
    [RequestEmailTemplateActionsType.SetRequestEmailTemplateData]: RequestEmailTemplate[],
    [RequestEmailTemplateActionsType.SetSelectedEmailTemplate]: RequestEmailTemplate,
    [RequestEmailTemplateActionsType.SetTokens]: Tokens[],
    [RequestEmailTemplateActionsType.SetSelectedToken]: Tokens,
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

        default:
            return state
    }
}
