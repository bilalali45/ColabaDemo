import jwt_decode from "jwt-decode";
import Cookies from "universal-cookie";
const cookies = new Cookies();

export class LocalDB {
  static getCredentials() {
    const credentials = {
      userName: LocalDB.getLoginUserName(),
      password: LocalDB.getLoginPassword(),
      employee: false,
    };

    return credentials;
  }

  //#region Local DB get methods
  static getAuthToken() {
    return localStorage.getItem("token");
  }

  static getRefreshToken() {
    return localStorage.getItem("refreshToken");
  }

  static getLoginUserName() {
    return localStorage.getItem("devusername");
  }

  static getLoginPassword() {
    return localStorage.getItem("devuserpassword");
  }

  static getUserPayload() {
    let payload = localStorage.getItem("payload");
    if (payload) {
      return JSON.parse(payload);
    }
  }

  static getLoanAppliationId() {
    return localStorage.getItem("loanApplicationId") || "";
  }

  static getTenantId() {
    return localStorage.getItem("tenantId") || "";
  }

  static getBusinessUnitId() {
    return localStorage.getItem("businessUnitId") || "";
  }

  //#endregion

  //#region Local DB Post Methods
  static storeTokenPayload(payload: any) {
    if (!payload) return;
    localStorage.setItem("payload", JSON.stringify(payload));
  }

  static storeAuthTokens(token: string, refreshToken: string) {
    localStorage.setItem("token", token);
    localStorage.setItem("refreshToken", refreshToken);
  }

  public static checkAuth(): boolean | string {
    let rainmaker2Token = cookies.get("Rainmaker2Token");
    let auth = localStorage.getItem("auth");
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
  //#endregion

  //#region Remove Auth
  static removeAuth() {
    let items = ["auth", "payload", "refreshToken"];
    for (const item of items) {
      localStorage.removeItem(item);
    }
  }
  //#endregion
}
