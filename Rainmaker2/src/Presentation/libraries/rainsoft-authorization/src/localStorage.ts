import jwt_decode from "jwt-decode";
import Cookies from "universal-cookie";
import { Http } from "rainsoft-js";

import { ApplicationEnv } from "./applicationEnv";

const cookies = new Cookies();
const baseUrl: any = window.envConfig.API_BASE_URL;
const httpClient = new Http(baseUrl, "Rainmaker2Token");

export class LocalDB {
  static getCredentials(): {
    userName: string | null;
    password: string | null;
    employee: boolean;
  } {
    const credentials = {
      userName: LocalDB.getLoginDevUserName(),
      password: LocalDB.getLoginDevPassword(),
      employee: true,
    };

    return credentials;
  }

  //#region Local DB get methods
  static getAuthToken(): string | null {
    return this.decodeString(localStorage.getItem("Rainmaker2Token"));
  }

  static getRefreshToken(): string | null {
    return this.decodeString(localStorage.getItem("Rainmaker2RefreshToken"));
  }

  static getLoginDevUserName(): string | null {
    return localStorage.getItem("devusername");
  }

  static getLoginDevPassword(): string | null {
    return localStorage.getItem("devuserpassword");
  }

  static getUserPayload(): any {
    const payload = this.decodeString(localStorage.getItem("TokenPayload"));

    if (payload) {
      return JSON.parse(payload);
    }

    return null;
  }

  //#endregion

  //#region Local DB Post Methods
  static storeTokenPayload(payload: any | string | undefined): void {
    if (!payload) return;

    localStorage.setItem(
      "TokenPayload",
      this.encodeString(JSON.stringify(payload))
    );
  }

  static storeAuthTokens(token: string, refreshToken: string): void {
    localStorage.setItem("Rainmaker2Token", this.encodeString(token));
    localStorage.setItem(
      "Rainmaker2RefreshToken",
      this.encodeString(refreshToken)
    );
  }

  static getPortalReferralUrl(): string | null {
    return localStorage.getItem("PortalReferralUrl");
  }

  static setPortalReferralUrl(portalReferralUrl: string): void {
    localStorage.setItem("PortalReferralUrl", portalReferralUrl);
  }

  public static checkAuth(): boolean | string {
    const rainmaker2Token = cookies.get("Rainmaker2Token");
    const auth = this.getAuthToken();
    if (!auth) {
      return false;
    }
    if (rainmaker2Token) {
      const decodeCacheToken: any = jwt_decode(rainmaker2Token);
      const decodeAuth: any = jwt_decode(auth);
      if (decodeAuth?.UserName != decodeCacheToken?.UserName) {
        return false;
      }
      if (decodeCacheToken.exp > decodeAuth.exp) {
        console.log("Cache token is going to validate");
        return false;
      }
    }

    const payload = this.getUserPayload();
    if (payload) {
      const expiry = new Date(payload.exp * 1000);
      const currentDate = new Date(Date.now());
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

  public static storeItem(name: string, data: string): void {
    localStorage.setItem(name, this.encodeString(data));
  }
  //#endregion

  //#region Remove Auth
  static removeAuth(): void {
    const items = ["Rainmaker2Token", "TokenPayload", "Rainmaker2RefreshToken"];
    for (const item of items) {
      localStorage.removeItem(item);
    }
  }
  //#endregion

  //#region Encode Decode
  public static encodeString(value: string): string {
    // Encode the String
    //const currentDate = Date.toString();
    const string = value + "|" + ApplicationEnv.Encode_Key;
    return btoa(unescape(encodeURIComponent(string)));
  }

  public static decodeString(value?: string | null): string | null {
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
