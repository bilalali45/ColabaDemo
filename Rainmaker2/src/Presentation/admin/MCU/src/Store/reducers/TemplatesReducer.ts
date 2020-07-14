import { Template } from "../../Entities/Models/Template";
import { CategoryDocument } from "../../Entities/Models/CategoryDocument";
import { TemplateDocument } from "../../Entities/Models/TemplateDocument";
import { ActionMap, Actions } from "./reducers"

export enum TemplateActionsType {
    SetTemplates = "SET_TEMPLATES",
    SetCategoryDocuments = "SET_CATEGORY_DOCUMENTS",
    SetTemplateDocuments = "SET_TEMPLATE_DOCUMENTS",
    SetCurrentTemplate = "SET_CURRENT_TEMPLATE",
    InsertTemplate = "INSERT_TEMPLATE",
    RenameTemplate = "RENAME_TEMPLATE",
    DeleteTemplate = "DELETE_TEMPLATE"
}

export type TemplateType = {
    templates: Template[],
    currentTemplate: Template,
    categoryDocuments: CategoryDocument[],
    templateDocuments: TemplateDocument[],
}

export type TemplateActionPayload = {
    [TemplateActionsType.SetTemplates]: Template[],
    [TemplateActionsType.SetCategoryDocuments]: CategoryDocument[],
    [TemplateActionsType.SetTemplateDocuments]: TemplateDocument[],
    [TemplateActionsType.SetCurrentTemplate]: Template,
}

export type TemplateActions = ActionMap<TemplateActionPayload>[keyof ActionMap<TemplateActionPayload>];

export const templateReducer = (state: TemplateType | {}, { type, payload }: Actions) => {
    switch (type) {
        case TemplateActionsType.SetTemplates:
            return {
                ...state,
                templates: payload
            }

        case TemplateActionsType.SetCurrentTemplate:
            return {
                ...state,
                currentTemplate: payload
            }

        case TemplateActionsType.SetCategoryDocuments:
            return {
                ...state,
                categoryDocuments: payload
            }

        case TemplateActionsType.SetTemplateDocuments:

            return {
                ...state,
                templateDocuments: payload
            }

        default:
            return state;
    }
}