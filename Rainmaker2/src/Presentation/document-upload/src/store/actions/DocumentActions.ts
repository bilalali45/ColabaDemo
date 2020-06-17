import { Http } from "../../services/http/Http";
import { Auth } from "../../services/auth/Auth";
import { Endpoints } from "../endpoints/Endpoints";

const http = new Http();

export class DocumentActions {

  static async getPendingDocuments(loanApplicationId: string, tenentId: string) {
    try {
      let res: any = await http.get(Endpoints.documents.GET.pendingDocuments(loanApplicationId, tenentId));
      console.log(res);
      return res.data;
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
      l.status = 'Completed';
    }

    if (i === current) {
      l.status = 'In progress'
    }

    if (i > current) {
      l.status = 'To be done'
    }
    return l;
  })
}