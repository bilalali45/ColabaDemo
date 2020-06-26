import axios, { AxiosResponse } from "axios";

import { Http } from "../../services/http/Http";
import { Auth } from "../../services/auth/Auth";
import { Endpoints } from "../endpoints/Endpoints";
import { DocumentRequest } from "../../entities/Models/DocumentRequest";
import { UploadedDocuments } from "../../entities/Models/UploadedDocuments";

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
      return res.data.map((r) => r);
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
      console.log("getSubmittedDocuments", res);
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

  static async submitDocuments() {}
}
