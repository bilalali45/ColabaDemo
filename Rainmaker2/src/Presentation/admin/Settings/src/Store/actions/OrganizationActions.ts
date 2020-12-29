import { Http } from "rainsoft-js";
import Organization from "../../Entities/Models/Organization";
import { Endpoints } from "../endpoints/Endpoints";

export class OrganizationsActions {

    static async fetchOrganizationSettings() {
        let url = Endpoints.OrganizationManager.GET.getOrganizationSetting();
        try {
           let res: any = await Http.get(url);
            let mappedData = res.data.map((org:any) => {
                return new Organization(org.id, org.name, org.byteOrganizationCode,org.photo);
            });
              return mappedData;

        } catch (error) {
            console.log('error',error)
        }   
   
    }

    static async updateOrganizationSettings(Organization: Organization[]){
        
       let url = Endpoints.OrganizationManager.POST.updateOrganizationSettings();
       try {
           let res = await Http.post(url, Organization);
           return res;
       } catch (error) {
        console.log('error',error)
       }
    }
   
}