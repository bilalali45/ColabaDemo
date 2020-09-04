import { Http } from 'rainsoft-js';
import { Auth } from "../../services/auth/Auth";

const httpClient = new Http()

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
