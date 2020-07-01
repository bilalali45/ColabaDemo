import axios, { AxiosResponse } from "axios";

import { Http } from "../../services/http/Http";
import { Auth } from "../../services/auth/Auth";
import { Endpoints } from "../endpoints/Endpoints";
import { DocumentRequest } from "../../entities/Models/DocumentRequest";
import { UploadedDocuments } from "../../entities/Models/UploadedDocuments";
// import { FileSelected } from "../../components/Home/DocumentRequest/DocumentUpload/DocumentUpload";
import { Document } from "../../entities/Models/Document";
import { DocumentsActionType } from "../reducers/documentReducer";
import { DocumentsEndpoints } from "../endpoints/DocumentsEndpoints";
import { removeSpecialChars } from "../../components/Home/DocumentRequest/DocumentUpload/SelectedDocuments/DocumentItem/DocumentItem";
import { DateFormat } from "../../utils/helpers/DateFormat";
import moment from 'moment';

const todayDate = DateFormat(moment().format('MMM DD, YYYY hh:mm:ss A'), true);


const http = new Http();

export class DocumentActions {
  static async getPendingDocuments(
    loanApplicationId: string,
    tenentId: string
  ) {
    try {
      let res: AxiosResponse<DocumentRequest[]> = await http.get<
        DocumentRequest[]
      >(Endpoints.documents.GET.pendingDocuments(loanApplicationId, tenentId));
      let d = res.data.map((d: DocumentRequest, i: number) => {
        let { id, requestId, docId, docName, docMessage, files } = d;
        let doc = new DocumentRequest(
          id,
          requestId,
          docId,
          docName,
          docMessage,
          files
        );
        // doc.files = null;
        if (doc.files === null || doc.files === undefined) {
          doc.files = [];
        }
        doc.files = doc.files.map((f: Document) => {
          return new Document(
            f.id,
            f.clientName,
            f.fileUploadedOn,
            f.size,
            f.order,
            getDocLogo(f, 'dot'),
            'done'
          );
        });
        // doc.files = [];
        return doc;
      });
      return d;
    } catch (error) {
      console.log(error);
    }
  }

  static async getSubmittedDocuments(
    loanApplicationId: string,
    tenentId: string
  ) {
    try {
      let res: AxiosResponse<UploadedDocuments[]> = await http.get<
        UploadedDocuments[]
      >(
        Endpoints.documents.GET.submittedDocuments(loanApplicationId, tenentId)
      );
      return res.data.map((r) => r);
    } catch (error) {
      console.log(error);
    }
  }

  static async getSubmittedDocumentForView(params: any) {
    try {
      const accessToken = Auth.getAuth();
      const url =
        DocumentsEndpoints.GET.viewDocuments(params.id,
          params.requestId,
          params.docId,
          params.fileId,
          params.tenantId);

      const response = await axios.get(http.createUrl(http.baseUrl, url), {
        params: { ...params },
        responseType: "arraybuffer", //arraybuffer response type important to get the correct response back from server.
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      });

      return response;
    } catch (error) {
      console.log(error);
    }
  }

  static async submitDocuments(currentSelected: DocumentRequest, file: Document, dispatchProgress: Function, loanApplicationId: string,
    tenentId: string) {

    try {
      let res = await http.fetch(
        {
          method: http.methods.POST,
          url: http.createUrl(http.baseUrl, Endpoints.documents.POST.submitDocuments()),
          data: prepareFormData(currentSelected, file),
          onUploadProgress: (e) => {
            let p = Math.floor((e.loaded / e.total) * 100);
            let files: any = currentSelected.files;
            let updatedFiles = files.map((f: Document) => {
              if (f.clientName === file.clientName) {
                f.uploadProgress = p;
                if (p === 100) {
                  f.uploadStatus = 'done';
                }
                return f;
              }
              return f;
            })
            dispatchProgress({ type: DocumentsActionType.AddFileToDoc, payload: updatedFiles })
            // dispatchProgress({type: }
            // setUploadPercent(p);
          },
        },
        {
          Authorization: `Bearer ${Auth.getAuth()}`,
        }
      );
      if (res.status === 200) {
        return await DocumentActions.getPendingDocuments(loanApplicationId, tenentId);
      }
      // setShowProgressBar(false);
    } catch (error) { }
  }



  static async finishDocument(loanApplicationId: string, tenentId: string, data: {}) {
    try {
      let doneRes = await http.put(Endpoints.documents.PUT.finishDocument(), { ...data, tenantId: +tenentId });
      if (doneRes) {
        let remainingPendingDocs = await DocumentActions.getPendingDocuments(loanApplicationId, tenentId);
        if (remainingPendingDocs) {
          return remainingPendingDocs;
        }
      }
    } catch (error) {

    }
  }
}

const prepareFormData = (currentSelected: DocumentRequest, file: Document) => {

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
  data.append("tenantId", Auth.getTenantId());

  return data;
}


export const isFileAllowed = (file) => {
  if (!file) return null;
  const allowedExtensions = "pdf, jpg, jpeg, png";
  const allowedSize = 15000;
  let ext = file.type.split('/')[1]
  if (allowedExtensions.includes(ext) && file.size / 1000 < allowedSize) {
    return true;
  }
  return false;

}

export const getExtension = (file, splitBy) => {
  if (splitBy === 'dot') {
    return file.clientName.split('.')[1]
  } else {
    return file?.type.split('/')[1];
  }
}


export const getDocLogo = (file, splitBy) => {
  let ext = getExtension(file, splitBy);
  if (ext === 'pdf') {
    return "far fa-file-pdf"
  }
  else {
    return "far fa-file-image"
  }
}

export const removeDefaultExt = (fileName: string) => {
  let splitData = fileName.split('.');
  let onlyName = "";
  for (let i = 0; i < splitData.length - 1; i++) {
    if (i != splitData.length - 2)
      onlyName += splitData[i] + '.';
    else
      onlyName += splitData[i];
  }
  return onlyName != "" ? onlyName : fileName;
}
 
export const sortByDate = (array: any[]) => {
  return array.sort((a, b) => {
    let first = new Date(a.fileUploadedOn);
    let second = new Date(b.fileUploadedOn);
    return first > second ? -1 : first < second ? 1 : 0;
  })
}

export const removeActualFile = (fileName: string, prevFiles: Document[], dispatch: Function) => {
  prevFiles.filter(f => {
    if (f?.clientName.split('.')[0] !== fileName) {
      return f;
    }
  });
  dispatch({ type: DocumentsActionType.AddFileToDoc, payload: prevFiles });
}

export const updateName = (name, type) => {
  let newName = removeDefaultExt(name);
  var uniq = 'rsft' + (new Date()).getTime();
  return newName + uniq + '.' + type.split("/")[1];
}

export const updateFiles = (files: File[], prevFiles: Document[], dispatch: Function) => {
  let allSelectedFiles: Document[] = [...prevFiles];
  for (let f of files) {
    if (isFileAllowed(f)) {
      var newName = f.name;
      var isNameExist = prevFiles.find(i => removeDefaultExt(i.clientName) === removeSpecialChars(removeDefaultExt(f.name)))
      if (isNameExist) {
        newName = updateName(f.name, f.type)
      }
      const selectedFile = new Document("", newName, todayDate, 0, 0, getDocLogo(f, 'slash'), 'pending', f);
      selectedFile.editName = true;
      allSelectedFiles.push(selectedFile);
    }
  }
  dispatch({ type: DocumentsActionType.AddFileToDoc, payload: allSelectedFiles });
};