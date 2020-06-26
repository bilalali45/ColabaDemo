
import { Auth } from "../../services/auth/Auth";
import { Endpoints } from "../endpoints/Endpoints";
import jwt_decode from "jwt-decode";
import { Http } from "../../services/http/Http";
import Cookies from "universal-cookie";
const cookies = new Cookies();
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
    console.log('in refresh token called');
    let res: any = await http.post(Endpoints.user.POST.refreshToken(), {
      token: Auth.getAuth(),
      refreshToken: Auth.getRefreshToken()
    });
    console.log(res);

    if (res?.data?.data?.token && res?.data?.data?.refreshToken) {
      console.log(localStorage);
      Auth.saveAuth(res.data.data.token);
      Auth.saveRefreshToken(res.data.data.refreshToken);
      let payload = UserActions.decodeJwt(res.data.data.token);
      Auth.storeTokenPayload(payload);
      UserActions.addExpiryListener(payload);

      return true;
    }
    return false;
  }

  static async authorize() {
    let isAuth = Auth.checkAuth();
    if (isAuth === 'token expired') {
      let res: any = await UserActions.refreshToken();
      if (res) {
        return true;
      } else {
        return false;
      }
    }

    if (!isAuth) {

      if (process.env.NODE_ENV === 'development') {
        let tokens: any = await UserActions.authenticate();
        if (tokens.token) {
          Auth.saveAuth(tokens.token);
          Auth.saveRefreshToken(tokens.refreshToken);
          return true;
        } else {
          return false;
        }
      }

      let Rainmaker2Token = cookies.get('Rainmaker2Token');
      let Rainmaker2RefreshToken = cookies.get('Rainmaker2RefreshToken');

      if (Rainmaker2Token && Rainmaker2RefreshToken) {
        Auth.saveAuth(Rainmaker2Token);
        Auth.saveRefreshToken(Rainmaker2RefreshToken);
        Auth.storeTokenPayload(UserActions.decodeJwt(Rainmaker2Token));
        return true;
      } else {
        return false;
      }

    } else {
      return true;
    }
  }

  static addExpiryListener(payload) {
    console.log('in listener added');
    let expiry = payload.exp;
    let currentTime = Date.now();
    let expiryTime = expiry * 1000;
    // debugger
    let time = expiryTime - currentTime;
    console.log(time, time < 1);
    if (time < 1) {
      console.log('in here if < 1', time);
      UserActions.refreshToken();
      return;
    }
    // let t = (time * 1000) * 60;

    console.log('time', time);
    setTimeout(async () => {
      console.log('in set time out', time);
      await UserActions.refreshToken();
    }, time - 120000);
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