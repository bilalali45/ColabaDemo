import { DocumentFile } from "../../Models/DocumentFile";
import { DocumentItem } from "../../Models/DocumentItem";
import { DocumentRequest } from "../../Models/DocumentRequest";
import { WorkbenchItem } from "../../Models/WorkBenchItem";
import DocumentActions from "../actions/DocumentActions";
import { ActionMap, Actions } from "./reducers"

export enum DocumentActionsType {
    SetDocumentItems = "SET_DOCUMENT_ITEMS",
    SetWorkbenchItems = "SET_WORKBENCH_ITEMS",
    SetIsDragging = 'SET_IS_DRAGGING',
    SetIsSynching = 'SET_IS_SYNCING',
    SetSyncStatus = 'SET_SYNC_STATUS',
    SetCurrentDoc = 'SET_CURRENT_DOC',
    SetTrashedDoc = 'SET_TRASHED_DOC',
    AddFileToDoc = 'ADD_FILE_TO_DOC',
    UpdateDocFile = 'UPDATE_DOC_FILE',
    UpdateWorkbenchFile = 'UPDATE_WORKBENCH_FILE',
    AddFileToTrash = 'ADD_FILE_TO_TRASH',
    AddFileToWorkbench = 'ADD_FILE_TO_WORKBENCH',
    SetFilesToSync = 'SET_Files_To_Sync',
    SetSyncStarted = 'SET_SYNC_STARTED',
    SetUploadFailedDocs = 'SET_UPLOAD_FAILED_DOCS',
    SetFailedDocs = 'SET_FAILED_DOCS',
    SetIsFileDirty = 'SET_IS_FILE_DIRTY',
    SetFileUploadInProgress = 'SET_FILE_UPLOAD_IN_PROGRESS'
}

export type DocumentsType = {
    documentItems: DocumentItem[] | [];
    workbenchItems: WorkbenchItem[] | [];
    isDragging: boolean;
    isSynching: string;
    syncStatus: string;
    currentDoc: DocumentRequest | null,
    trashedDoc: DocumentRequest | [],
    filesToSync: any[],
    syncStarted: boolean,
    uploadFailedDocs: DocumentFile | [],
    isFileDirty: boolean
    fileUploadInProgress:boolean
}

export type DocumentActionsPayload = {
    [DocumentActionsType.SetDocumentItems]: DocumentItem[],
    [DocumentActionsType.SetWorkbenchItems]: WorkbenchItem[],
    [DocumentActionsType.SetCurrentDoc]: DocumentRequest,
    [DocumentActionsType.SetTrashedDoc]: DocumentRequest,
    [DocumentActionsType.SetIsDragging]: boolean,
    [DocumentActionsType.SetIsSynching]: string,
    [DocumentActionsType.SetSyncStatus]: string,
    [DocumentActionsType.SetFilesToSync]: any[],
    [DocumentActionsType.SetSyncStarted]: boolean,
    [DocumentActionsType.AddFileToDoc]: DocumentRequest,
    [DocumentActionsType.UpdateDocFile]: DocumentRequest,
    [DocumentActionsType.AddFileToTrash]: DocumentRequest,
    [DocumentActionsType.AddFileToWorkbench]: DocumentRequest,
    [DocumentActionsType.UpdateWorkbenchFile]: DocumentRequest,
    [DocumentActionsType.SetUploadFailedDocs]: DocumentFile[],
    [DocumentActionsType.SetFailedDocs]: DocumentFile[],
    [DocumentActionsType.SetIsFileDirty]: boolean
    [DocumentActionsType.AddFileToTrash]:DocumentRequest, 
    [DocumentActionsType.AddFileToWorkbench]:DocumentRequest, 
    [DocumentActionsType.UpdateWorkbenchFile]:DocumentRequest,
    [DocumentActionsType.SetUploadFailedDocs]:DocumentFile[],
    [DocumentActionsType.SetFailedDocs]:DocumentFile[],
    [DocumentActionsType.SetFileUploadInProgress]:boolean
}

export type DocumentsActions = ActionMap<DocumentActionsPayload>[keyof ActionMap<DocumentActionsPayload>];

export const documentsReducer = (state: DocumentsType | {} | any, { type, payload }: Actions) => {
    switch (type) {
        case DocumentActionsType.SetDocumentItems:
            return {
                ...state,
                documentItems: payload
            }

        case DocumentActionsType.SetWorkbenchItems:
            return {
                ...state,
                workbenchItems: payload
            }
        case DocumentActionsType.SetCurrentDoc:
            return {
                ...state,
                currentDoc: payload
            };

        case DocumentActionsType.SetTrashedDoc:
            return {
                ...state,
                trashedDoc: payload
            };

        case DocumentActionsType.SetFailedDocs:
            return {
                ...state,
                uploadFailedDocs: payload
            };
        case DocumentActionsType.AddFileToDoc:

            return {
                ...state,
                documentItems: payload
            };

        case DocumentActionsType.UpdateDocFile:
            const updatedDocs = state['documentItems']?.map((doc: any) => {
                if (doc.docId === state['currentDoc']?.docId) {
                    doc = payload
                }
                return doc;
            })

            return {
                ...state,
                documentItems: updatedDocs
            };




        case DocumentActionsType.AddFileToTrash:

            return {
                ...state,
                trashedDoc: payload
            };

        case DocumentActionsType.AddFileToWorkbench:


            return {
                ...state,
                workbenchItems: payload
            };

        case DocumentActionsType.UpdateWorkbenchFile:

            return {
                ...state,
                workbenchItems: payload
            };

        case DocumentActionsType.SetIsDragging:
            return {
                ...state,
                isDragging: payload
            }

        case DocumentActionsType.SetIsSynching:
            return {
                ...state,
                isSynching: payload
            }

        case DocumentActionsType.SetSyncStatus:
            return {
                ...state,
                syncStatus: payload
            }
        case DocumentActionsType.SetFilesToSync:
            return {
                ...state,
                filesToSync: payload
            }
        case DocumentActionsType.SetSyncStarted:
            return {
                ...state,
                syncStarted: payload
            }
        case DocumentActionsType.SetUploadFailedDocs:

            let failedDocs = [...state['uploadFailedDocs'], payload]
            return {
                ...state,
                uploadFailedDocs: failedDocs
            }

        case DocumentActionsType.SetIsFileDirty:
            return {
                ...state,
                isFileDirty: payload
            }

            case DocumentActionsType.SetFileUploadInProgress:
                return {
                    ...state,
                    fileUploadInProgress: payload
                }
        default:
            return state;
    }
}