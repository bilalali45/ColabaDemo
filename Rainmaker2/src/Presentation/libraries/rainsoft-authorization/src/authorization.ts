import { Http } from "rainsoft-js";
import { UserEndpoints } from "./endPoints/UserEndPoints";
import { LocalDB } from "./localStorage";
import Cookies from "universal-cookie";
import jwt_decode from "jwt-decode";
import { get } from "lodash";
import { Console } from "console";
import axios from "axios";

const cookies = new Cookies();

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
        userName: LocalDB.getLoginDevUserName(),
        password: LocalDB.getLoginDevPassword(),
        employee: false,
      };
      credentials.userName.includes("@")
        ? (credentials.employee = false)
        : (credentials.employee = true);
      const authorizeResponse = await Http.post(
        UserEndpoints.POST.authorize(),
        credentials
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
      if (!LocalDB.checkAuth()) {
        return;
      }
      const baseUrl: any = window.envConfig.API_BASE_URL;
      let url = `${baseUrl}${UserEndpoints.POST.refreshToken()}`
      const refreshTokenResponse: any = await axios({
        method: 'post',
        url: url,
        headers: {
          'Content-Type': 'application/json'
        },
        data: {
          token: LocalDB.getAuthToken(),
          refreshToken: LocalDB.getRefreshToken(),
        }
      });
      
      // await Http.post(
      //   UserEndpoints.POST.refreshToken(),
      //   {
      //     token: LocalDB.getAuthToken(),
      //     refreshToken: LocalDB.getRefreshToken(),
      //   }
      // );
      
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
      LocalDB.removeAuth();
      window.top.location.href = "/Account/LogOff";

      return false;
      
      
    } catch (error) {
      console.log("Refresh Token Error", error)
      LocalDB.removeAuth();
      window.top.location.href = "/Account/LogOff";
      return false;
    }
  }

  static async authorize(): Promise<boolean> {
    let rainmakerToken = cookies.get("Rainmaker2Token");
    let rainmakerRefreshToken = cookies.get("Rainmaker2RefreshToken");
    const auth = LocalDB.getAuthToken();
    
    const isAuth = LocalDB.checkAuth();
    if (isAuth === "token expired") {
      console.log("Refresh token called from authorize");
      const res: any = await this.refreshToken();
      if (res) {
        this.addExpiryListener();
        return true;
      } else {
        return false;
      }
    }

    console.log("process.env.NODE_ENV", process.env.NODE_ENV, "isAuth", isAuth);
    if (!isAuth) {
      if (rainmakerToken) {
        if (auth) {
          const decodeCacheToken: any = jwt_decode(rainmakerToken);
          const decodeAuth: any = jwt_decode(auth);
          if (decodeAuth.exp > decodeCacheToken.exp) {
            rainmakerToken = auth;
            rainmakerRefreshToken = LocalDB.getRefreshToken();
          }
        }
      } else {
        const tokens: any = await this.authenticate();
        if (tokens?.token) {
          rainmakerToken = tokens.token;
          rainmakerRefreshToken = tokens.refreshToken;
        }
      }

      console.log(
        "Cache token values in authorize Rainmaker2Token",
        rainmakerToken,
        "rainmakerRefreshToken",
        rainmakerRefreshToken
      );
      if (rainmakerToken && rainmakerRefreshToken) {
        console.log("Token values exist");
        LocalDB.storeAuthTokens(rainmakerToken, rainmakerRefreshToken);
        LocalDB.storeTokenPayload(this.decodeJwt(rainmakerToken));
        const isAuth = LocalDB.checkAuth();
        console.log("Token check Auth", isAuth);
        if (isAuth === "token expired" || !isAuth) {
          console.log("Cache token is not valid");
          console.log(
            "Refresh token called from authorize in case of MVC expire token"
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
        const decoded = jwt_decode(token);
        return decoded;
      }
    } catch (error) {
      console.log(error);
    }
  }
}
