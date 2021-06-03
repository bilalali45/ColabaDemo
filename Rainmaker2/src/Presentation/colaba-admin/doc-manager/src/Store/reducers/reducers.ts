import { useReducer } from "react";
import { InitialStateType } from "../Store";
import { DocumentsActions, documentsReducer } from "./documentsReducer";
import { ToolbarActions, toolbarReducer } from "./ToolbarReducer"
import { ViewerActions, viewerReducer } from "./ViewerReducer";
import { TemplateActions, templateReducer } from './TemplatesReducer';


export type ActionMap<M extends { [index: string]: any }> = {
    [Key in keyof M]: M[Key] extends undefined
    ? {
        type: Key;
    }
    : {
        type: Key;
        payload: M[Key];
    }
}

export type Actions = ToolbarActions | DocumentsActions | ViewerActions | TemplateActions;

export const mainReducer = ({
    toolbar,
    documents,
    viewer,
    templateManager
} : InitialStateType,
action: Actions) => ({
   toolbar: toolbarReducer(toolbar, action),
   documents: documentsReducer(documents, action),
   viewer: viewerReducer(viewer, action),
   templateManager: templateReducer(templateManager, action)
})