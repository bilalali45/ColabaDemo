
import Organization from "../../Entities/Models/Organization";
import { ActionMap, Actions } from "./reducers"

export enum OrganizationActionsType {
    SetOrganizationData = "SET_ORGANIZATION_Data"
}

export type OrganizationType = {
    organizationData: Organization[] 
}

export type OrganizationActionPayload = {
    [OrganizationActionsType.SetOrganizationData]: Organization[] 
}

export type OrganizationActions = ActionMap<OrganizationActionPayload>[keyof ActionMap<OrganizationActionPayload>];

export const organizationReducer = (state : OrganizationType | {}, {type, payload} : Actions) => {
    switch (type) {
        case OrganizationActionsType.SetOrganizationData:
            return {
                ...state,
                organizationData: payload
            }

        default:
            return state
    }
}
