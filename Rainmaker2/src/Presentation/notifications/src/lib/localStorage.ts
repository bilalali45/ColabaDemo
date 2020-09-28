import jwt_decode from 'jwt-decode';
import Cookies from 'universal-cookie';
import {Http} from 'rainsoft-js';

import {ApplicationEnv} from './appEnv';
import {PayloadType} from './types';

const cookies = new Cookies();
const httpClient = new Http();
const baseUrl: any = window.envConfig.API_BASE_URL;
httpClient.setBaseUrl(baseUrl);

export class LocalDB {
  static getCredentials(): {
    userName: string | null;
    password: string | null;
    employee: boolean;
  } {
    const credentials = {
      userName: LocalDB.getLoginDevUserName(),
      password: LocalDB.getLoginDevPassword(),
      employee: true
    };

    return credentials;
  }

  //#region Local DB get methods
  static getAuthToken(): string | null {
    return this.decodeString(localStorage.getItem('notificationToken'));
  }

  static getRefreshToken(): string | null {
    return this.decodeString(localStorage.getItem('notificationRefreshToken'));
  }

  static getLoginDevUserName(): string | null {
    return localStorage.getItem('devusername');
  }

  static getLoginDevPassword(): string | null {
    return localStorage.getItem('devuserpassword');
  }

  static getUserPayload(): any {
    const payload = this.decodeString(
      localStorage.getItem('notificationPayload')
    );

    if (payload) {
      return JSON.parse(payload);
    }

    return null;
  }

  //#endregion

  //#region Local DB Post Methods
  static storeTokenPayload(payload: PayloadType | string | undefined): void {
    if (!payload) return;

    localStorage.setItem(
      'notificationPayload',
      this.encodeString(JSON.stringify(payload))
    );
  }

  static storeAuthTokens(token: string, refreshToken: string): void {
    localStorage.setItem('notificationToken', this.encodeString(token));
    localStorage.setItem(
      'notificationRefreshToken',
      this.encodeString(refreshToken)
    );
  }

  static getPortalReferralUrl(): string | null {
    return localStorage.getItem('PortalReferralUrl');
  }

  static setPortalReferralUrl(portalReferralUrl: string): void {
    localStorage.setItem('PortalReferralUrl', portalReferralUrl);
  }

  public static checkAuth(): boolean | string {
    const notificationToken = cookies.get('NotificationToken');
    const auth = this.getAuthToken();
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

    const payload = this.getUserPayload();
    if (payload) {
      const expiry = new Date(payload.exp * 1000);
      const currentDate = new Date(Date.now());
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

  public static storeItem(name: string, data: string): void {
    localStorage.setItem(name, this.encodeString(data));
  }
  //#endregion

  //#region Remove Auth
  static removeAuth(): void {
    const items = [
      'notificationToken',
      'notificationPayload',
      'notificationRefreshToken'
    ];
    for (const item of items) {
      localStorage.removeItem(item);
    }
  }
  //#endregion

  //#region Encode Decode
  public static encodeString(value: string): string {
    // Encode the String
    //const currentDate = Date.toString();
    const string = value + '|' + ApplicationEnv.Encode_Key;
    return btoa(unescape(encodeURIComponent(string)));
  }

  public static decodeString(value?: string | null): string | null {
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
