import axios from 'axios';
import {LocalDB} from '../../Utils/LocalDB';
import {Http} from 'rainsoft-js';
import {Endpoints} from '../endpoints/Endpoints';
import Cookies from 'universal-cookie';
import jwt_decode from 'jwt-decode';
import { URL } from 'url';

const cookies = new Cookies();

export class UserActions {
  static keepAliveParentApp = () => {
    if (process.env.NODE_ENV === 'production') {
      setInterval(() => {
        UserActions.refreshParentApp();
      }, 60 * 1000);
    }
  };

  static async refreshParentApp() {
    try {
      console.log('In refreshParentApp');
      let baseUrl =  (new window.URL(localStorage.getItem('PortalReferralUrl')!)).origin;

      axios.get(baseUrl + '/Login/KeepAlive');
      return true;
    } catch (error) {
      console.log('In refreshParentApp Error',error);
      return false;
    }
  }
}
