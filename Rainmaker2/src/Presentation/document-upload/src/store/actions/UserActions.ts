import { Http } from "../../services/http/Http";
import { Auth } from "../../services/auth/Auth";
import { Endpoints } from "../endpoints/Endpoints";
import jwt_decode from "jwt-decode";

const http = new Http();

export class UserActions {
  static async authenticate() {

    const credentials = {
      userName: 'danish',
      password: 'Rainsoft',
      employee: true
    }

    let res: any = await http.post(Endpoints.user.POST.authorize(), credentials);
    if (!res.data.data) {
      return ''
    }
    let token = res.data.data.token;
    if (token) {
      Auth.storeTokenPayload(UserActions.decodeJwt(token));
      return token;
    }
  }

  static decodeJwt(token) {
    if(token) {
      let decoded = jwt_decode(token);
      return decoded;
    }
  }

  static getUserInfo() {
    let token = Auth.getAuth() || '';
    if(token) {
      let decoded = jwt_decode(token);
      return decoded;
    }
  }

  static async logout() {
    Auth.removeAuth();
  }
}