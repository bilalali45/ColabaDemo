export class Auth {
    
    public static saveAuth(token: string) {
        localStorage.setItem('auth', token);
    }
    
    public static checkAuth() {
        let auth = localStorage.getItem('auth');
        if (!auth) {
            return;
        }
        return auth;
    }

    public static removeAuth() {
        localStorage.removeItem('auth');    
    }

}