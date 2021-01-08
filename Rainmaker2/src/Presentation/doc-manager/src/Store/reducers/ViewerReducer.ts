import Instance from "pspdfkit/dist/types/typescript/Instance";
import { RefObject } from "react";
import { CurrentInView } from "../../Models/CurrentInView";
import { DocumentRequest } from "../../Models/DocumentRequest";
import { SelectedFile } from "../../Models/SelectedFile";
import { ThumbnailItem } from "../../Models/ThumbnailItem";
import { ActionMap, Actions } from "./reducers";

export type NeedListLock = {
    id: string,
    loanApplicationId: number,
    lockDateTime: string,
    lockUserId: number,
    lockUserName: string
}

export type FileToChange = {
    file: CurrentInView,
    document: DocumentRequest,
    action: string,
    isWorkbenchFile:boolean
}

export enum ViewerActionsType {
    SetInstance = "SET_INSTANCE",
    SetThumbnailItems = "SET_THUMBNAIL_ITEMS",
    SetCurrentFile = "SET_CURRENT_FILE",
    SetContainerElement = "SET_CONTAINER_ELEMENT",
    SetIsFileChanged = "SET_IS_FILE_CHANGED",
    SetIsNeedListLocked = "SET_IS_NEED_LIST_LOCKED",
    SetSelectedFileData = "SET_SELECTED_FILE_DATA",
    SetIsLoading = "SET_IS_LOADING",
    SetIsSaving = "SET_IS_SAVING",
    SetFileProgress = "SET_FILE_PROGRESS",
    SetShowingConfirmationAlert = "SET_SHOWING_CONFIRMATION_ALERT",
    SetFileToChangeWhenUnSaved = "SET_FILE_TO_CHANGE_WHEN_UNSAVED",
    SetSaveFile="SET_SAVE_FILE",
    SetDiscardFile="SET_DISCARD_FILE",
    SetPerformNextAction="SET_PERFORM_NEXT_ACTION",
    SetAnnotationsFirstTime= "SET_ANNOTATIONS_FIRST_TIME"
}

export type ViewerType = {
    instance: Instance;
    thumbnailItems: ThumbnailItem[] | [];
    currentFile: CurrentInView | {};
    containerElement: RefObject<HTMLDivElement>;
    isFileChanged: boolean;
    isNeedListLocked: NeedListLock;
    selectedFileData: SelectedFile;
    isLoading: boolean;
    isSaving:boolean;
    fileProgress:number;
    showingConfirmationAlert: boolean;
    fileToChangeWhenUnSaved: FileToChange,
    SaveCurrentFile:boolean,
    DiscardCurrentFile:boolean,
    performNextAction:boolean,
    setAnnotationsFirstTime:boolean
    // documentsSelection: {}
}

export type ViewerActionsPayload = {
    [ViewerActionsType.SetInstance]: Instance;
    [ViewerActionsType.SetThumbnailItems]: ThumbnailItem[];
    [ViewerActionsType.SetCurrentFile]: CurrentInView;
    [ViewerActionsType.SetContainerElement]: RefObject<HTMLDivElement>;
    [ViewerActionsType.SetIsFileChanged]: boolean;
    [ViewerActionsType.SetIsNeedListLocked]: NeedListLock;
    [ViewerActionsType.SetSelectedFileData]: SelectedFile;
    [ViewerActionsType.SetIsLoading]: boolean;
    [ViewerActionsType.SetIsSaving]: boolean;
    [ViewerActionsType.SetFileProgress]: number;
    [ViewerActionsType.SetShowingConfirmationAlert]: boolean;
    [ViewerActionsType.SetFileToChangeWhenUnSaved]: FileToChange;
    [ViewerActionsType.SetSaveFile]:boolean;
    [ViewerActionsType.SetDiscardFile]:boolean;
    [ViewerActionsType.SetPerformNextAction]:boolean;
    [ViewerActionsType.SetAnnotationsFirstTime]:boolean

}

export type ViewerActions = ActionMap<ViewerActionsPayload>[keyof ActionMap<ViewerActionsPayload>];

export const viewerReducer = (state: ViewerType | {}, { type, payload }: Actions) => {
    switch (type) {
        case ViewerActionsType.SetInstance:

            return {
                ...state,
                instance: payload
            };

        case ViewerActionsType.SetCurrentFile:

            return {
                ...state,
                currentFile: payload
            };
        case ViewerActionsType.SetContainerElement:

            return {
                ...state,
                containerElement: payload
            };
        case ViewerActionsType.SetIsFileChanged:
            return {
                ...state,
                isFileChanged: payload
            };

        case ViewerActionsType.SetIsNeedListLocked:
            return {
                ...state,
                isNeedListLocked: payload
            }
        case ViewerActionsType.SetSelectedFileData:

            return {
                ...state,
                selectedFileData: payload
            };
        case ViewerActionsType.SetAnnotationsFirstTime:

            return {
                ...state,
                setAnnotationsFirstTime: payload
            };
        case ViewerActionsType.SetIsLoading:

            return {
                ...state,
                isLoading: payload
            };
        case ViewerActionsType.SetIsSaving:

            return {
                ...state,
                isSaving: payload
            };

        case ViewerActionsType.SetFileProgress:

            return {
                ...state,
                fileProgress: payload
            };
            
        case ViewerActionsType.SetShowingConfirmationAlert:
            return {
                ...state,
                showingConfirmationAlert: payload
            };

        case ViewerActionsType.SetFileToChangeWhenUnSaved:
            return {
                ...state,
                fileToChangeWhenUnSaved: payload
            };

        case ViewerActionsType.SetPerformNextAction:
            return {
                ...state,
                performNextAction: payload
            };
        case ViewerActionsType.SetSaveFile:
        return {
            ...state,
            SaveCurrentFile: payload
        };
        case ViewerActionsType.SetDiscardFile:
        return {
            ...state,
            DiscardCurrentFile: payload
        };
        default:
            return state;
    }


}