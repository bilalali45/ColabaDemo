import { Http } from "../../services/http/Http";
import { Auth } from "../../services/auth/Auth";

const http = new Http();

export class UserActions {
    static async authenticate() {
      let res: any = await http.post<{token: string}, {email: string}>('/authorize', {email: 'test@test.com'});
      return res.data;
    }

    static async getUserInfo() {

    }

    static async getLoanApplication() {

    }

    static async getRequiredDocuments() {

    }

    static async getSubmittedDocuments() {

    }

    static async submitDocuments() {

    }

    static async logout() {
        Auth.removeAuth();
    }
}