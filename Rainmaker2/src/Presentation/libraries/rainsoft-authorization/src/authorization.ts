import { Http } from "rainsoft-js";
import { UserEndpoints } from "./endPoints/UserEndPoints";
import { LocalDB } from "./localStorage";
import jwt_decode from "jwt-decode";
import { get } from "lodash";



export default class Authorization {

  //adding Cookie for testing locally 
  // static async addCookie(): Promise<{
  //   token: any;
  //   refreshToken: any;
  // } | null> {
  //   try {
  //     const credentials = {
  //       userName: "rainsoft",
  //       password: "rainsoft",
  //       employee: false,
  //     };
  //     credentials.userName.includes("@")
  //       ? (credentials.employee = false)
  //       : (credentials.employee = true);
  //     const authorizeResponse = await Http.post(
  //       UserEndpoints.POST.authorize(),
  //       credentials
  //     );

  //     const { token, refreshToken } = get(authorizeResponse, "data.data");
  //     console.log("CookieRefreshToken" + token);
  //     document.cookie = "Rainmaker2Token=" + token;
  //     document.cookie = "Rainmaker2RefreshToken=" + refreshToken;
  //   } catch (error) {
  //     console.warn(error);

  //     return null;
  //   }
  // }

  static async authenticate(): Promise<{
    token: any;
    refreshToken: any;
  } | null> {
    try {
      const credentials = {
        email: LocalDB.getLoginDevUserName(),
        password: LocalDB.getLoginDevPassword(),
        IsDevMode: true,
      };
      // credentials.email.includes("@")
      //   ? (credentials.employee = false)
      //   : (credentials.employee = true);
      const authorizeResponse = await Http.post(
        UserEndpoints.POST.customerauthorize(),
        credentials,  {
          'RecaptchaCode': "rainsoft"
        }, true
      );

      const { token, refreshToken } = get(authorizeResponse, "data.data");
      return { token, refreshToken };
    } catch (error) {
      console.warn(error);

      return null;
    }
  }

  static async refreshToken(): Promise<boolean | undefined> {
    try {
      if (!LocalDB.getAuthToken()) {
        return;
      }
      const refreshTokenResponse : any = await Http.post(
        UserEndpoints.POST.refreshToken(),
        {
          token: LocalDB.getAuthToken(),
          refreshToken: LocalDB.getRefreshToken(),
        }
      );
      
      if(refreshTokenResponse?.data?.data){
        const { token, refreshToken } = get(refreshTokenResponse, "data.data");
        if (token && refreshToken) {
          LocalDB.storeAuthTokens(token, refreshToken);

          const payload = this.decodeJwt(token);
          LocalDB.storeTokenPayload(payload);

          // Event raise to refresh 
          const refreshedTokenEvent = new Event('tokenrefreshed');
          window.dispatchEvent(refreshedTokenEvent);
          this.addExpiryListener();
          console.log("Token refresh successfully.");
          return true;
        }
      }
      
      console.log("Refresh token request fail.");
      LocalDB.removeAuthFromCookie();
      window.location.href = `${LocalDB.getCookiePath}app/signin}`;

      return false;
      
      
    } catch (error) {
      console.log("Refresh Token Error", error)
      // setTimeout(() => {
      //   this.refreshToken();
      // }, 10 * 1000);

      return false;
    }
  }

  static async authorize(): Promise<boolean> {
    let authToken = LocalDB.getAuthToken();
    let authRefreshToken = LocalDB.getRefreshToken();
    
    if (!this.checkAuth()) {
      if (window.location.origin.includes("localhost")) {
        console.log("Check Auth false and developer token in process");
        const tokens: any = await this.authenticate();
        if (tokens?.token) {
          authToken = tokens.token;
          authRefreshToken = tokens.refreshToken;
          if (authToken && authRefreshToken) {
            LocalDB.storeAuthTokens(authToken, authRefreshToken);  
          }
        }
      }

      if (!this.checkAuth()) {
        console.log("Refresh token called from authorize");
        const res: any = await this.refreshToken();
        if (res) {
          this.addExpiryListener();
          return true;
        } else {
          return false;
        }
      }

      if(!authToken){
        console.log("Cache token values does not exist");
      }
      
      if (authToken && authRefreshToken) {
        console.log("Token values exist");
        LocalDB.storeAuthTokens(authToken, authRefreshToken);
        LocalDB.storeTokenPayload(this.decodeJwt(authToken));
        const isAuth = this.checkAuth();
        console.log("Token check Auth", isAuth);
        if (!isAuth) {
          console.log("Cache token is not valid");
          console.log(
            "Refresh token called from authorize in case of expire token"
          );
          await this.refreshToken();
        }
        console.log("Token is valid");
        this.addExpiryListener();
        return true;
      } else {
        console.log("Token not found");
        return false;
      }
    } else {
      this.addExpiryListener();
      return true;
    }
  }

  static addExpiryListener(): void {
    const payload = LocalDB.getUserPayload();
    console.log("in listener added");
    if (payload != undefined) {
      const expiry = payload.exp;
      const currentTime = Date.now();
      const expiryTime = expiry * 1000;
      const time = expiryTime - currentTime;
      if (time < 1) {
        console.log(
          "Refresh token called from addExpiryListener in case of < 1"
        );
        (async () => {
          await this.refreshToken();
        })();
        return;
      }

      console.log("toke will expire after", time, "mil sec");
      setTimeout(async () => {
        console.log(
          "Refresh token called from addExpiryListener in case of time out meet"
        );
        await this.refreshToken();
      }, time - 2000);
    }
  }

  static decodeJwt(token: string): string | undefined {
    try {
      if (token) {
        const decoded: string = jwt_decode(token);
        return decoded;
      }
      return undefined;
    } catch (error) {
      console.log(error);
      return undefined;
    }
  }

  public static checkAuth(): boolean | string {
    const rainmaker2Token = LocalDB.getAuthToken();
    if (!rainmaker2Token) {
      return false;
    }
    // if (rainmaker2Token) {
    //   const decodeCacheToken: any = jwt_decode(rainmaker2Token);
    //   const decodeAuth: any = jwt_decode(auth);
    //   if (decodeAuth?.UserName != decodeCacheToken?.UserName) {
    //     return false;
    //   }
    //   if (decodeCacheToken.exp > decodeAuth.exp) {
    //     console.log("Cache token is going to validate");
    //     return false;
    //   }
    // }

    const payload = LocalDB.getUserPayload();
    if (payload) {
      const expiry = new Date(payload.exp * 1000);
      const currentDate = new Date(Date.now());
      if (currentDate < expiry) {
        return true;
      } else {
        return false;
      }
    }
    return false;
  }

}
