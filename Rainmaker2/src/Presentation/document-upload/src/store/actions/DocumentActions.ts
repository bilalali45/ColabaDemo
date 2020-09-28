import axios, { AxiosResponse } from "axios";
import { Http } from "rainsoft-js";
import { Auth } from "../../services/auth/Auth";
import { Endpoints } from "../endpoints/Endpoints";
import { DocumentRequest } from "../../entities/Models/DocumentRequest";
import { UploadedDocuments } from "../../entities/Models/UploadedDocuments";
import { Document } from "../../entities/Models/Document";
import { DocumentsEndpoints } from "../endpoints/DocumentsEndpoints";
import { FileUpload } from "../../utils/helpers/FileUpload";

export class DocumentActions {
  static async getPendingDocuments(loanApplicationId: string) {
    try {
      let res: AxiosResponse<DocumentRequest[]> = await Http.get<
        DocumentRequest[]
      >(Endpoints.documents.GET.pendingDocuments(loanApplicationId));

      let d = res?.data?.map((d: DocumentRequest, i: number) => {
        let {
          id,
          requestId,
          docId,
          docName,
          docMessage,
          files,
          isRejected,
        } = d;
        let doc = new DocumentRequest(
          id,
          requestId,
          docId,
          docName,
          docMessage,
          files,
          isRejected
        );

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
            FileUpload.getDocLogo(f, "dot"),
            "done"
          );
        });

        return doc;
      });
      return d;
    } catch (error) {
      console.log(error);
    }
  }

  static async getSubmittedDocuments(loanApplicationId: string) {
    try {
      let res: AxiosResponse<UploadedDocuments[]> = await Http.get<
        UploadedDocuments[]
      >(Endpoints.documents.GET.submittedDocuments(loanApplicationId));
      return res.data.map((r) => r);
    } catch (error) {
      console.log(error);
    }
  }

  static documentViewCancelToken: any = axios.CancelToken.source();
  static async getSubmittedDocumentForView(params: any) {
    DocumentActions.documentViewCancelToken.cancel();
    DocumentActions.documentViewCancelToken = axios.CancelToken.source();
    try {
      const accessToken = Auth.getAuth();
      const url = DocumentsEndpoints.GET.viewDocuments(
        params.id,
        params.requestId,
        params.docId,
        params.fileId
      );

      const response = await axios.get(Http.createUrl(Http.baseUrl, url), {
        cancelToken: DocumentActions.documentViewCancelToken.token,
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

  static async finishDocument(loanApplicationId: string, data: {}) {
    try {
      let doneRes = await Http.put(Endpoints.documents.PUT.finishDocument(), {
        ...data,
      });
      if (doneRes) {
        let remainingPendingDocs = await DocumentActions.getPendingDocuments(
          loanApplicationId
        );
        if (remainingPendingDocs) {
          return remainingPendingDocs;
        }
      }
    } catch (error) {}
  }
}
