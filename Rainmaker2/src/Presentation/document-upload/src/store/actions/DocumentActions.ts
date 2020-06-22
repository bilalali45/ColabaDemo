import { Http } from "../../services/http/Http";
import { Auth } from "../../services/auth/Auth";
import { Endpoints } from "../endpoints/Endpoints";
import { DocumentRequest } from "../../entities/Models/DocumentRequest";
import { AxiosResponse } from "axios";

const http = new Http();

export const statusText = {
  COMPLETED: 'COMPLETED',
  CURRENT: 'CURRENT STEP',
  UPCOMMING: 'UPCOMING'
}

export class DocumentActions {

  static async getPendingDocuments(loanApplicationId: string, tenentId: string) {
    try {
      let res: AxiosResponse<DocumentRequest[]> = await http.get<DocumentRequest[]>(Endpoints.documents.GET.pendingDocuments(loanApplicationId, tenentId));
      console.log(res);
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
  static async getDocumentsStatus(loanApplicationId: string, tenentId: string) {
    try {
      let res: any = await http.get(Endpoints.documents.GET.documentsProgress(loanApplicationId, tenentId));
      return attachStatus(res.data);
    } catch (error) {
      console.log(error);
    }

  }
  static async submitDocuments() {

  }

}

const attachStatus = (data: any) => {
  let current = 0;


  data.forEach((l: any, i: number) => {
    // debugger;
    if (l.isCurrentStep) {
      current = i
    }
  });

  return data.map((l: any, i: number) => {
    // debugger
    if (i < current) {
      l.status = statusText.COMPLETED
    }

    if (i === current) {
      l.status = statusText.CURRENT
    }

    if (i > current) {
      l.status = statusText.UPCOMMING
    }
    return l;
  })
}