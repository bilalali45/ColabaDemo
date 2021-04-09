import { ActionMap, Actions } from "./reducers"

export enum ErrorActionsType {
     
    SetErrorMessage = "SET_ERROR_MESSAGE"
}

export type ErrorType = {
    error:Error 
}

export type ErrorActionPayload = {
    [ErrorActionsType.SetErrorMessage]:Error
}

export type ErrorActions = ActionMap<ErrorActionPayload>[keyof ActionMap<ErrorActionPayload>];

export const errorReducer = (state : ErrorType |{}, {type, payload} : Actions) => {
    switch (type) {
        case ErrorActionsType.SetErrorMessage:
            return {
                ...state,
                error: payload
            }

        default:
            return state
    }
}
