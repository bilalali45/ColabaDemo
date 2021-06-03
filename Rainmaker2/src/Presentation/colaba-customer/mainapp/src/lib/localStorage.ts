import Cookies from "universal-cookie";
import { ApplicationEnv } from "./appEnv";
import jwt_decode from "jwt-decode";
import { UserInfoFromLocalDB } from "../Entities/Models/UserInfoFromLocalDB";

const cookies = new Cookies();

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
     document.cookie = name + "=" + value +";path=" + path;
  }

  

  //#region Local DB get methods
  static getAuthToken(): string | null {
    return this.decodeString(this.getCookie("Rainmaker2Token"));
  }

  static getRefreshToken(): string | null {
    return this.decodeString(this.getCookie("Rainmaker2RefreshToken"));
  }

  static getCookiePath(): string {
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

  static storeTokenPayload(payload: any | string | undefined): void {
    if (!payload) return;

    this.setCookie(
      "TokenPayload",
      this.encodeString(JSON.stringify(payload)),
      this.getCookiePath() 
    );
  }

  //#endregion

  

  static storeAuthTokens(token: string, refreshToken: string): void {
    this.setCookie("Rainmaker2Token", this.encodeString(token), this.getCookiePath());
    this.setCookie(
      "Rainmaker2RefreshToken",
      this.encodeString(refreshToken),
      this.getCookiePath()
    );
    this.storeTokenPayload(this.decodeJwt(token));
  }

  static storeCookiePath(path: string): void {
    window.sessionStorage.setItem(
      "CookiePath",
      this.encodeString(process.env.NODE_ENV == "production" ? path : "/")
    );
  }

  public static setlocalStorage(name: string, data: string): void {
    localStorage.setItem(name, this.encodeString(data));
  }

  static removeAuthFromCookie(): void {
    const items = ["Rainmaker2Token", "TokenPayload", "Rainmaker2RefreshToken"];
    for (const item of items) {
      cookies.remove(item);
    }
  }

  static clearSessionStorage(){
    window.sessionStorage.removeItem("loanApplicationId");
    window.sessionStorage.removeItem("borrowerId");
    window.sessionStorage.removeItem("loanGoalId");
    window.sessionStorage.removeItem("loanPurposeId");
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
      return decodedString.split("|")[0] ?? null;
    } catch {
      return null;
    }
  }
  //#endregion
  
  public static async getcaptchaCode (callBack: Function, params:any )  {
      const promise = new Promise((resolve, reject) => {
        window.grecaptcha.ready(() =>
        window.grecaptcha.execute('6LcBQXEaAAAAAJAs0qRkeMyd-ymNvoWpFhZt3UHj', { action: 'submit' }).then((token: string) => {
            resolve(token);
          },
          reject)
        );
      })
        .then(token => callBack(params,token)) // v3 token
        .catch(err => {
          console.log(err)
        });
      return promise;
  }

  static getLoggedInUserDetails(): UserInfoFromLocalDB | null{
    if(LocalDB.getAuthToken()) {
      let decryptedUserInfo:any = LocalDB.decodeJwt(LocalDB.getAuthToken() || "");
      let user: UserInfoFromLocalDB;
      if (decryptedUserInfo) {
        user = new UserInfoFromLocalDB(decryptedUserInfo["UserId"],decryptedUserInfo["FirstName"], decryptedUserInfo["LastName"], decryptedUserInfo["TenantCode"], decryptedUserInfo["BranchCode"],
          decryptedUserInfo["aud"], decryptedUserInfo["exp"], decryptedUserInfo["iss"]);
          return user;  
      }
      return null;
      }
      return null;
  }

  /*
  For decoding JWT Token so user details could be stored in context after decode
  */ 
  static decodeJwt(token: string): string | undefined | null {
    try {
      if (token) {
        const decoded: string = jwt_decode(token);
        return decoded;
      }
      return null;
    } catch (error) {
      console.log(error);
      return null;
    }
  }

  /*
  For Logging off user
  */
  public static logOffCurrentUser() {
    let cookies =  ['Rainmaker2Token','Rainmaker2RefreshToken', 'TokenPayload'];
    cookies.forEach(function (cookieName) {
      document.cookie = cookieName +`=; Path=${LocalDB.getCookiePath()}; Expires=Thu, 01 Jan 1970 00:00:01 GMT;`;
      console.log("deleting ", cookieName +`=; Path=${LocalDB.getCookiePath()}; Expires=Thu, 01 Jan 1970 00:00:01 GMT;Domain=${window.location.hostname}`);
    });
   }
}

