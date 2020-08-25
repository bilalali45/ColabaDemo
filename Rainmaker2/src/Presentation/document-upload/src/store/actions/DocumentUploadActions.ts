import { Http } from "rainsoft-js";
import { DocumentRequest } from "../../entities/Models/DocumentRequest";
import { Document } from "../../entities/Models/Document";
import { DocumentsActionType } from "../reducers/documentReducer";
import { FileUpload } from "../../utils/helpers/FileUpload";
import { Auth } from "../../services/auth/Auth";
import { Endpoints } from "../endpoints/Endpoints";
import { ApplicationEnv } from "../../utils/helpers/AppEnv";

//const http = new Http();
const http = new Http();

export class DocumentUploadActions {
  static async submitDocuments(
    currentSelected: DocumentRequest,
    file: Document,
    dispatchProgress: Function,
    loanApplicationId: string
  ) {
    try {
      let res = await http.fetch(
        {
          method: http.methods.POST,
          url: http.createUrl(
            http.baseUrl,
            Endpoints.documents.POST.submitDocuments()
          ),
          cancelToken: file.uploadReqCancelToken.token,
          data: DocumentUploadActions.prepareFormData(currentSelected, file),
          onUploadProgress: (e) => {
            let p = Math.floor((e.loaded / e.total) * 100);
            let files: any = currentSelected.files;
            let updatedFiles = files.map((f: Document) => {
              if (f.clientName === file.clientName) {
                f.uploadProgress = p;
                if (p === 100) {
                  f.uploadStatus = "done";
                }
                return f;
              }
              return f;
            });
            dispatchProgress({
              type: DocumentsActionType.AddFileToDoc,
              payload: updatedFiles,
            });
          },
        },
        {
          Authorization: `Bearer ${Auth.getAuth()}`,
        }
      );
    } catch (error) {
      console.log('-------------->Upload errors------------>',error)
    }
  }

  static prepareFormData(currentSelected: DocumentRequest, file: Document) {
    const data = new FormData();

    let fields = ["id", "requestId", "docId"];

    if (file.file) {
      data.append("files", file.file, `${file.clientName}`);
    }

    for (const field of fields) {
      const value = currentSelected[field];
      data.append(field, value);
    }

    data.append("order", JSON.stringify(file.documentOrder));

    return data;
  }

  static async updateFiles(
    files: File[],
    prevFiles: Document[],
    dispatch: Function,
    setFileLimitError: Function
  ) {
    let allSelectedFiles: Document[] = [...prevFiles];
    let counter = 0;
    for (let f of files) {
      if (allSelectedFiles.length >= ApplicationEnv.MaxDocumentCount) {
        setFileLimitError({ value: true });
        break;
      }
      var newName = f.name.replace(/\s/g,'');
     
      var countArray = FileUpload.checkName(prevFiles, f);
    
      if (countArray[0] != 0) {
        newName = FileUpload.updateName(f.name, f.type, countArray).replace(/\s/g,'');;
      }

      const selectedFile = new Document(
        "",
        newName,
        FileUpload.todayDate(),
        0,
        0,
        FileUpload.getDocLogo(f, "slash"),
        "pending",
        f
      );
      if (!FileUpload.isSizeAllowed(f)) {
        selectedFile.notAllowedReason = "FileSize";
        selectedFile.notAllowed = true;
      }

      if (!(await FileUpload.isTypeAllowed(f))) {
        selectedFile.notAllowedReason = "FileType";
        selectedFile.notAllowed = true;
      }

      selectedFile.editName = true;
      allSelectedFiles.push(selectedFile);
      // }

      if (counter === 0) {
        selectedFile.focused = true;
      } else {
        selectedFile.focused = false;
      }
      counter++;
    }
    dispatch({
      type: DocumentsActionType.AddFileToDoc,
      payload: allSelectedFiles,
    });
  }

  static removeActualFile(
    fileName: string,
    prevFiles: Document[],
    dispatch: Function
  ) {
    prevFiles.filter((f) => {
      if (f?.clientName.split(".")[0] !== fileName) {
        return f;
      }
    });
    dispatch({ type: DocumentsActionType.AddFileToDoc, payload: prevFiles });
  }
}
