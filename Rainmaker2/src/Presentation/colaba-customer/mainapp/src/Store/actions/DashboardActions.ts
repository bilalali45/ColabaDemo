import { Endpoints } from "../endpoints/Endpoints";
import { Http } from "rainsoft-js";
import { AxiosResponse } from 'axios';
import { APIResponse } from "../../Entities/Models/APIResponse";


export default class DashboardActions {
    static async fetchLoggedInUserCurrentLoanApplications() {
        let url = Endpoints.Dashboard.GET.fetchLoggedInUserLoanApplications();
        try {
             let response: any = await Http.get(url, {}, false);
             return response.data;
        } catch (error) {
            console.log(error);
            return undefined;
        }
    }

    static async setSettings(color:string, captchaCode: string) {
        let url = Endpoints.Dashboard.PUT.setTenantSettings();
        try {
          let res
            : AxiosResponse = await Http.put(
              url,
              {
                color
              }, {
              'RecaptchaCode': captchaCode
            },true
            );
          return new APIResponse(res.status, "");
        } catch (error) {
          console.log(error);
          return error;
        }
    
      }
};
