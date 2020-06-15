import { Http } from "../../services/http/Http";
import { Auth } from "../../services/auth/Auth";
import { Endpoints } from "../endpoints/Endpoints";
import { ContactUs } from "../../entities/Models/ContactU";
import { AxiosResponse } from "axios";
import { LoanApplication } from "../../entities/Models/LoanApplication";
import { isFunction } from "util";
import { url } from "inspector";

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
    try {
      let res: any = await http.get(Endpoints.loan.GET.getLOPhoto(lOPhotoId, businessUnitId));
      return res.data;
    } catch (error) {
      console.log('error.response', error);
    }

  }


}

const b64toBlob = (b64Data: any, contentType: any = '', sliceSize: any = 512) => {
  const byteCharacters = atob(b64Data);
  const byteArrays = [];

  for (let offset = 0; offset < byteCharacters.length; offset += sliceSize) {
    const slice = byteCharacters.slice(offset, offset + sliceSize);

    const byteNumbers = new Array(slice.length);
    for (let i = 0; i < slice.length; i++) {
      byteNumbers[i] = slice.charCodeAt(i);
    }

    const byteArray = new Uint8Array(byteNumbers);
    byteArrays.push(byteArray);
  }

  const blob = new Blob(byteArrays, { type: contentType });
  return blob;
}