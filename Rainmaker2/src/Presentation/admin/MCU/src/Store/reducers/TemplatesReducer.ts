import { Template } from "../../Entities/Models/Template";
import { CategoryDocument } from "../../Entities/Models/CategoryDocument";
import { TemplateDocument } from "../../Entities/Models/TemplateDocument";
import { ActionMap, Actions } from "./reducers"
import { Document } from "../../Entities/Models/Document";

type AddDocumentBoxVisibleType = {
    value: boolean
}

export enum TemplateActionsType {
    SetTemplates = "SET_TEMPLATES",
    SetCategoryDocuments = "SET_CATEGORY_DOCUMENTS",
    SetTemplateDocuments = "SET_TEMPLATE_DOCUMENTS",
    SetCurrentTemplate = "SET_CURRENT_TEMPLATE",
    SetCurrentCategoryDocuments = "SET_CURRENT_CATEGORY_DOCUMENT",
    InsertTemplate = "INSERT_TEMPLATE",
    RenameTemplate = "RENAME_TEMPLATE",
    DeleteTemplate = "DELETE_TEMPLATE",
    ToggleAddDocumentBox = "TOGGLE_ADD_DOCUMENT_BOX",
    SetSelectedTemplateDocuments = "SET_SELECTED_TEMPLATE_DOCUMENTS",
    SetEmailContent = "SET_EMAIL_CONTENT"
}

export type TemplateType = {
    templates: Template[],
    currentTemplate: Template,
    categoryDocuments: CategoryDocument[],
    currentCategoryDocuments: CategoryDocument,
    templateDocuments: TemplateDocument[],
    addDocumentBoxVisible: AddDocumentBoxVisibleType,
}

export type TemplateActionPayload = {
    [TemplateActionsType.SetTemplates]: Template[],
    [TemplateActionsType.SetCurrentTemplate]: Template,
    [TemplateActionsType.SetTemplateDocuments]: TemplateDocument[],
    [TemplateActionsType.SetCategoryDocuments]: CategoryDocument[],
    [TemplateActionsType.SetCurrentCategoryDocuments]: CategoryDocument,
    [TemplateActionsType.ToggleAddDocumentBox]: AddDocumentBoxVisibleType,
    [TemplateActionsType.SetSelectedTemplateDocuments]: TemplateDocument[],
    [TemplateActionsType.SetEmailContent]: string
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

        case TemplateActionsType.SetCurrentCategoryDocuments:

            return {
                ...state,
                currentCategoryDocuments: payload
            }

        case TemplateActionsType.ToggleAddDocumentBox:

            return {
                ...state,
                addDocumentBoxVisible: payload
            }
        case TemplateActionsType.SetSelectedTemplateDocuments:
            return {
                ...state,
                selectedTemplateDocuments: payload
            }
        case TemplateActionsType.SetEmailContent:
            return {
                ...state,
                emailContent: payload
            }

        default:
            return state;
    }
}