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
    UpdateDocFile = 'UPDATE_DOC_FILE',
    UpdateWorkbenchFile = 'UPDATE_WORKBENCH_FILE',
    AddFileToTrash = 'ADD_FILE_TO_TRASH',
    AddFileToWorkbench = 'ADD_FILE_TO_WORKBENCH',
    SetFilesToSync = 'SET_Files_To_Sync',
    SetSyncStarted = 'SET_SYNC_STARTED',
    SetUploadFailedDocs = 'SET_UPLOAD_FAILED_DOCS',
    SetFailedDocs = 'SET_FAILED_DOCS',
    SetIsFileDirty = 'SET_IS_FILE_DIRTY',
    SetFileUploadInProgress = 'SET_FILE_UPLOAD_IN_PROGRESS',
    SetIsByteProAuto = "SET_IS_BYTE_PRO_AUTO",
    SetIsDraggingSelf = "SET_IS_DRAGGING_SELF",
    SetCatScrollFreeze = "SET_CAT_SCROLL_FREEZE",
    SetWbScrollFreeze = "SET_WB_SCROLL_FREEZE",
    SetIsDraggingCurrentFile = "SET_IS_DRAGGING_CURRENT_FILE",
    SetImportedFileIds = "SET_IMPORTED_FILE_IDS",
    SetLoanApplication = "SET_LOAN_APPLICATION",
    SetLoanApplicationId = "SET_LOAN_APPLICATION_ID",
    SearchDocumentItems = "SEARCH_DOCUMENT_ITEMS",
    DocSearchTerm = "DOC_SEARCH_TERM",
}

export type DocumentsType = {
    documentItems: DocumentItem[] | [];
    workbenchItems: WorkbenchItem[] | [];
    isDragging: boolean;
    isDraggingCurrentFile: boolean;
    isSynching: string;
    syncStatus: string;
    currentDoc: DocumentRequest | null,
    trashedDoc: DocumentRequest | [],
    filesToSync: any[],
    syncStarted: boolean,
    uploadFailedDocs: DocumentFile | [],
    isFileDirty: boolean,
    fileUploadInProgress: boolean,
    isByteProAuto: boolean,
    isDraggingSelf: DocumentFile
    catScrollFreeze: boolean,
    wbScrollFreeze: boolean,
    importedFileIds: any[]
    loanApplication: any,
    loanApplicationId: string,
    searchdocumentItems: any[] | [];
    docSearchTerm: string;
    
}

export type DocumentActionsPayload = {
    [DocumentActionsType.SetDocumentItems]: DocumentItem[],
    [DocumentActionsType.SetWorkbenchItems]: WorkbenchItem[],
    [DocumentActionsType.SetCurrentDoc]: DocumentRequest,
    [DocumentActionsType.SetTrashedDoc]: DocumentRequest,
    [DocumentActionsType.SetIsDragging]: boolean,
    [DocumentActionsType.SetIsDraggingCurrentFile]: boolean,
    [DocumentActionsType.SetIsSynching]: string,
    [DocumentActionsType.SetSyncStatus]: string,
    [DocumentActionsType.SetFilesToSync]: any[],
    [DocumentActionsType.SetSyncStarted]: boolean,
    [DocumentActionsType.UpdateDocFile]: DocumentRequest,
    [DocumentActionsType.AddFileToTrash]: DocumentRequest,
    [DocumentActionsType.AddFileToWorkbench]: DocumentRequest,
    [DocumentActionsType.UpdateWorkbenchFile]: DocumentRequest,
    [DocumentActionsType.SetUploadFailedDocs]: DocumentFile[],
    [DocumentActionsType.SetFailedDocs]: DocumentFile[],
    [DocumentActionsType.SetIsFileDirty]: boolean
    [DocumentActionsType.AddFileToTrash]: DocumentRequest,
    [DocumentActionsType.AddFileToWorkbench]: DocumentRequest,
    [DocumentActionsType.UpdateWorkbenchFile]: DocumentRequest,
    [DocumentActionsType.SetUploadFailedDocs]: DocumentFile[],
    [DocumentActionsType.SetFailedDocs]: DocumentFile[],
    [DocumentActionsType.SetFileUploadInProgress]: boolean
    [DocumentActionsType.SetIsByteProAuto]: string,
    [DocumentActionsType.SetCatScrollFreeze]: boolean,
    [DocumentActionsType.SetWbScrollFreeze]: boolean,
    [DocumentActionsType.SetIsDraggingSelf]: DocumentFile,
    [DocumentActionsType.SetImportedFileIds]: any[],
    [DocumentActionsType.SetLoanApplication]: {},
    [DocumentActionsType.SetLoanApplicationId]: string,
    [DocumentActionsType.SearchDocumentItems]: any[],
    [DocumentActionsType.DocSearchTerm]: string,
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

        case DocumentActionsType.UpdateDocFile:

            let currentDoc: any = (<DocumentRequest>payload)
            const updatedDocs = state['documentItems']?.map((doc: any) => {
                if (doc.docId === currentDoc?.docId) {
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

        case DocumentActionsType.SetIsDraggingCurrentFile:
            return {
                ...state,
                isDraggingCurrentFile: payload
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
        case DocumentActionsType.SetIsByteProAuto:
            return {
                ...state,
                isByteProAuto: payload
            }
        case DocumentActionsType.SetCatScrollFreeze:
            return {
                ...state,
                catScrollFreeze: payload
            }
        case DocumentActionsType.SetWbScrollFreeze:
            return {
                ...state,
                wbScrollFreeze: payload
            }
        case DocumentActionsType.SetIsDraggingSelf:
            return {
                ...state,
                isDraggingSelf: payload
            }
            return {
                ...state,
                wbScrollFreeze: payload
            }

        case DocumentActionsType.SetImportedFileIds:

            return {
                ...state,
                importedFileIds: payload
            }

        case DocumentActionsType.SetLoanApplication:

            return {
                ...state,
                loanApplication: payload
            
            }
        case DocumentActionsType.SetLoanApplicationId:

            return {
                ...state,
                loanApplicationId: payload
            
            }
        
        case DocumentActionsType.SearchDocumentItems:
            return {
                ...state,
                searchdocumentItems: payload
            }
        case DocumentActionsType.DocSearchTerm:
            return {
                ...state,
                docSearchTerm: payload
            }
        default:
            return state;
    }
}