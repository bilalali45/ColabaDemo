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
    try{
    const response = await Axios.get(Http.createUrl(Http.baseUrl, url), {
        responseType: 'arraybuffer',
        headers: {
          Authorization: `Bearer ${authToken}`
        }
      });
       
      return response;
    } catch (error) {
      console.log('error',error) 
      return error
     }
   }

   static async acceptDocument(id: string, requestId: string, docId: string){
     try{
        let res = await Http.post(NeedListEndpoints.POST.documents.accept(), {
        id,
        requestId,
        docId
      });
      return res
    } catch (error) {
      console.log('error',error) 
      return error
     }
   }

   static async rejectDocument(loanApplicationId: number, id: string, requestId: string, docId: string){
     try{
    let res = await Http.post(NeedListEndpoints.POST.documents.reject(), {
        loanApplicationId,
        id,
        requestId,
        docId
      });
      return res
    } catch (error) {
      console.log('error',error) 
      return error
    }
}

  static async requestDocumentFiles(id: string, requestId: string, docId: string){
    try{
    const res = await Http.get<DocumentFileType[]>(
        NeedListEndpoints.GET.documents.files(id, requestId, docId)
      );
      return res; 
    } catch (error) {
      console.log('error',error) 
      return error
     }
  }

  static async getActivityLogs(id: string, docId: string, requestId: string){
    try{
    const res =  await Http.get<ActivityLogType[]>(NeedListEndpoints.GET.documents.activityLogs(id, docId, requestId));
    return res;
  } catch (error) {
    console.log('error',error) 
    return error
   }
}
   

}
