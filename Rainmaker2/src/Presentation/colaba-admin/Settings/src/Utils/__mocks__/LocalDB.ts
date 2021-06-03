import { Role } from "../../Store/Navigation";

export class LocalDB {
    //#region Local DB get methods
    static getAuthToken() {
      return 'token';
    }
  
    static getRefreshToken() {
      return 'refreshToken';
    }
  
    static getUserPayload() {
      return {};
    }
  
    static getLoanAppliationId() {
      return 3;
    }
  
    static removeAuth() {}
  
    static setLoanAppliationId(loanApplicationId: string) {}
  
    public static checkAuth(): boolean | string {
      return true;
    }

    public static getUserRole() {
      return Role.ADMIN_ROLE;
    }

    public static getAuthentication() {
      return true;
    }

    public static setCurrentUrl(currentNavigation: string) {
      
    }

    public static getCurrentUrl() {
      return '/Profile'
    }
  }
  