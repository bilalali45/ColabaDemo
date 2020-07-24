import jwt_decode from "jwt-decode";
import Cookies from "universal-cookie";
import { ApplicationEnv } from "../../utils/helpers/AppEnv";
import { debug } from "console";
const cookies = new Cookies();

export class Auth {
  public static saveAuth(token: string) {
    localStorage.setItem("auth", this.encodeString(token));
  }

  public static saveRefreshToken(refToken: string) {
    localStorage.setItem("refreshToken", this.encodeString(refToken));
  }

  public static getRefreshToken() {
    return this.decodeString(localStorage.getItem("refreshToken"));
  }

  public static removeRefreshToken() {
    localStorage.removeItem("refreshToken");
  }

  public static getAuth() {
    return this.decodeString(localStorage.getItem("auth"));
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
    localStorage.setItem("payload", this.encodeString(JSON.stringify(payload)));
  }

  static getUserPayload() {
    let payload = this.decodeString(localStorage.getItem("payload"));
    if (payload) {
      return JSON.parse(payload);
    }
  }

  static removeAuthToken() {
    localStorage.removeItem("auth");
  }

  public static removeAuth() {
    let items = [
      "auth",
      "loanApplicationId",
      "tenantId",
      "businessUnitId",
      "payload",
      "refreshToken",
    ];
    for (const item of items) {
      localStorage.removeItem(item);
    }
  }

  public static getLoanAppliationId() {
    return this.decodeString(localStorage.getItem("loanApplicationId")) || "";
  }

  public static getTenantId() {
    return this.decodeString(localStorage.getItem("tenantId")) || "";
  }

  public static getBusinessUnitId() {
    return this.decodeString(localStorage.getItem("businessUnitId")) || "";
  }

  public static setLoanAppliationId(loanApplicationId: string) {
    localStorage.setItem(
      "loanApplicationId",
      this.encodeString(loanApplicationId)
    );
  }

  public static setTenantId(tenantId: string) {
    localStorage.setItem("tenantId", this.encodeString(tenantId));
  }

  public static setBusinessUnitId(businessUnitId: string) {
    localStorage.setItem("businessUnitId", this.encodeString(businessUnitId));
  }

  public static storeItem(name: string, data: string) {
    localStorage.setItem(name, this.encodeString(data));
  }

  public static removeItem(name: string) {
    localStorage.removeItem(name);
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
      Auth.removeAuth();
      return null;
    }
  }
}
