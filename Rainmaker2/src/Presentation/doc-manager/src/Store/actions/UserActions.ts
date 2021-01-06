import axios from 'axios';
import {Http} from 'rainsoft-js';
import {Endpoints} from '../endpoints/Endpoints';
import Cookies from 'universal-cookie';
import jwt_decode from 'jwt-decode';
import { UserEndpoints } from '../endpoints/UserEndpoints';
import { LocalDB } from '../../Utilities/LocalDB';

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
      console.log('In refreshParentApp Error');
      return false;
    }
  }
  static async retainLock() {
    let id = LocalDB.getLoanAppliationId();
    let url = UserEndpoints.POST.retainLock();

    try {
      let res = await Http.post(url, { loanApplicationId: parseInt(id) });
      return res.data;

    } catch (error) {
      console.log(error);
    }
  }

  static async aquireLock() {
    let id = LocalDB.getLoanAppliationId();
    let url = UserEndpoints.POST.aquireLock();
    try {
      let res = await Http.post(url, { loanApplicationId: parseInt(id) })
      return res.data;

    } catch (error) {
      console.log(error);
      return error?.response?.data;

    }
  }
}
