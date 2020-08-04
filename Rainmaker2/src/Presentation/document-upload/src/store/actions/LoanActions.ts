import { Http } from "rainsoft-js";
import { Auth } from "../../services/auth/Auth";
import { Endpoints } from "../endpoints/Endpoints";
import { ContactUs } from "../../entities/Models/ContactU";
import { AxiosResponse } from "axios";
import { LoanApplication } from "../../entities/Models/LoanApplication";
import { isFunction } from "util";
import { url } from "inspector";
import { LoanProgress } from "../../entities/Models/LoanProgress";

const http = new Http();

export const statusText = {
  COMPLETED: "COMPLETED",
  CURRENT: "CURRENT STEP",
  UPCOMMING: "UPCOMING",
};
export class LaonActions {
  static async getLoanOfficer(loanApplicationId: string) {
    try {
      let res: AxiosResponse<ContactUs> = await http.get<ContactUs>(
        Endpoints.loan.GET.officer(loanApplicationId)
      );
      return new ContactUs().fromJson(res.data);
    } catch (error) {
      if (error?.response?.data?.errors?.loanApplicationId?.length) {
        Auth.removeAuth();
        //alert("The Loan Application ID provided does not exist");
      }
    }
  }

  static async getLoanApplication(loanApplicationId: string) {
    try {
      let res: AxiosResponse<LoanApplication> = await http.get<LoanApplication>(
        Endpoints.loan.GET.info(loanApplicationId)
      );
      return new LoanApplication().fromJson(res.data);
    } catch (error) {
      console.log(error);
    }
  }

  static async getLOPhoto(lOPhotoId: string = "") {
    try {
      let res: any = await http.get(Endpoints.loan.GET.getLOPhoto(lOPhotoId));
      return res.data;
    } catch (error) {
      console.log("error.response", error);
    }
  }

  static async getFooter() {
    try {
      let res: any = await http.get(Endpoints.loan.GET.getFooter());
      return res.data;
    } catch (error) {
      console.log("error.response", error);
    }
  }

  static async getLoanProgressStatus(loanApplicationId: string) {
    try {
      let res: AxiosResponse<LoanProgress[]> = await http.get<LoanProgress[]>(
        Endpoints.loan.GET.loanProgressStatus(loanApplicationId)
      );
      return attachStatus(res.data);
    } catch (error) {
      console.log(error);
    }
  }
}

const attachStatus = (data: any) => {
  let current = 0;

  data.forEach((l: any, i: number) => {
    if (l.isCurrentStep) {
      current = i;
    }
  });

  return data.map((l: any, i: number) => {
    if (i < current) {
      l.status = statusText.COMPLETED;
    }

    if (i === current) {
      l.status = statusText.CURRENT;
    }

    if (i > current) {
      l.status = statusText.UPCOMMING;
    }
    return l;
  });
};
