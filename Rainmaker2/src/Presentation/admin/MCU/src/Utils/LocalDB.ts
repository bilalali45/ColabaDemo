export class LocalDB {
  static getCredentials() {
    const credentials = {
      userName: LocalDB.getLoginUserName(),
      password: LocalDB.getLoginPassword(),
      employee: false,
    };

    return credentials;
  }

  static storeAuthTokens(token: string, refreshToken: string) {
    localStorage.setItem("token", token);
    localStorage.setItem("refreshToken", refreshToken);
  }

  static getAuthToken() {
    return localStorage.getItem("token");
  }

  static getLoginUserName() {
    return localStorage.getItem("devusername");
  }

  static getLoginPassword() {
    return localStorage.getItem("devuserpassword");
  }
}
