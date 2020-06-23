
import { Auth } from "../../services/auth/Auth";
import { Endpoints } from "../endpoints/Endpoints";
import jwt_decode from "jwt-decode";
import { Http } from "../../services/http/Http";

const http = new Http();

export class UserActions {
  static async authenticate() {

    const credentials = {
      "userName": "pkdunnjr@dunnheat.com",
      "password": "test123",
      "employee": false
    }

    let res: any = await http.post(Endpoints.user.POST.authorize(), credentials);
    if (!res.data.data) {
      return null
    }
    let { token, refreshToken } = res.data.data;
    if (token && refreshToken) {
      Auth.storeTokenPayload(UserActions.decodeJwt(token));
      return { token, refreshToken };
    }
  }

  static async refreshToken() {
    let res: any = await http.post(Endpoints.user.POST.refreshToken(), {
      token: Auth.getAuth(),
      refreshToken: Auth.getRefreshToken()
    });
    if (res.data.token) {
      Auth.saveAuth(res.data.token);
      Auth.saveRefreshToken(res.data.refreshToken);
      Auth.storeTokenPayload(UserActions.decodeJwt(res.data.token));
      console.log(res);
    }
  }

  static decodeJwt(token) {
    try {
      if (token) {
        let decoded = jwt_decode(token);
        return decoded;
      }

    } catch (error) {
      console.log(error);
    }
  }

  static getUserInfo() {
    let token = Auth.getAuth() || '';
    if (token) {
      let decoded = jwt_decode(token);
      return decoded;
    }
  }

  static async logout() {
    Auth.removeAuth();
  }
}