import { Http } from "../../services/http/Http";
import { Auth } from "../../services/auth/Auth";
import { Endpoints } from "../endpoints/Endpoints";

const http = new Http();

export class UserActions {
  static async authenticate() {

    const credentials = {
      userName: 'danish',
      password: 'Rainsoft',
      employee: true
    }

    let res: any = await http.post(Endpoints.user.POST.authorize(), credentials);

    if (res.data.data.token) {
      Auth.saveAuth(res.data.data.token);
    }
  }

  static async getUserInfo() {

  }


  static async logout() {
    Auth.removeAuth();
  }
}