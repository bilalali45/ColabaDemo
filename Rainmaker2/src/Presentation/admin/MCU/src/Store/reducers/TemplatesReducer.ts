import { Template } from "../../Entities/Modals/Template";
import { CategoryDocument } from "../../Entities/Modals/CategoryDocument";
import { TemplateDocument } from "../../Entities/Modals/TemplateDocument";
import { UserActionPayload } from "./UserReducer";
import { ActionMap, Actions } from "./reducers"
import { start } from "repl";

export enum TemplateActionsType {
    FetchTemplates = "FETCH_TEMPLATES",
    FetchCategoryDocuments = "FETCH_CATEGORY_DOCUMENTS",
    FetchTemplateDocuments = "FETCH_TEMPLATE_DOCUMENTS",
    InsertTemplate = "INSERT_TEMPLATE",
    RenameTemplate = "RENAME_TEMPLATE",
    DeleteTemplate = "DELETE_TEMPLATE"
}

export type TemplateType = {
    templates: Template[],
    categoryDocuments: CategoryDocument[],
    templateDocuments: TemplateDocument[],
}

export type TemplateActionPayload = {
    [TemplateActionsType.FetchTemplates]: Template[],
    [TemplateActionsType.FetchCategoryDocuments]: CategoryDocument[],
    [TemplateActionsType.FetchTemplateDocuments]: TemplateDocument[]
}

export type TemplateActions = ActionMap<TemplateActionPayload>[keyof ActionMap<TemplateActionPayload>];

export const templateReducer = (state: TemplateType | {}, { type, payload }: Actions) => {
    switch (type) {
        case TemplateActionsType.FetchTemplates:
            return {
                ...state,
                templates: payload
            }

        case TemplateActionsType.FetchCategoryDocuments:

            return {
                ...state,
                categoryDocuments: payload
            }

        case TemplateActionsType.FetchTemplateDocuments:

            return {
                ...state,
                templateDocuments: payload
            }

        default:
            return state;
    }
}