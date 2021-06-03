import { Http } from "rainsoft-js";
import axios, { AxiosRequestConfig, AxiosResponse } from "axios";
import { OutgoingHttpHeaders } from "http";
import { Auth } from "../../services/auth/Auth";
import { Endpoints } from "../endpoints/Endpoints";
import { ContactUs } from "../../entities/Models/ContactU";
import { LoanApplication } from "../../entities/Models/LoanApplication";
import { LoanProgress } from "../../entities/Models/LoanProgress";
import Cookies from "universal-cookie";
const cookies = new Cookies();

export const statusText = {
  COMPLETED: "COMPLETED",
  CURRENT: "CURRENT STEP",
  UPCOMMING: "UPCOMING",
};
export class LaonActions {

  static async getLoanOfficer(loanApplicationId: string) {
    try {
      let res: AxiosResponse<ContactUs> = await Http.get<ContactUs>(
        Endpoints.loan.GET.officer(loanApplicationId)
      );
      return new ContactUs().fromJson(res.data);
    } catch (error) {
      if (error?.response?.data?.errors?.loanApplicationId?.length) {
        window.open("/404", "_self");
        //alert("The Loan Application ID provided does not exist");
      }
    }
  }

  static async getLoanApplication(loanApplicationId: string) {
   let baseUrl: any = window.envConfig.API_BASE_URL;
   
    try {
      // let newUrl = `${baseUrl}${Endpoints.loan.GET.info(loanApplicationId)}`
      // const bearer = `Bearer ${cookies.get("TokenPayload")}`;
      // const options: any = {
      //   method: "get",
      //   url: newUrl,
      //   data: "",
      //   headers: { "Authorization": bearer, "colabaweburl": "https://apply.lendova.com:5003"}
      // };
     let res: AxiosResponse<LoanApplication> = await Http.get<LoanApplication>(Endpoints.loan.GET.info(loanApplicationId));
    //  let res: AxiosResponse<LoanApplication> =  await axios(options)  
      console.log('Axios Direct Network Response getLoanApplication Success')
      return new LoanApplication().fromJson(res.data);
    } catch (error) {
      console.log("Axios Direct Network Response Error", error);
    }
  }

  static async getLOPhoto(lOPhotoId: string = "", loanApplicationId: string) {
    try {
      let res: any = await Http.get(
        Endpoints.loan.GET.getLOPhoto(lOPhotoId, loanApplicationId)
      );
      return res.data;
    } catch (error) {
      console.log("error.response", error);
    }
  }

  static async getFooter(loanApplicationId: string) {
    try {
      let res: any = await Http.get(
        Endpoints.loan.GET.getFooter(loanApplicationId)
      );
      return res.data;
    } catch (error) {
      console.log("error.response", error);
    }
  }

  static async getCompanyLogoSrc(loanApplicationId: string) {
    try {
      let res: any = await Http.get(
        Endpoints.loan.GET.getCompanyLogoSrc(loanApplicationId)
      );
      return res.data;
    } catch (error) {
      console.log("error.response", error);
    }
  }

  static async getCompanyFavIconSrc(loanApplicationId: string) {
    try {
      let res: any = await Http.get(
        Endpoints.loan.GET.getCompanyFavIconSrc(loanApplicationId)
      );
      return res.data;
    } catch (error) {
      console.log("error.response", error);
    }
  }

  static async getLoanProgressStatus(loanApplicationId: string) {
    try {
      let res: AxiosResponse<LoanProgress[]> = await Http.get<LoanProgress[]>(
        Endpoints.loan.GET.loanProgressStatus(loanApplicationId)
      );
      res.data = res.data.map((d, idx)=> {
        d.order = idx + 1;
        return d;
      });

      return attachStatus(res.data);
    } catch (error) {
      console.log(error);
    }
  }

  public static decodeString(value?: string | null) {
    // Decode the String
    if (!value) {
      return '';
    }
    try {
      const decodedString = atob(value);
      return decodedString.split('|')[0];
    } catch {
      return null;
    }
  }
}

const attachStatus = (data: any) => {
  let current = 0;

  data.forEach((l: any, i: number) => {
    if (l.isCurrent) {
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
