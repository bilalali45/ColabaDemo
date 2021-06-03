
import { AssignedRole } from "../../Entities/Models/AssignedRole";
import { ActionMap, Actions } from "./reducers"

export enum AssignedRoleActionsType {
    SET_ASSIGNED_ROLES = "SET_ASSIGNED_ROLES"
}

export type AssignedRoleType = {
    assignedRoles: AssignedRole[] 
}

export type AssignedRoleActionPayload = {
    [AssignedRoleActionsType.SET_ASSIGNED_ROLES]: AssignedRole[] 
}

export type AssignedRoleActions = ActionMap<AssignedRoleActionPayload>[keyof ActionMap<AssignedRoleActionPayload>];

export const assignedRoleReducer = (state : AssignedRoleType | {}, {type, payload} : Actions) => {
    switch (type) {
        case AssignedRoleActionsType.SET_ASSIGNED_ROLES:
            return {
                ...state,
                assignedRoles: payload
            }

        default:
            return state
    }
}
