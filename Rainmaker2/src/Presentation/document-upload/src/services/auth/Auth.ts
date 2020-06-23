import { UserActions } from "../../store/actions/UserActions";
import Cookies from "universal-cookie";

const cookies = new Cookies();

export class Auth {

    public static saveAuth(token: string) {
        localStorage.setItem('auth', token);
    }

    public static saveRefreshToken(refToken: string) {
        localStorage.setItem('refreshToken', refToken);
    }

    public static getRefreshToken() {
        localStorage.getItem('refreshToken');
    }

    public static removeRefreshToken() {
        localStorage.removeItem('refreshToken');
    }

    public static getAuth() {
        return localStorage.getItem('auth');
    }

    public static checkAuth(): boolean | string {
        let auth = localStorage.getItem('auth');
        if (!auth) {
            return false;
        }
        let payload = this.getUserPayload();
        if (payload) {
            let expiry = new Date(payload.exp * 1000);
            let currentDate = new Date(Date.now());
            if (currentDate <= expiry) {
                return true;
            } else {
                return 'token expired'
                // return false;
                // Auth.removeAuth();
            }
        }
        return true;
    }

    static async authenticate() {

        let isAuth = Auth.checkAuth();

        if (isAuth === 'token expired') {
            console.log('in token expired akldsjfkdfj');
            let res: any = await UserActions.refreshToken();
            if (res.data.token && res.data.refreshToken) {
                return true;
            }else {
                return false;
            }
        }

        if (!isAuth) {

            if (process.env.NODE_ENV === 'development') {
                let tokens: any = await UserActions.authenticate();
                if (tokens.token) {
                    Auth.saveAuth(tokens.token);
                    Auth.saveRefreshToken(tokens.refreshToken);
                    return true;
                } else {
                    return false;
                }
            }

            let Rainmaker2Token = cookies.get('Rainmaker2Token');
            let Rainmaker2RefreshToken = cookies.get('Rainmaker2RefreshToken');

            if (Rainmaker2Token && Rainmaker2RefreshToken) {
                Auth.saveAuth(Rainmaker2Token);
                Auth.saveRefreshToken(Rainmaker2RefreshToken);
                return true;
            }else {
                return false;
            }

        } else {
            return true;
        }
    }

    static storeTokenPayload(payload) {
        if (!payload) return;
        localStorage.setItem('payload', JSON.stringify(payload));
    }

    static getUserPayload() {
        let payload = localStorage.getItem('payload');
        if (payload) {
            return JSON.parse(payload);
        }
    }


    public static removeAuth() {
        let items = [
            'auth',
            'loanApplicationId',
            'tenantId',
            'businessUnitId',
            'payload',
            'refreshToken'
        ];
        for (const item of items) {
            localStorage.removeItem(item);
        }
    }

    public static getLoanAppliationId() {
        return localStorage.getItem('loanApplicationId') || '';

    }

    public static getTenantId() {
        return localStorage.getItem('tenantId') || '';

    }

    public static getBusinessUnitId() {
        return localStorage.getItem('businessUnitId') || '';

    }

    public static setLoanAppliationId(loanApplicationId: string) {
        localStorage.setItem('loanApplicationId', loanApplicationId);
    }

    public static setTenantId(tenantId: string) {
        localStorage.setItem('tenantId', tenantId);
    }

    public static setBusinessUnitId(businessUnitId: string) {
        localStorage.setItem('businessUnitId', businessUnitId);
    }


    public static soreItem(name: string, data: string) {
        localStorage.setItem(name, data);
    }

    public static removeItem(name: string) {
        localStorage.removeItem(name);
    }

}