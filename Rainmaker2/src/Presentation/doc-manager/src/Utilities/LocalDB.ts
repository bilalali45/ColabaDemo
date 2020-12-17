import jwt_decode from 'jwt-decode';
import Cookies from 'universal-cookie';
// import { Role } from '../Store/Navigation';
import { ApplicationEnv } from './helpers/AppEnv';
const cookies = new Cookies();

export class LocalDB {
  //#region Local DB get methods
  static getAuthToken() {
    return this.decodeString(localStorage.getItem('Rainmaker2Token'));
  }

  static getRefreshToken() {
    return this.decodeString(localStorage.getItem('Rainmaker2RefreshToken'));
  }

  static getUserPayload() {
    let payload = this.decodeString(localStorage.getItem('TokenPayload'));
    if (payload) {
      return JSON.parse(payload);
    }
  }

  static getPortalReferralUrl() {
    return localStorage.getItem('PortalReferralUrl');
  }

  static getLoanAppliationId() {
    return (
      this.decodeString(window.sessionStorage.getItem('loanApplicationId')) ||
      ''
    );
  }

  static setLoanAppliationId(loanApplicationId: string) {
    window.sessionStorage.setItem(
      'loanApplicationId',
      this.encodeString(loanApplicationId)
    );
  }

  //#endregion

  //#region Local DB Post Methods



  public static storeItem(name: string, data: string) {
    localStorage.setItem(name, this.encodeString(data));
  }
  //#endregion

  //#region Remove Auth
  static removeAuth() {
    let items = ['Rainmaker2Token', 'TokenPayload', 'Rainmaker2RefreshToken'];
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

  // public static getUserRole() {
  //   return Role.ADMIN_ROLE; // 1
  //   //return Role.MCU_ROLE; // 2
  // }

  public static setCurrentUrl(currentNavigation: string) {
    window.sessionStorage.setItem(
      'CurrentNavigation',
      currentNavigation
    );
  }

  public static getCurrentUrl() {
    return (
      window.sessionStorage.getItem('CurrentNavigation') || '/Profile'
    );
  }

  public static getAuthentication(currentNavigation: string) {
    LocalDB.setCurrentUrl(currentNavigation);
    return true;
  }
  //#endregion
}
