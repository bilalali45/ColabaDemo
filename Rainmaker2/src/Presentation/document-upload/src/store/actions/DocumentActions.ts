import { Http } from "../../services/http/Http";
import { Auth } from "../../services/auth/Auth";
import { Endpoints } from "../endpoints/Endpoints";
import { DocumentRequest } from "../../entities/Models/DocumentRequest";
import { AxiosResponse } from "axios";
import { UploadedDocuments } from "../../entities/Models/UploadedDocuments";
// import { FileSelected } from "../../components/Home/DocumentRequest/DocumentUpload/DocumentUpload";
import { Document } from "../../entities/Models/Document";

const http = new Http();


export class DocumentActions {

  static async getPendingDocuments(loanApplicationId: string, tenentId: string) {
    try {
      let res: AxiosResponse<DocumentRequest[]> = await http.get<DocumentRequest[]>(Endpoints.documents.GET.pendingDocuments(loanApplicationId, tenentId));
      let d = res.data.map((d: DocumentRequest) => {
        // debugger
        let { id, requestId, docId, docName, docMessage, files } = d;
        let doc = new DocumentRequest(id, docId, requestId, docName, docMessage, files);
        doc.files = doc.files.map((f: Document) => {
          return new Document(f.id, f.clientName, f.fileUploadedOn, f.size, f.order);
        });
        return doc;
      });
      console.log(d);
      return d;
      console.log(d);
      // return res.data;

    } catch (error) {
      console.log(error);
    }
  }

  static async getSubmittedDocuments(loanApplicationId: string, tenentId: string) {
    try {
      let res: AxiosResponse<UploadedDocuments[]> = await http.get<UploadedDocuments[]>(Endpoints.documents.GET.submittedDocuments(loanApplicationId, tenentId));
      console.log('getSubmittedDocuments', res);
      return res.data.map(r => r);
    } catch (error) {
      console.log(error);
    }
  }

  static async submitDocuments() {

  }

}

