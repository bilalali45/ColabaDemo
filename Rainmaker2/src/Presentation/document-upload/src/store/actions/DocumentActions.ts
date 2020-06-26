import { Http } from "../../services/http/Http";
import { Auth } from "../../services/auth/Auth";
import { Endpoints } from "../endpoints/Endpoints";
import { DocumentRequest } from "../../entities/Models/DocumentRequest";
import { AxiosResponse } from "axios";
import { UploadedDocuments } from "../../entities/Models/UploadedDocuments";

const http = new Http();


export class DocumentActions {

  static async getPendingDocuments(loanApplicationId: string, tenentId: string) {
    try {
      let res: AxiosResponse<DocumentRequest[]> = await http.get<DocumentRequest[]>(Endpoints.documents.GET.pendingDocuments(loanApplicationId, tenentId));
      return res.data.map(r => r);
    } catch (error) {
      console.log(error);
    }
  }

  static async getSubmittedDocuments(loanApplicationId: string, tenentId: string) {
    try {
      let res: AxiosResponse<UploadedDocuments[]> = await http.get<UploadedDocuments[]>(Endpoints.documents.GET.submittedDocuments(loanApplicationId, tenentId));
      console.log('getSubmittedDocuments',res);
      return res.data.map(r => r);
    } catch (error) {
      console.log(error);
    }
  }

  static async submitDocuments() {

  }

  

}

