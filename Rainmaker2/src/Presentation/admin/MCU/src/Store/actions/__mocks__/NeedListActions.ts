import {LoanApplication} from '../../../Entities/Models/LoanApplication';

export class NeedListActions {
  static async getLoanApplicationDetail(loanApplicationId: string) {
    try {
      return new LoanApplication();
    } catch (error) {
      console.log(error);
    }
  }

  static async getNeedList(loanApplicationId: string, status: boolean) {
    try {
      return [];
    } catch (error) {
      console.log(error);
    }
  }

  static async deleteNeedListDocument(
    id: string,
    requestId: string,
    docId: string
  ) {
    try {
      return '200';
    } catch (error) {
      console.log(error);
    }
  }

  static async checkIsByteProAuto() {
    try {
      return new Object();
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
    try {
      return '200';
    } catch (error) {
      console.log(error);
    }
  }
}
