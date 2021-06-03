import { stat } from 'fs';
import {RequestEmailTemplate} from '../../Entities/Models/RequestEmailTemplate';
import { Tokens } from '../../Entities/Models/Token';
import { ActionMap, Actions } from "./reducers";

export enum RequestEmailTemplateActionsType {
    SetRequestEmailTemplateData = "SET_REQUESTEMAILTEMPLATE_DATA",
    SetSelectedEmailTemplate = "SET_SELECTED_EMAIL_TEMPLATE",
    SetTokens = "SET_TOKENS",
    SetSelectedToken = "SET_SELECTED_TOKEN",
    SetShowAlertBox = "SET_SHOW_ALERT_BOX",
    SetEditedFields = "SET_EDITED_FIELDS"
}

export type RequestEmailTemplateType = {
   requestEmailTemplateData: RequestEmailTemplate[],
   selectedEmailTemplate: RequestEmailTemplate,
   tokens: Tokens[],
   selectedToken: Tokens,
   showAlertBox: boolean,
   editedFields: boolean
}

export type RequestEmailTemplateActionPayload = {
    [RequestEmailTemplateActionsType.SetRequestEmailTemplateData]: RequestEmailTemplate[],
    [RequestEmailTemplateActionsType.SetSelectedEmailTemplate]: RequestEmailTemplate,
    [RequestEmailTemplateActionsType.SetTokens]: Tokens[],
    [RequestEmailTemplateActionsType.SetSelectedToken]: Tokens,
    [RequestEmailTemplateActionsType.SetShowAlertBox]: boolean,
    [RequestEmailTemplateActionsType.SetEditedFields]: boolean,
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
            case RequestEmailTemplateActionsType.SetShowAlertBox:
                return{
                    ...state,
                    showAlertBox: payload
                }
            case RequestEmailTemplateActionsType.SetEditedFields:
            return{
                    ...state,
                    editedFields: payload
                }

        default:
            return state
    }
}
