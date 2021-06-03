import { Http } from "rainsoft-js";
import { APIResponse } from "../../Entities/Models/APIResponse";
import { BorrowerBasicInfo } from "../../Entities/Models/BorrowerInfo";
import { GetReviewBorrowerInfoSectionPayload } from "../../Entities/Models/types";
import { Endpoints } from "../endpoints/Endpoint";

export default class BorrowerActions {

    static async getAllBorrower(loanApplicationId: number) {
        let url = Endpoints.Borrower.GET.getAllBorrower(loanApplicationId);
        try {
          let response = await Http.get(url);
          return new APIResponse(response.status,response.data);
        } catch (error) {
          console.log(error);
          return error;
        }
     }

     static async getBorrowerInfo(loanApplicationId: number, borrowerId: number) {
        let url = Endpoints.Borrower.GET.getBorrowerInfo(loanApplicationId, borrowerId);
        try {
          let response = await Http.get(url);
          return response.data;
        } catch (error) {
          console.log(error);
          return error;
        }
     }

     static async populatePrimaryBorrower() {
        let url = Endpoints.Borrower.GET.populatePrimaryBorrower()
        try {
          let response = await Http.get(url);
          return response.data;
        } catch (error) {
          console.log(error);
          return error;
        }
     }

     static async addOrUpdateBorrowerInfo(borrowerBasicInfo: BorrowerBasicInfo) {
        let url = Endpoints.Borrower.POST.addOrUpdateBorrowerInfo()
        try {
          let response = await Http.post(url, borrowerBasicInfo)
          return response.data;
        } catch (error) {
          console.log(error);
           return error;
        }
     }

     static async getReviewBorrowerInfoSection(loanApplicationId: Number) {
      let url = Endpoints.Borrower.GET.getReviewBorrowerInfoSection(loanApplicationId)
      try {
          let response: any = await Http.get(url, {}, false);
          const dataPayload :GetReviewBorrowerInfoSectionPayload = response.data;
          return dataPayload
      } catch (error) {
          console.log(error);
          return error;
      }
  }

  static async deleteSecondaryBorrower(loanApplicationId: number, borrowerId :number) {
      let url = Endpoints.Borrower.DELETE.deleteSecondaryBorrower();
      try {
          let response: any = await Http.delete(url,{"loanApplicationId":loanApplicationId,"borrowerId": borrowerId},{},false);
          const dataPayload :GetReviewBorrowerInfoSectionPayload = response.data;
          return dataPayload
      } catch (error) {
          console.log(error);
          return error;
      }
  }

  static async getAllConsentTypes(borrowerId :number) {
    let url = Endpoints.Borrower.GET.getAllConsentTypes(borrowerId);
    try {
        let response: any = await Http.get(url, {}, false);
        return new APIResponse(response.status,response.data);
    } catch (error) {
        console.log(error);
        return error;
    }
}

static async getBorrowerAcceptedConsents(loanApplicationId: number, borrowerId: number) {
  let url = Endpoints.Borrower.GET.getBorrowerAcceptedConsents(loanApplicationId, borrowerId);
  try {
      let response: any = await Http.get(url, {}, false);
      return new APIResponse(response.status,response.data);
  } catch (error) {
      console.log(error);
      return error;
  }
}

static async addOrUpdateBorrowerConsents(loanApplicationId: number, borrowerId: number, isAccepted: boolean, State: string, consentHash: string) {
  let url = Endpoints.Borrower.POST.addOrUpdateBorrowerConsent()
  try {
    let response = await Http.post(url, {
      LoanApplicationId: loanApplicationId,
      BorrowerId: borrowerId,
      IsAccepted: isAccepted,
      State: State,
      ConsentHash: consentHash
    });
    return new APIResponse(response.status,response.data);
  } catch (error) {
    console.log(error);
    return error;
  }
}
static async getBorrowersForFirstReview(loanApplicationId: number) {
  let url = Endpoints.Borrower.GET.getBorrowersForFirstReview(loanApplicationId);
        try {
          let response = await Http.get(url);
          return new APIResponse(response.status,response.data);
        } catch (error) {
          console.log(error);
          return error;
        }
}
static async getBorrowersForSecondReview(loanApplicationId: number) {
  let url = Endpoints.Borrower.GET.getBorrowersForSecondReview(loanApplicationId);
  try {
    let response = await Http.get(url);
    return new APIResponse(response.status,response.data);
  } catch (error) {
    console.log(error);
    return error;
  }
}
}
