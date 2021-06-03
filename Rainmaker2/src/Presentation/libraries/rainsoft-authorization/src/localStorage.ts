import jwt_decode from "jwt-decode";
import Cookies from "universal-cookie";
import { Http } from "rainsoft-js";

import { ApplicationEnv } from "./applicationEnv";

const cookies = new Cookies();
const baseUrl: any = window.envConfig.API_BASE_URL;
new Http(baseUrl, "Rainmaker2Token", "https://apply.lendova.com:5003");

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

  static getCookie (name: string): string {
    return cookies.get(name)
  }
  
  static setCookie (name: string, value: string, path : string = "/"): void {
    cookies.set(name, value, { path: path })
    //document.cookie = name + "=" + value +";path=" + path;
  }

  //#region Local DB get methods
  static getAuthToken(): string | null {
    return this.decodeString(this.getCookie("Rainmaker2Token"));
  }

  static getRefreshToken(): string | null {
    return this.decodeString(this.getCookie("Rainmaker2RefreshToken"));
  }

  static getCookiePath(): string | null {
    return (
      this.decodeString(window.sessionStorage.getItem("CookiePath")) ||
      "/"
    );
  }

  static getLoginDevUserName(): string | null {
    return localStorage.getItem("devusername");
  }

  static getLoginDevPassword(): string | null {
    return localStorage.getItem("devuserpassword");
  }

  static getUserPayload(): any {
    const payload = this.decodeString(this.getCookie("TokenPayload"));

    if (payload) {
      return JSON.parse(payload);
    }

    return null;
  }

  //#endregion

  //#region Local DB Post Methods
  static storeTokenPayload(payload: any | string | undefined): void {
    if (!payload) return;

    this.setCookie(
      "TokenPayload",
      this.encodeString(JSON.stringify(payload)),
      this.getCookiePath() || "/"
    );
  }

  static storeAuthTokens(token: string, refreshToken: string): void {
    this.setCookie("Rainmaker2Token", this.encodeString(token), this.getCookiePath() || "/");
    this.setCookie(
      "Rainmaker2RefreshToken",
      this.encodeString(refreshToken),
      this.getCookiePath() || "/"
    );
    this.storeTokenPayload(this.decodeJwt(token));
  }

  static getPortalReferralUrl(): string | null {
    return localStorage.getItem("PortalReferralUrl");
  }

  static setPortalReferralUrl(portalReferralUrl: string): void {
    localStorage.setItem("PortalReferralUrl", portalReferralUrl);
  }

  public static setlocalStorage(name: string, data: string): void {
    localStorage.setItem(name, this.encodeString(data));
  }
  //#endregion

  //#region Remove Auth
  static removeAuthFromCookie(): void {
    const items = ["Rainmaker2Token", "TokenPayload", "Rainmaker2RefreshToken"];
    for (const item of items) {
      cookies.remove(item);
    }
  }
  //#endregion

  static decodeJwt(token: string): string | undefined {
    try {
      if (token) {
        const decoded: string = jwt_decode(token);
        return decoded;
      }
    } catch (error) {
      console.log(error);
      return undefined;
    }
  }
  
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
      return null;
    }
    try {
      const decodedString = atob(value);
      return String(decodedString.split("|")[0]);
    } catch {
      return null;
    }
  }
  //#endregion
}
