import { Http } from "../../services/http/Http";
import { Auth } from "../../services/auth/Auth";
import { Endpoints } from "../endpoints/Endpoints";
import { ContactUs } from "../../entities/Models/ContactU";
import { AxiosResponse } from "axios";
import { LoanApplication } from "../../entities/Models/LoanApplication";

const http = new Http();

export class LaonActions {

  static async getLoanOfficer(loanApplicationId: string, businessUnitId: string) {
    try {
      let res: AxiosResponse<ContactUs> = await http.get<ContactUs>(Endpoints.loan.GET.officer(loanApplicationId, businessUnitId));
      
      return new ContactUs().fromJson(res.data);
    } catch (error) {

    }
  }

  static async getLoanApplication(loanApplicationId: string) {
    try {
      let res: AxiosResponse<LoanApplication> = await http.get<LoanApplication>(Endpoints.loan.GET.info(loanApplicationId));
      return new LoanApplication().fromJson(res.data);
    } catch (error) {
      console.log(error);
    }
  }

  static async getLOPhoto(lOPhotoId: string = '', businessUnitId: string = '') {
    if(!lOPhotoId) {
      return 'Photo Id & business Unit Id are requireq.';
    }
    try {
      let res = await http.get(Endpoints.loan.GET.getLOPhoto(lOPhotoId, businessUnitId));
      console.log(res);
      return res;
    } catch (error) {
      console.log(error);
    }
  }
}