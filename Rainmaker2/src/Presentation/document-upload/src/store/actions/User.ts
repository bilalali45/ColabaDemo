import { Http } from "../../services/http/Http";
// import { Http } from 'rainsoft-js';
import { Auth } from "../../services/auth/Auth";

const httpClient = new Http()
// const httpClient = new Http(Auth.getAuth())

export class AuthActions {

    static async login(payload: any) {
        const res: any = await httpClient.post('/login', { email: 'test@test.com', password: 'test123' });
        let token = res?.data?.token;
        if (token) {
            Auth.saveAuth(token);
            return token;
        }
    }
}

