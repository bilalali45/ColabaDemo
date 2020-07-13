export class LocalDB {
    static getCredentials() {
        let credentials : any = {}
        let fields = ['userName', 'password', 'employee'];
        for (const field of fields) {
            credentials[field] = field === 'employee'? Boolean(localStorage.getItem(field)) : localStorage.getItem(field);
        }

        return credentials;
    }

    static storeAuthTokens(token: string, refreshToken: string) {
        localStorage.setItem('token', token);
        localStorage.setItem('refreshToken', refreshToken);
    }

    static getAuthToken() {
       return localStorage.getItem('token') || '';
    }
}