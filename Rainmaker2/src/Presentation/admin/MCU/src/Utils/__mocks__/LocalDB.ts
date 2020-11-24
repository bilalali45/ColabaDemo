export class LocalDB {
  //#region Local DB get methods
  static getAuthToken() {
    return 'Rainmaker2Token';
  }

  static getRefreshToken() {
    return 'Rainmaker2RefreshToken';
  }

  static getUserPayload() {
    return {};
  }

  static getLoanAppliationId() {
    return 3;
  }


  static getPortalReferralUrl() {
    return 'PortalReferralUrl';
  }

  static removeAuth() {}

  static setLoanAppliationId(loanApplicationId: string) {}

  public static checkAuth(): boolean | string {
    return true;
  }

  public static encodeString(value: string) {
  }
}
