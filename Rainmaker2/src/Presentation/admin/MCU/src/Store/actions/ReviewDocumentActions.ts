import { Http } from "rainsoft-js";
import Axios, { AxiosResponse } from "axios";
import { Endpoints } from "../endpoints/Endpoints";
import { NeedList } from "../../Entities/Models/NeedList";
import { NeedListEndpoints } from "../endpoints/NeedListEndpoints";
import { LocalDB } from "../../Utils/LocalDB";
import { DocumentFileType } from "../../Entities/Types/Types";


const http = new Http();
const authToken = LocalDB.getAuthToken();

export class ReviewDocumentActions {
   static async getDocumentForView(id: string, requestId: string, docId: string,fileId: string){
    const url = NeedListEndpoints.GET.documents.view(id,requestId,docId,fileId);
    const response = await Axios.get(http.createUrl(http.baseUrl, url), {
        responseType: 'arraybuffer',
        headers: {
          Authorization: `Bearer ${authToken}`
        }
      });
       
      return response;
   }

   static async acceptDocument(id: string, requestId: string, docId: string){
        await http.post(NeedListEndpoints.POST.documents.accept(), {
        id,
        requestId,
        docId
      });
       
   }

   static async rejectDocument(loanApplicationId: number, id: string, requestId: string, docId: string){
    await http.post(NeedListEndpoints.POST.documents.reject(), {
        loanApplicationId,
        id,
        requestId,
        docId
  });
}

  static async requestDocumentFiles(id: string, requestId: string, docId: string){
    const { data } = await http.get<DocumentFileType[]>(
        NeedListEndpoints.GET.documents.files(id, requestId, docId)
      );
      return data; 
  }
   

}