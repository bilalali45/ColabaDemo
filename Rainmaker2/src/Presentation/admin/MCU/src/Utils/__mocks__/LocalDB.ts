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
    return 81;
  }

  static removeAuth() {}

  static setLoanAppliationId(loanApplicationId: string) {}

  public static checkAuth(): boolean | string {
    return true;
  }
}
