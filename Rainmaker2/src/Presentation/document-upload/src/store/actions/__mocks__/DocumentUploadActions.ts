import { Http } from "rainsoft-js";
import { Document } from "../../../entities/Models/Document";
import { DocumentRequest } from "../../../entities/Models/DocumentRequest";
import { ApplicationEnv } from "../../../utils/helpers/AppEnv";
import { FileUpload } from "../../../utils/helpers/FileUpload";
import { Rename } from "../../../utils/helpers/rename";
import { DocumentsActionType } from "../../reducers/documentReducer";


const http = new Http();

export class DocumentUploadActions {

  static percent : any = 0.1;

  static async submitDocuments(currentSelected: DocumentRequest, file: Document, dispatchProgress: Function, loanApplicationId: string) {

    let p = Math.floor(this.percent * 100);
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

      if (counter === 0) {
        selectedFile.focused = true;
      } else {
        selectedFile.focused = false;
      }
      counter++;
    }
    // console.log(allSelectedFiles);
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
    prevFiles = prevFiles.filter((f) => {
      if (f?.clientName.split(".")[0] !== fileName) {
        return f;
      }
    });
    dispatch({ type: DocumentsActionType.AddFileToDoc, payload: prevFiles });
  }
}
