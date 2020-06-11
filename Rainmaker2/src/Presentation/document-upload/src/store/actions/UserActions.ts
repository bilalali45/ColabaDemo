import { Http } from "../../services/http/Http";
import { Auth } from "../../services/auth/Auth";
import { Endpoints } from "../endpoints/Endpoints";

const http = new Http();

export class UserActions {
  static async authenticate() {
    try {
      let res: any = await http.get(Endpoints.user.GET.authorize());
      Auth.saveAuth(res.data.access_token);
    } catch (error) {
      console.log(error);
    }
  }

  static async getUserInfo() {

  }

  
  static async logout() {
    Auth.removeAuth();
  }
}