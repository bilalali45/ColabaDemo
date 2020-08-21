import axios from 'axios';
import {LocalDB} from '../Utils/LocalDB';
import {Http} from 'rainsoft-js';
import {Endpoints} from '../actions/Endpoints';
import Cookies from 'universal-cookie';
import jwt_decode from 'jwt-decode';
const http = new Http();
const cookies = new Cookies();

export class UserActions {
  static async authenticate() {
    const credentials = {
      userName: LocalDB.getLoginDevUserName(),
      password: LocalDB.getLoginDevPassword(),
      employee: true
    };

    let res: any = await http.post(
      Endpoints.User.POST.authorize(),
      credentials
    );
    if (!res.data.data) {
      return null;
    }
    let {token, refreshToken} = res.data.data;
    if (token && refreshToken) {
      LocalDB.storeTokenPayload(UserActions.decodeJwt(token));
      return {token, refreshToken};
    }
  }

  static async refreshToken() {
    try {
      if (!LocalDB.checkAuth()) {
        return;
      }
      let res: any = await http.post(Endpoints.User.POST.refreshToken(), {
        token: LocalDB.getAuthToken(),
        refreshToken: LocalDB.getRefreshToken()
      });

      if (res?.data?.data?.token && res?.data?.data?.refreshToken) {
        LocalDB.storeAuthTokens(
          res.data.data.token,
          res.data.data.refreshToken
        );
        let payload = UserActions.decodeJwt(res.data.data.token);
        LocalDB.storeTokenPayload(payload);
        UserActions.addExpiryListener();
        http.setAuth(res.data.data.token);
        return true;
      }
      console.log('Refresh token fail.');
      LocalDB.removeAuth();
      //window.open("/Login/LogOff", "_self");
      window.top.location.href = '/Login/LogOff';

      return false;
    } catch (error) {
      setTimeout(() => {
        UserActions.refreshToken();
      }, 10 * 1000);
      return false;
    }
  }

  static async authorize() {
    let isAuth = LocalDB.checkAuth();
    if (isAuth === 'token expired') {
      console.log('Refresh token called from authorize');
      let res: any = await UserActions.refreshToken();
      if (res) {
        return true;
      } else {
        return false;
      }
    }

    if (!isAuth) {
      if (process.env.NODE_ENV === 'development') {
        let tokens: any = await UserActions.authenticate();
        if (tokens?.token) {
          LocalDB.storeAuthTokens(tokens.token, tokens.refreshToken);
          http.setAuth(tokens.token);
          return true;
        } else {
          return false;
        }
      }

      let notificationToken = cookies.get('NotificationToken');
      let notificationRefreshToken = cookies.get('NotificationRefreshToken');
      console.log(
        'Cache token values in authorize NotificationToken',
        notificationToken,
        'notificationRefreshToken',
        notificationRefreshToken
      );
      if (notificationToken && notificationRefreshToken) {
        console.log('Cache token values exist');
        LocalDB.storeAuthTokens(notificationToken, notificationRefreshToken);
        LocalDB.storeTokenPayload(UserActions.decodeJwt(notificationToken));
        http.setAuth(notificationToken);
        let isAuth = LocalDB.checkAuth();
        console.log('Cache token check Auth', isAuth);
        if (isAuth === 'token expired' || !isAuth) {
          console.log('Cache token is not valid');
          console.log(
            'Refresh token called from authorize in case of MVC expire token'
          );
          await UserActions.refreshToken();
        }
        console.log('Cache token is valid');
        return true;
      } else {
        console.log('Cache token not found');
        return false;
      }
    } else {
      return true;
    }
  }

  static addExpiryListener() {
    const payload = LocalDB.getUserPayload();
    console.log('in listener added');
    if (payload != undefined) {
      let expiry = payload.exp;
      let currentTime = Date.now();
      let expiryTime = expiry * 1000;
      let time = expiryTime - currentTime;
      if (time < 1) {
        console.log(
          'Refresh token called from addExpiryListener in case of < 1'
        );
        (async () => {
          await UserActions.refreshToken();
        })();
        return;
      }

      console.log('toke will expire after', time, 'mil sec');
      setTimeout(async () => {
        console.log(
          'Refresh token called from addExpiryListener in case of time out meet'
        );
        await UserActions.refreshToken();
      }, time - 2000);
    }
  }

  static decodeJwt(token: any) {
    try {
      if (token) {
        let decoded = jwt_decode(token);
        return decoded;
      }
    } catch (error) {
      console.log(error);
    }
  }
}
