import { Http } from "../../services/http/Http";
import { Auth } from "../../services/auth/Auth";
import { Endpoints } from "../endpoints/Endpoints";
import { DocumentRequest } from "../../entities/Models/DocumentRequest";
import { AxiosResponse } from "axios";

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
      let res: any = await http.get(Endpoints.documents.GET.submittedDocuments(loanApplicationId, tenentId));
      return res.data;
    } catch (error) {
      console.log(error);
    }
  }

  static async submitDocuments() {

  }

}

