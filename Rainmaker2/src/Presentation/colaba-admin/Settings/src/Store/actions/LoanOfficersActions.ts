import { Http } from "rainsoft-js";
import LoanOfficer from "../../Entities/Models/LoanOfficer";
import { Endpoints } from "../endpoints/Endpoints";
import { LoanOfficerActions } from "../reducers/LoanOfficerReducer";

export class LoanOfficersActions {

    static async fetchLoanOfficersSettings() {
        let url = Endpoints.LoanOfficersManager.GET.getLoanOfficerSetting();
        try {
           let res: any = await Http.get(url);
            let mappedData = res.data.map((lo:any) => {
                return new LoanOfficer(lo.userId, lo.userName, lo.byteUserName, lo.fullName,lo.photo);
            });
              return mappedData;

        } catch (error) {
            console.log('error',error)
        }   
   
    }

    static async updateLoanOfficersSettings(loanOfficers: LoanOfficer[]){
        
       let url = Endpoints.LoanOfficersManager.POST.updateSettings();
       try {
           let res = await Http.post(url, loanOfficers);
           return res;
       } catch (error) {
        console.log('error',error)
       }
    }
   
}