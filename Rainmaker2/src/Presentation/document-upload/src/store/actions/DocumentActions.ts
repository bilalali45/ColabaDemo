import axios, { AxiosResponse } from "axios";

import { Http } from "../../services/http/Http";
import { Auth } from "../../services/auth/Auth";
import { Endpoints } from "../endpoints/Endpoints";
import { DocumentRequest } from "../../entities/Models/DocumentRequest";
import { UploadedDocuments } from "../../entities/Models/UploadedDocuments";
// import { FileSelected } from "../../components/Home/DocumentRequest/DocumentUpload/DocumentUpload";
import { Document } from "../../entities/Models/Document";
import { DocumentsActionType } from "../reducers/documentReducer";

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
        // if (i === 0) {
        //   let { id, requestId, docId, docName, docMessage, files } = d;
        //   let doc = new DocumentRequest(
        //     id,
        //     requestId,
        //     docId,
        //     docName,
        //     docMessage,
        //     []
        //   );
        //   return doc;
        // }
        // debugger
        let { id, requestId, docId, docName, docMessage, files } = d;
        let doc = new DocumentRequest(
          id,
          requestId,
          docId,
          docName,
          docMessage,
          files
        );
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
        "https://Alphamaingateway.rainsoftfn.com/api/documentmanagement/file/view";

      const response = await axios.get(url, {
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

  static async submitDocuments(currentSelected: DocumentRequest, file: Document, dispatchProgress: Function) {

    try {
      let res = await http.fetch(
        {
          method: http.methods.POST,
          url: http.createUrl(http.baseUrl, Endpoints.documents.POST.submitDocuments()),
          data: prepareFormData(currentSelected, file),
          onUploadProgress: (e) => {
            let p = Math.floor((e.loaded / e.total) * 100);
            let files: Document[] = currentSelected.files;
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
      // setShowProgressBar(false);
    } catch (error) { }
  }



  static async finishDocument(data: {}) {
    try {
      await http.put(Endpoints.documents.PUT.finishDocument(), data);
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
  const allowedSize = 70000;
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