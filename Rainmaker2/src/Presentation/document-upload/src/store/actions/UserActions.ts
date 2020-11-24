import { Auth } from "../../services/auth/Auth";
import { Endpoints } from "../endpoints/Endpoints";
import jwt_decode from "jwt-decode";
import { Http } from "rainsoft-js";
import Cookies from "universal-cookie";
import axios from "axios";
const cookies = new Cookies();

export class UserActions {
  static async refreshParentApp() {
    try {
      // console.log("In refreshParentApp");
      axios.get(window.location.origin + "/Account/KeepAlive");
      return true;
    } catch (error) {
      console.log("In refreshParentApp Error");
      return false;
    }
  }

  static getUserInfo() {
    let token = Auth.getAuth() || "";
    if (token) {
      let decoded = jwt_decode(token);
      return decoded;
    }
  }

  static getUserName() {
    let info: any = UserActions.getUserInfo();
    return ` ${info?.FirstName} ${info?.LastName} `;
  }

  static async logout() {
    Auth.removeAuth();
  }
}
