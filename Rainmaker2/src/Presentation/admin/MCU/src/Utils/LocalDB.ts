import jwt_decode from "jwt-decode";
import Cookies from "universal-cookie";
import { ApplicationEnv } from "./helpers/AppEnv";
const cookies = new Cookies();

export class LocalDB {
  static getCredentials() {
    const credentials = {
      userName: LocalDB.getLoginDevUserName(),
      password: LocalDB.getLoginDevPassword(),
      employee: true,
    };

    return credentials;
  }

  //#region Local DB get methods
  static getAuthToken() {
    return this.decodeString(localStorage.getItem("token"));
  }

  static getRefreshToken() {
    return this.decodeString(localStorage.getItem("refreshToken"));
  }

  static getLoginDevUserName() {
    return localStorage.getItem("devusername");
  }

  static getLoginDevPassword() {
    return localStorage.getItem("devuserpassword");
  }

  static getUserPayload() {
    let payload = this.decodeString(localStorage.getItem("payload"));
    if (payload) {
      return JSON.parse(payload);
    }
  }

  static getLoanAppliationId() {
    return (
      this.decodeString(window.sessionStorage.getItem("loanApplicationId")) ||
      ""
    );
  }

  static setLoanAppliationId(loanApplicationId: string) {
    window.sessionStorage.setItem(
      "loanApplicationId",
      this.encodeString(loanApplicationId)
    );
  }

  //#endregion

  //#region Local DB Post Methods
  static storeTokenPayload(payload: any) {
    if (!payload) return;
    localStorage.setItem("payload", this.encodeString(JSON.stringify(payload)));
  }

  static storeAuthTokens(token: string, refreshToken: string) {
    localStorage.setItem("token", this.encodeString(token));
    localStorage.setItem("refreshToken", this.encodeString(refreshToken));
  }

  public static checkAuth(): boolean | string {
    let rainmaker2Token = cookies.get("Rainmaker2Token");
    let auth = this.getAuthToken();
    if (!auth) {
      return false;
    }
    if (rainmaker2Token) {
      const decodeCacheToken: any = jwt_decode(rainmaker2Token);
      const decodeAuth: any = jwt_decode(auth);
      if (decodeAuth?.UserName != decodeCacheToken?.UserName) {
        return false;
      }
    }

    let payload = this.getUserPayload();
    if (payload) {
      let expiry = new Date(payload.exp * 1000);
      let currentDate = new Date(Date.now());
      if (currentDate < expiry) {
        return true;
      } else {
        return "token expired";
        // return false;
        // Auth.removeAuth();
      }
    }
    return true;
  }

  public static storeItem(name: string, data: string) {
    localStorage.setItem(name, this.encodeString(data));
  }
  //#endregion

  //#region Remove Auth
  static removeAuth() {
    let items = ["token", "payload", "refreshToken"];
    for (const item of items) {
      localStorage.removeItem(item);
    }
  }
  //#endregion

  //#region Encode Decode
  public static encodeString(value: string) {
    // Encode the String
    //const currentDate = Date.toString();
    const string = value + "|" + ApplicationEnv.Encode_Key;
    return btoa(unescape(encodeURIComponent(string)));
  }

  public static decodeString(value?: string | null) {
    // Decode the String
    if (!value) {
      return "";
    }
    try {
      const decodedString = atob(value);
      return decodedString.split("|")[0];
    } catch {
      return null;
    }
  }
  //#endregion
}
