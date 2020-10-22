import { Http } from "rainsoft-js";
import Axios, { AxiosResponse } from "axios";
import { Endpoints } from "../endpoints/Endpoints";
import { NeedList } from "../../Entities/Models/NeedList";
import { NeedListEndpoints } from "../endpoints/NeedListEndpoints";
import { LocalDB } from "../../Utils/LocalDB";
import { ActivityLogType, DocumentFileType } from "../../Entities/Types/Types";

export class ReviewDocumentActions {
   static async getDocumentForView(id: string, requestId: string, docId: string,fileId: string){
    const url = NeedListEndpoints.GET.documents.view(id,requestId,docId,fileId);
    const authToken = LocalDB.getAuthToken();
    const response = await Axios.get(Http.createUrl(Http.baseUrl, url), {
        responseType: 'arraybuffer',
        headers: {
          Authorization: `Bearer ${authToken}`
        }
      });
       
      return response;
   }

   static async acceptDocument(id: string, requestId: string, docId: string){
        await Http.post(NeedListEndpoints.POST.documents.accept(), {
        id,
        requestId,
        docId
      });
       
   }

   static async rejectDocument(loanApplicationId: number, id: string, requestId: string, docId: string){
    await Http.post(NeedListEndpoints.POST.documents.reject(), {
        loanApplicationId,
        id,
        requestId,
        docId
  });
}

  static async requestDocumentFiles(id: string, requestId: string, docId: string){
    const { data } = await Http.get<DocumentFileType[]>(
        NeedListEndpoints.GET.documents.files(id, requestId, docId)
      );
      return data; 
  }

  static async getActivityLogs(id: string, docId: string, requestId: string){
    const { data } =  await Http.get<ActivityLogType[]>(NeedListEndpoints.GET.documents.activityLogs(id, docId, requestId));
    return data;
}
   

}
