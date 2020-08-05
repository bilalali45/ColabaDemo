import { Http } from "rainsoft-js";
import { AxiosResponse } from "axios";
import { LoanApplication } from "../../Entities/Models/LoanApplication";
import { Endpoints } from "../endpoints/Endpoints";
import { NeedList } from "../../Entities/Models/NeedList";

const http = new Http();

export class NeedListActions {
  static async getLoanApplicationDetail(loanApplicationId: string) {
    try {
      let result: AxiosResponse<LoanApplication> = await http.get<
        LoanApplication
      >(Endpoints.NeedListManager.GET.loan.info(loanApplicationId));

      return new LoanApplication().fromJson(result.data);
    } catch (error) {
      console.log(error);
    }
  }

  static async getNeedList(loanApplicationId: string, status: boolean) {
    let url = Endpoints.NeedListManager.GET.documents.submitted(
      loanApplicationId,
      status
    );
    try {
      let res: AxiosResponse<NeedList> = await http.get<NeedList>(url);
      return res.data;
    } catch (error) {
      console.log(error);
    }
  }

  static async deleteNeedListDocument(
    id: string,
    requestId: string,
    docId: string
  ) {
    let url = Endpoints.NeedListManager.PUT.documents.deleteDoc();
    try {
      let res = await http.put(url, {
        id,
        requestId,
        docId,
      });
      return res.status;
    } catch (error) {
      console.log(error);
    }
  }
}
