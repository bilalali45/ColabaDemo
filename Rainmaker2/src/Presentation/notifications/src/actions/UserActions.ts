import {Http} from 'rainsoft-js';
import Cookies from 'universal-cookie';
import jwt_decode from 'jwt-decode';
import {get} from 'lodash';

import {LocalDB} from '../lib/localStorage';
import {Endpoints} from '../actions/Endpoints';
import {AuthorizeType} from '../lib/types';

const http = new Http();
const cookies = new Cookies();

export class UserActions {
  static async authenticate(): Promise<{
    token: any;
    refreshToken: any;
  } | null> {
    try {
      const credentials = {
        userName: LocalDB.getLoginDevUserName(),
        password: LocalDB.getLoginDevPassword(),
        employee: true
      };

      const authorizeResponse = await http.post<
        AuthorizeType,
        typeof credentials
      >(Endpoints.User.POST.authorize(), credentials);

      const {token, refreshToken} = get(authorizeResponse, 'data.data');

      if (token && refreshToken) {
        LocalDB.storeTokenPayload(UserActions.decodeJwt(token));
        return {token, refreshToken};
      }
      return null;
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

      const refreshTokenResponse = await http.post<
        AuthorizeType,
        AuthorizeType
      >(Endpoints.User.POST.refreshToken(), {
        token: LocalDB.getAuthToken(),
        refreshToken: LocalDB.getRefreshToken()
      });

      const {token, refreshToken} = get(refreshTokenResponse, 'data.data');

      if (!token || !refreshToken) {
        console.log('Refresh token fail.');
        LocalDB.removeAuth();
        window.top.location.href = '/Login/LogOff';

        return false;
      }

      LocalDB.storeAuthTokens(token, refreshToken);

      const payload = UserActions.decodeJwt(token);
      LocalDB.storeTokenPayload(payload);
      UserActions.addExpiryListener();
      http.setAuth(token);

      return true;
    } catch (error) {
      setTimeout(() => {
        UserActions.refreshToken();
      }, 10 * 1000);

      return false;
    }
  }

  static async authorize(): Promise<boolean> {
    const isAuth = LocalDB.checkAuth();

    if (isAuth === 'token expired') {
      console.log('Refresh token called from authorize');
      const res: any = await UserActions.refreshToken();
      if (res) {
        return true;
      } else {
        return false;
      }
    }

    if (!isAuth) {
      if (process.env.NODE_ENV === 'development') {
        const tokens: any = await UserActions.authenticate();
        if (tokens?.token) {
          LocalDB.storeAuthTokens(tokens.token, tokens.refreshToken);
          http.setAuth(tokens.token);
          return true;
        } else {
          return false;
        }
      }

      const notificationToken = cookies.get('NotificationToken');
      const notificationRefreshToken = cookies.get('NotificationRefreshToken');
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
        const isAuth = LocalDB.checkAuth();
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
      http.setAuth(LocalDB.getAuthToken() || '');
      return true;
    }
  }

  static addExpiryListener(): void {
    const payload = LocalDB.getUserPayload();
    console.log('in listener added');
    if (payload != undefined) {
      const expiry = payload.exp;
      const currentTime = Date.now();
      const expiryTime = expiry * 1000;
      const time = expiryTime - currentTime;
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

  static decodeJwt(token: string): string | undefined {
    try {
      if (token) {
        const decoded = jwt_decode<string>(token);
        return decoded;
      }
    } catch (error) {
      console.log(error);
    }
  }
}
