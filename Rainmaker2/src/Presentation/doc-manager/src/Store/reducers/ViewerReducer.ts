import Instance from "pspdfkit/dist/types/typescript/Instance";
import { RefObject } from "react";
import { CurrentInView } from "../../Models/CurrentInView";
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


export enum ViewerActionsType {
    SetInstance = "SET_INSTANCE",
    SetThumbnailItems = "SET_THUMBNAIL_ITEMS",
    SetCurrentFile = "SET_CURRENT_FILE",
    SetContainerElement = "SET_CONTAINER_ELEMENT",
    SetIsFileChanged = "SET_IS_FILE_CHANGED",
    SetIsNeedListLocked = "SET_IS_NEED_LIST_LOCKED",
    SetSelectedFileData = "SET_SELECTED_FILE_DATA",
    SetIsLoading = "SET_IS_LOADING"

}

export type ViewerType = {
    instance: Instance;
    thumbnailItems: ThumbnailItem[] | [];
    currentFile: CurrentInView | {};
    containerElement: RefObject<HTMLDivElement>;
    isFileChanged: boolean;
    isNeedListLocked: NeedListLock;
    selectedFileData: SelectedFile;
    isLoading:boolean;
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
    [ViewerActionsType.SetIsLoading]:boolean;

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
            case ViewerActionsType.SetIsLoading:

                return {
                    ...state,
                    isLoading: payload
                };
        default:
            return state;
    }


}