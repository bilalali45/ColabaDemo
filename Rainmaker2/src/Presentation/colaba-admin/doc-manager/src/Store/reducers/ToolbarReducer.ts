import { ReassignItem } from "../../Models/ReassignItem";
import { TrashItem } from "../../Models/TrashItem";
import { ActionMap, Actions } from "./reducers";

export enum ToolbarActionsType {
    SetReassignItems = "SET_TEMPLATES",
    SetTrashItems = "SET_CATEGORY_DOCUMENTS"
}

export type ToolbarType = {
    reassignItems: ReassignItem[] | [];
    trashItems: TrashItem[] | []
    // documentsSelection: {}
}



export type ToolbarActionPayload = {
    [ToolbarActionsType.SetReassignItems]: ReassignItem[],
    [ToolbarActionsType.SetTrashItems]: TrashItem[],
}

export type ToolbarActions = ActionMap<ToolbarActionPayload>[keyof ActionMap<ToolbarActionPayload>];

export const toolbarReducer = (state: ToolbarType | {}, {type, payload} : Actions) => {
    // switch (key) {
    //     case value:
            
    //         break;
    
    //     default:
    //         break;
    // }

    return state;
}