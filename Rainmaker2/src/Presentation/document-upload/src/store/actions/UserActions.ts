import { Auth } from "../../services/auth/Auth";
import { Endpoints } from "../endpoints/Endpoints";
import jwt_decode from "jwt-decode";
import { Http } from "../../services/http/Http";
import Cookies from "universal-cookie";
import axios from "axios";
const cookies = new Cookies();
const http = new Http();
export class UserActions {
  static async authenticate() {
    const credentials = {
      userName: "pkdunnjr@dunnheat.com",
      password: "test123",
      employee: false,
    };

    let res: any = await http.post(
      Endpoints.user.POST.authorize(),
      credentials
    );
    if (!res.data.data) {
      return null;
    }
    let { token, refreshToken } = res.data.data;
    if (token && refreshToken) {
      Auth.storeTokenPayload(UserActions.decodeJwt(token));
      return { token, refreshToken };
    }
  }

  static async refreshToken() {
    try {
      if (!Auth.checkAuth()) {
        return;
      }
      let res: any = await http.post(Endpoints.user.POST.refreshToken(), {
        token: Auth.getAuth(),
        refreshToken: Auth.getRefreshToken(),
      });

      if (res?.data?.data?.token && res?.data?.data?.refreshToken) {
        Auth.saveAuth(res.data.data.token);
        Auth.saveRefreshToken(res.data.data.refreshToken);
        let payload = UserActions.decodeJwt(res.data.data.token);
        Auth.storeTokenPayload(payload);
        UserActions.addExpiryListener(payload);
        return true;
      }
      Auth.removeAuth();
      return false;
    } catch (error) {
      setTimeout(() => {
        UserActions.refreshToken();
      }, 10 * 1000);
      return false;
    }
  }

  static async refreshParentApp() {
    try {
      console.log("In refreshParentApp");
      axios.get(window.envConfig.APP_BASE_URL + "Account/KeepAlive");
      return true;
    } catch (error) {
      console.log("In refreshParentApp Error");
      return false;
    }
  }

  static async authorize() {
    let isAuth = Auth.checkAuth();
    if (isAuth === "token expired") {
      console.log("Refresh token called from authorize");
      let res: any = await UserActions.refreshToken();
      if (res) {
        return true;
      } else {
        return false;
      }
    }

    if (!isAuth) {
      if (process.env.NODE_ENV === "development") {
        let tokens: any = await UserActions.authenticate();
        if (tokens.token) {
          Auth.saveAuth(tokens.token);
          Auth.saveRefreshToken(tokens.refreshToken);
          return true;
        } else {
          return false;
        }
      }

      let Rainmaker2Token = cookies.get("Rainmaker2Token");
      let Rainmaker2RefreshToken = cookies.get("Rainmaker2RefreshToken");
      console.log(
        "Cache token values in authorize Rainmaker2Token",
        Rainmaker2Token,
        "Rainmaker2RefreshToken",
        Rainmaker2RefreshToken
      );
      if (Rainmaker2Token && Rainmaker2RefreshToken) {
        console.log("Cache token values exist");
        Auth.saveAuth(Rainmaker2Token);
        Auth.saveRefreshToken(Rainmaker2RefreshToken);
        let isAuth = Auth.checkAuth();
        if (isAuth) {
          console.log("Cache token is valid");
          Auth.storeTokenPayload(UserActions.decodeJwt(Rainmaker2Token));
        } else {
          console.log("Cache token is not valid");
          console.log(
            "Refresh token called from authorize in case of MVC expire token"
          );
          UserActions.refreshToken();
        }
        return true;
      } else {
        console.log("Cache token not found");
        return false;
      }
    } else {
      return true;
    }
  }

  static addExpiryListener(payload) {
    console.log("in listener added");
    let expiry = payload.exp;
    let currentTime = Date.now();
    let expiryTime = expiry * 1000;
    let time = expiryTime - currentTime;
    if (time < 1) {
      console.log("Refresh token called from addExpiryListener in case of < 1");
      UserActions.refreshToken();
      return;
    }
    // let t = (time * 1000) * 60;

    console.log("toke will expire after", time, "mil sec");
    setTimeout(async () => {
      console.log(
        "Refresh token called from addExpiryListener in case of time out meet"
      );
      await UserActions.refreshToken();
    }, time - 2000);
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
    let token = Auth.getAuth() || "";
    if (token) {
      let decoded = jwt_decode(token);
      return decoded;
    }
  }

  static getUserName() {
    let info: any = UserActions.getUserInfo();
    return ` ${info?.FirstName} ${info?.LastName} `;
  }

  static async logout() {
    Auth.removeAuth();
  }
}
