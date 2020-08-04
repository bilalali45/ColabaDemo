import jwt_decode from "jwt-decode";
import Cookies from "universal-cookie";
import { ApplicationEnv } from "../../utils/helpers/AppEnv";
import { debug } from "console";
const cookies = new Cookies();

export class Auth {
  public static saveAuth(token: string) {
    window.sessionStorage.setItem("auth", this.encodeString(token));
  }

  public static saveRefreshToken(refToken: string) {
    window.sessionStorage.setItem("refreshToken", this.encodeString(refToken));
  }

  public static getRefreshToken() {
    return this.decodeString(window.sessionStorage.getItem("refreshToken"));
  }

  public static removeRefreshToken() {
    window.sessionStorage.removeItem("refreshToken");
  }

  public static getAuth() {
    return this.decodeString(window.sessionStorage.getItem("auth"));
  }

  public static getLoginUserName() {
    return localStorage.getItem("devusername");
  }

  public static getLoginPassword() {
    return localStorage.getItem("devuserpassword");
  }

  public static checkAuth(): boolean | string {
    let rainmaker2Token = cookies.get("Rainmaker2Token");
    let auth = this.getAuth();
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

  static storeTokenPayload(payload) {
    if (!payload) return;
    window.sessionStorage.setItem(
      "payload",
      this.encodeString(JSON.stringify(payload))
    );
  }

  static getUserPayload() {
    let payload = this.decodeString(window.sessionStorage.getItem("payload"));
    if (payload) {
      return JSON.parse(payload);
    }
  }

  static removeAuthToken() {
    window.sessionStorage.removeItem("auth");
  }

  public static removeAuth() {
    let items = [
      "auth",
      "loanApplicationId",
      "payload",
      "refreshToken",
      "baseParam",
    ];
    for (const item of items) {
      window.sessionStorage.removeItem(item);
    }
  }

  public static getLoanAppliationId() {
    return (
      this.decodeString(window.sessionStorage.getItem("loanApplicationId")) ||
      ""
    );
  }

  public static setLoanAppliationId(loanApplicationId: string) {
    window.sessionStorage.setItem(
      "loanApplicationId",
      this.encodeString(loanApplicationId)
    );
  }

  public static storeItem(name: string, data: string) {
    window.sessionStorage.setItem(name, this.encodeString(data));
  }

  public static removeItem(name: string) {
    window.sessionStorage.removeItem(name);
  }

  public static encodeString(value: string) {
    // Encode the String
    //const currentDate = Date.toString();
    const string = value + "|" + ApplicationEnv.Encode_Key;
    return btoa(string);
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
}
