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



}