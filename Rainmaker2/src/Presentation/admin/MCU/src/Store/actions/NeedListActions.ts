import { Http } from 'rainsoft-js';
import { AxiosResponse } from 'axios';
import { LoanApplication } from '../../Entities/Models/LoanApplication';
import { Endpoints } from '../endpoints/Endpoints';
import { NeedList } from '../../Entities/Models/NeedList';

export class NeedListActions {
  static async getLoanApplicationDetail(loanApplicationId: string) {
    try {
      let result: AxiosResponse<LoanApplication> = await Http.get<
        LoanApplication
      >(Endpoints.NeedListManager.GET.loan.info(loanApplicationId));

      let mileStoneresult: AxiosResponse<string> = await Http.get<
        string
      >(Endpoints.NeedListManager.GET.loan.milestoneInfo(loanApplicationId));

      result.data.status = mileStoneresult.data.toString()
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
      let res: AxiosResponse<NeedList[]> = await Http.get<NeedList[]>(url);

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
      let res = await Http.put(url, {
        id,
        requestId,
        docId
      });
      return res.status;
    } catch (error) {
      console.log(error);
    }
  }

  static async checkIsByteProAuto() {
    let url = Endpoints.NeedListManager.GET.documents.checkIsByteProAuto();
    try {
      let res: any = await Http.get(url);
      return res.data;
    } catch (error) {
      console.log(error);
    }
  }

  static async fileSyncToLos(
    LoanApplicationId: number,
    DocumentLoanApplicationId: string,
    RequestId: string,
    DocumentId: string,
    FileId: string
  ) {
    let url = Endpoints.NeedListManager.POST.documents.fileSyncToLos();
    try {
      let res: any = await Http.post(url, {
        LoanApplicationId,
        DocumentLoanApplicationId,
        RequestId,
        DocumentId,
        FileId
      });
      return res.status;
    } catch (error) {
      console.log(error);
    }
  }
}
