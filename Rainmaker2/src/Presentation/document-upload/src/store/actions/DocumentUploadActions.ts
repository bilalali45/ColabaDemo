import { Http } from "rainsoft-js";

import { DocumentRequest } from "../../entities/Models/DocumentRequest";
import { Document } from "../../entities/Models/Document";
import { DocumentsActionType } from "../reducers/documentReducer";
import { FileUpload } from "../../utils/helpers/FileUpload";
import { Auth } from "../../services/auth/Auth";
import { Endpoints } from "../endpoints/Endpoints";
import { ApplicationEnv } from "../../utils/helpers/AppEnv";
import { Rename } from "../../utils/helpers/rename";

export class DocumentUploadActions {
  static async submitDocuments(
    currentSelected: DocumentRequest,
    file: Document,
    dispatchProgress: Function,
    loanApplicationId: string,
    type?: string
  ) {
    try {
      let fileData = await DocumentUploadActions.prepareFormData(currentSelected, file, loanApplicationId, type);
      let fileError = fileData.get('fileError');
      
      if(fileError) {
        dispatchProgress({
          type: DocumentsActionType.AddFileToCategoryDocs,
          payload: currentSelected?.files?.map(item => {         
            if(item?.clientName === file?.clientName) {
              item.uploadStatus = 'failed';
              item.notAllowedReason = 'Failed';
              item.failedReason =  String(fileError);
            }
            return item;
          }),
        });
        return;
      }

     let res = await Http.fetch(
        {
          method: Http.methods.POST,
          url: Http.createUrl(Http.baseUrl, type === "WithoutReq" ? Endpoints.documents.POST.submitByBorrower() : Endpoints.documents.POST.submitDocuments()),
          cancelToken: file.uploadReqCancelToken.token,
          data: fileData,
          onUploadProgress: (event) => {
            let percentage = Math.floor((event.loaded / event.total) * 100);
            let files: any = currentSelected.files;
            let updatedFiles = files.map((f: Document) => {
              if (f.clientName === file.clientName) {
                f.uploadProgress = percentage;
                if (percentage === 100) {
                  f.uploadStatus = "done";
                }
                return f;
              }
              return f;
            });
            if(type === "WithoutReq"){
              dispatchProgress({
                type: DocumentsActionType.AddFileToCategoryDocs,
                payload: updatedFiles,
              });
            }else{
              dispatchProgress({
                type: DocumentsActionType.AddFileToDoc,
                payload: updatedFiles,
              });
            }          
          },
        },
        {
          Authorization: `Bearer ${Auth.getAuth()}`,
        }
      );
      return res;
    } catch (error) {
      console.log('error', error.response);
      if(type === "WithoutReq"){
        dispatchProgress({
          type: DocumentsActionType.AddFileToCategoryDocs,
          payload: currentSelected?.files?.map(f => {         
            let err = error?.response?.data || 'Something went wrong. Please try again.';
            if(f?.clientName === file?.clientName) {
              f.uploadStatus = 'failed';
              f.notAllowedReason = 'Failed';
              f.failedReason =  err.Message? err.Message : err;
            }
            return f;
          }),
        });
      }else{
        dispatchProgress({
          type: DocumentsActionType.AddFileToDoc,
          payload: currentSelected?.files?.map(f => {
            let err = error?.response?.data || 'Something went wrong. Please try again.';
            if(f?.clientName === file?.clientName) {
              f.uploadStatus = 'failed';
              f.notAllowedReason = 'Failed';
              f.failedReason =  err.Message? err.Message : err;
            }
            return f;
          }),
        });
      }      
    }
  }

  static async prepareFormData(currentSelected: DocumentRequest, file: Document, loanApplicationId: string, type?: string) {
    
    const data = new FormData();
    let fields ;
    if(type === "WithoutReq"){
      fields = ["loanApplicationId", "displayName", "docId"];
    }else{
       fields = ["id", "requestId", "docId"];
    }
    
    if (file.file) {
      try {
        let fileArrayBuffer = await file.file.arrayBuffer()
        let blob = new Blob([new Uint8Array(fileArrayBuffer)], { type: file.file.type });
        data.append("files", blob, `${file.clientName}`);
      } catch (error) {
        console.log("File could not be converted into blob", error);
        data.append('fileError', error);        
      }
    }

    if(type === "WithoutReq"){
      data.append(fields[0], loanApplicationId);
      data.append(fields[1], currentSelected.docName);
      data.append(fields[2], currentSelected.docId);
    }else{
      for (const field of fields) {
        const value = currentSelected[field];
        data.append(field, value);
      }
    }
    data.append("order", JSON.stringify(file.documentOrder));

    return data;
  }

  static async updateFiles(
    files: File[],
    prevFiles: Document[],
    dispatch: Function,
    setFileLimitError: Function,
    type?: string
  ) {
    let allSelectedFiles: Document[] = [...prevFiles];
    let counter = 0;
    for (let f of [...files]) {

      if(!f.size){
        console.log("Uploaded file size is not valid.", f, f?.name, f?.size);
      }
      
      if (allSelectedFiles.length >= ApplicationEnv.MaxDocumentCount) {
        setFileLimitError({ value: true });
        break;
      }
      
      let selectedFile = new Document(
        "",
        FileUpload.removeSpecialChars(f.name),
        FileUpload.todayDate(),
        0,
        0,
        FileUpload.getDocLogo(f, "slash"),
        "pending",
        f
      );
      selectedFile = Rename.rename(allSelectedFiles, selectedFile);

      if(!f.type) {
        selectedFile.notAllowedReason = "Invalid";
        selectedFile.notAllowed = true;
      }
        
      if (!FileUpload.isSizeAllowed(f)) {
        selectedFile.notAllowedReason = "FileSize";
        selectedFile.notAllowed = true;
      }

      if ((await FileUpload.isTypeAllowed(f)) === false) {
        selectedFile.notAllowedReason = "FileType";
        selectedFile.notAllowed = true;
      }

      selectedFile.editName = true;

      allSelectedFiles.push(selectedFile);

      if (counter === 0) {
        selectedFile.focused = true;
      } else {
        selectedFile.focused = false;
      }
      counter++;
    }
    if(type === "WithoutReq"){
      dispatch({
        type: DocumentsActionType.AddFileToCategoryDocs,
        payload: allSelectedFiles,
      });
    }else{
      dispatch({
        type: DocumentsActionType.AddFileToDoc,
        payload: allSelectedFiles,
      });
    }
    
  }

  static removeActualFile(
    fileName: string,
    prevFiles: Document[],
    dispatch: Function,
    type?: string
  ) {
    
    prevFiles = prevFiles.filter((f) => {
      if (f?.clientName.split(".")[0] !== fileName) {
        return f;
      }
    });
    if(type === "WithoutReq"){
      dispatch({
        type: DocumentsActionType.AddFileToCategoryDocs,
        payload: prevFiles,
      });
    }else{
      dispatch({
        type: DocumentsActionType.AddFileToDoc,
        payload: prevFiles,
      });
    }
  }
}
