import { Http } from "rainsoft-js";
import { Auth } from "../../services/auth/Auth";

export class AuthActions {
  static async login(payload: any) {
    const res: any = await Http.post("/login", {
      email: Auth.getLoginUserName(),
      password: Auth.getLoginPassword(),
    });
    let token = res?.data?.token;
    if (token) {
      Auth.saveAuth(token);
      return token;
    }
  }
}
