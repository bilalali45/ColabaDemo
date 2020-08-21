import jwt_decode from 'jwt-decode';
import Cookies from 'universal-cookie';
import {ApplicationEnv} from './helpers/AppEnv';
import {Http} from 'rainsoft-js';
const cookies = new Cookies();
const httpClient = new Http();
let baseUrl: any = window.envConfig.API_BASE_URL;
httpClient.setBaseUrl(baseUrl);

export class LocalDB {
  static getCredentials() {
    const credentials = {
      userName: LocalDB.getLoginDevUserName(),
      password: LocalDB.getLoginDevPassword(),
      employee: true
    };

    return credentials;
  }

  //#region Local DB get methods
  static getAuthToken() {
    return this.decodeString(localStorage.getItem('notificationToken'));
  }

  static getRefreshToken() {
    return this.decodeString(localStorage.getItem('notificationRefreshToken'));
  }

  static getLoginDevUserName() {
    return localStorage.getItem('devusername');
  }

  static getLoginDevPassword() {
    return localStorage.getItem('devuserpassword');
  }

  static getUserPayload() {
    let payload = this.decodeString(
      localStorage.getItem('notificationpayload')
    );
    if (payload) {
      return JSON.parse(payload);
    }
  }

  //#endregion

  //#region Local DB Post Methods
  static storeTokenPayload(payload: any) {
    if (!payload) return;
    localStorage.setItem(
      'notificationPayload',
      this.encodeString(JSON.stringify(payload))
    );
  }

  static storeAuthTokens(token: string, refreshToken: string) {
    localStorage.setItem('notificationToken', this.encodeString(token));
    localStorage.setItem(
      'notificationRefreshToken',
      this.encodeString(refreshToken)
    );
  }

  public static checkAuth(): boolean | string {
    let notificationToken = cookies.get('NotificationToken');
    let auth = this.getAuthToken();
    if (!auth) {
      return false;
    }
    if (notificationToken) {
      const decodeCacheToken: any = jwt_decode(notificationToken);
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
        return 'token expired';
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
    let items = ['token', 'payload', 'refreshToken'];
    for (const item of items) {
      localStorage.removeItem(item);
    }
  }
  //#endregion

  //#region Encode Decode
  public static encodeString(value: string) {
    // Encode the String
    //const currentDate = Date.toString();
    const string = value + '|' + ApplicationEnv.Encode_Key;
    return btoa(unescape(encodeURIComponent(string)));
  }

  public static decodeString(value?: string | null) {
    // Decode the String
    if (!value) {
      return '';
    }
    try {
      const decodedString = atob(value);
      return decodedString.split('|')[0];
    } catch {
      return null;
    }
  }
  //#endregion
}
