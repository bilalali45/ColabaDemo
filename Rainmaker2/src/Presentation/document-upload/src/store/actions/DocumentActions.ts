import axios, { AxiosResponse } from "axios";

//import { Http } from "../../services/http/Http";
import { Http } from 'rainsoft-js';
import { Auth } from "../../services/auth/Auth";
import { Endpoints } from "../endpoints/Endpoints";
import { DocumentRequest } from "../../entities/Models/DocumentRequest";
import { UploadedDocuments } from "../../entities/Models/UploadedDocuments";
import { Document } from "../../entities/Models/Document";
import { DocumentsEndpoints } from "../endpoints/DocumentsEndpoints";
import { FileUpload } from "../../utils/helpers/FileUpload";

//const http = new Http();
const http = new Http(Auth.getAuth());

export class DocumentActions {
  static async getPendingDocuments(
    loanApplicationId: string,
    tenentId: string
  ) {
    try {
      let res: AxiosResponse<DocumentRequest[]> = await http.get<
        DocumentRequest[]
      >(Endpoints.documents.GET.pendingDocuments(loanApplicationId, tenentId));
      // res.data = [
      //   new DocumentRequest(
      //     '1',
      //     '2',
      //     '3',
      //     'testing',
      //     '',
      //     []
      //   )
      // ]
      
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
            FileUpload.getDocLogo(f, 'dot'),
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