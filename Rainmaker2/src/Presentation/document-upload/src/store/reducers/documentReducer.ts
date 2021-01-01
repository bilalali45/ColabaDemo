import { Actions, ActionMap } from "./reducers";
import { DocumentRequest } from "../../entities/Models/DocumentRequest";
import { UploadedDocuments } from "../../entities/Models/UploadedDocuments";
import { FileUpload } from "../../utils/helpers/FileUpload";
import { Document } from "../../entities/Models/Document";
import { CategoryDocument } from "../../entities/Models/CategoryDocument";

export enum DocumentsActionType {
    FetchPendingDocs = 'FETCH_PENDING_DOCS',
    FetchSubmittedDocs = 'FETCH_SUBMITTED_DOCS',
    SetCurrentDoc = 'SET_CURRENT_DOC',
    AddFileToDoc = 'ADD_FILE_TO_DOC',
    SetCategoryDocuments = "SET_CATEGORY_DOCUMENTS",
    AddFileToCategoryDocs = "ADD_FILE_TO_CATEGORY_DOCS",
    SubmitButtonPressed = "SUBMITBUTTONPRESSED"
}

export type DocumentsType = {
    pendingDocs: DocumentRequest[] | null,
    currentDoc: DocumentRequest | null,
    submittedDocs: UploadedDocuments[] | null,
    categoryDocuments: CategoryDocument[],
    submitBtnPressed: { value: Boolean }
}


type DocumentsActionPayload = {
    [DocumentsActionType.FetchPendingDocs]: DocumentRequest[],
    [DocumentsActionType.SetCurrentDoc]: DocumentRequest,
    [DocumentsActionType.AddFileToDoc]: Document[],
    [DocumentsActionType.FetchSubmittedDocs]: UploadedDocuments[],
    [DocumentsActionType.SetCategoryDocuments]: CategoryDocument[],
    [DocumentsActionType.AddFileToCategoryDocs]: CategoryDocument[],
    [DocumentsActionType.SubmitButtonPressed]: { value: Boolean }
}

export type DocumentsActions = ActionMap<DocumentsActionPayload>[keyof ActionMap<DocumentsActionPayload>];

export const documentsReducer = (state: DocumentsType | {}, { type, payload }: Actions) => {
    switch (type) {
        case DocumentsActionType.FetchPendingDocs:
            return {
                ...state,
                pendingDocs: payload
            };

        case DocumentsActionType.SetCurrentDoc:
            
            return {
                ...state,
                currentDoc: payload
            };

        case DocumentsActionType.AddFileToDoc:
            const pdocs = state['pendingDocs']?.map((pd: any) => {              
                if (pd?.docId === state['currentDoc']?.docId) {
                    if (Array.isArray(payload)) {
                        pd.files = payload;
                    }
                    return pd;
                }
                return pd;
            });
            return {
                ...state,
                pendingDocs: pdocs
            }

            case DocumentsActionType.AddFileToCategoryDocs:
                let currentDocs = state['currentDoc'];
                const catDocs = state['categoryDocuments']?.map((cd: any) => {                  
                  if(cd.documents === undefined){
                    if (cd?.docTypeId === state['currentDoc']?.docId) {
                        if (Array.isArray(payload)) {
                            cd.files = payload;
                            currentDocs.files = payload;
                        }
                        return cd;
                    }
                    return cd;
                  }else{
                    for (const item of cd.documents) {
                        if (item?.docTypeId === state['currentDoc']?.docId) {
                            if (Array.isArray(payload)) {
                                item.files = payload;
                                currentDocs.files = payload;
                            }
                            return cd;
                        }
                        
                    }
                    return cd;
                  }                    
                });
                return {
                    ...state,
                    categoryDocuments: catDocs,
                    currentDoc:currentDocs
                }

        case DocumentsActionType.FetchSubmittedDocs:
            return {
                ...state,
                submittedDocs: payload
            };
        case DocumentsActionType.SetCategoryDocuments:
            return {
                ...state,
                categoryDocuments: payload
            }

        case DocumentsActionType.SubmitButtonPressed:
            return {
                ...state,
                submitBtnPressed: payload
            }


        default:
            return state;
    }
}